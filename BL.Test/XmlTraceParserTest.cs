using System.Text;
using System.Xml;
using BL.Interfaces.Services;
using BL.Models.Imports;
using BL.Services;
using BL.Settings;
using Domain.Imports;

namespace BL.Tests.Services
{
    public class XmlTraceParserTests
    {
        private readonly IXmlTraceParser _parser;

        public XmlTraceParserTests()
        {
            XmlTraceParserSettings settings = new()
            {
                MaxCharactersInDocument = 1_000_000
            };

            _parser = new XmlTraceParser(settings);
        }

        [Fact]
        public async Task ParseAsync_ShouldReadKnownAndUnknownTraceData()
        {
            const string xml = """
                <?xml version="1.0" encoding="UTF-8"?>
                <traces>
                  <trace>
                    <type>13</type>
                    <source></source>
                    <time>2026-01-18T08:06:49.721</time>
                    <coordinate>
                      <latitude>50.37949</latitude>
                      <longitude>4.27785</longitude>
                    </coordinate>
                    <mileage>533236605</mileage>
                    <heading>190</heading>
                    <speed>0</speed>
                    <addressmatch>unknown but preserved</addressmatch>
                    <property>
                      <key>ATY</key>
                      <value>Décharger Regie</value>
                    </property>
                    <property>
                      <key>LID</key>
                      <value>6000047c21768719497208-126</value>
                    </property>
                    <property>
                      <key>ALEN</key>
                      <value>5000000000</value>
                    </property>
                    <property>
                      <key>DID</key>
                      <value>787;325</value>
                    </property>
                    <property>
                      <key>AST</key>
                      <value>2026-01-18T06:57:29.631</value>
                    </property>
                    <property>
                      <key>AFRE</key>
                      <value>&lt;report&gt;ok&lt;/report&gt;</value>
                    </property>
                    <property>
                      <key>UNKNOWN</key>
                      <value>kept</value>
                    </property>
                  </trace>
                </traces>
                """;

            using MemoryStream stream =
                CreateStream(xml);

            XmlTraceParseResult result =
                await _parser.ParseAsync(
                    stream,
                    CancellationToken.None);

            ParsedTraceData trace =
                Assert.Single(result.Traces);

            Assert.Empty(result.Errors);
            Assert.Equal(13, trace.TraceType);
            Assert.Equal(string.Empty, trace.SourceRaw);
            Assert.Equal(50.37949m, trace.Latitude);
            Assert.Equal(4.27785m, trace.Longitude);
            Assert.Equal(533_236_605L, trace.Mileage);

            Assert.Equal(
                "6000047c21768719497208-126",
                trace.LinkId);

            Assert.Equal(
                "Décharger Regie",
                trace.ActivityCode);

            Assert.Equal(
                "787;325",
                trace.DriverIdsRaw);

            Assert.Equal(
                5_000_000_000L,
                trace.ActivityLengthMilliseconds);

            Assert.Equal(
                "<report>ok</report>",
                trace.ActivityFinalReportRaw);

            Assert.Equal(
                DateTimeKind.Unspecified,
                trace.TechnicalTime!.Value.Kind);

            Assert.Equal(
                DateTimeKind.Unspecified,
                trace.ActivityStartTime!.Value.Kind);

            Assert.Equal(64, trace.TraceHash.Length);

            Assert.Contains(
                "addressmatch",
                trace.RawXml);

            Assert.Contains(
                trace.Properties,
                property =>
                    property.KeyRaw == "UNKNOWN"
                    && property.ValueRaw == "kept");
        }

        [Fact]
        public async Task
            ParseAsync_ShouldReadSeveralTracesInTheirOriginalOrder()
        {
            const string xml = """
                <traces>
                  <trace>
                    <type>10</type>
                    <time>2026-01-18T08:00:00</time>
                  </trace>
                  <trace>
                    <type>13</type>
                    <time>2026-01-18T09:00:00</time>
                  </trace>
                </traces>
                """;

            using MemoryStream stream =
                CreateStream(xml);

            XmlTraceParseResult result =
                await _parser.ParseAsync(
                    stream,
                    CancellationToken.None);

            Assert.Equal(2, result.Traces.Count);
            Assert.Equal(1, result.Traces[0].Position);
            Assert.Equal(10, result.Traces[0].TraceType);
            Assert.Equal(2, result.Traces[1].Position);
            Assert.Equal(13, result.Traces[1].TraceType);
        }

        [Fact]
        public async Task
            ParseAsync_ShouldPreserveRawValuesWhenConversionsFail()
        {
            const string xml = """
                <traces>
                  <trace>
                    <type>not-an-integer</type>
                    <time>not-a-date</time>
                    <coordinate>
                      <latitude>invalid-latitude</latitude>
                    </coordinate>
                    <property>
                      <key>ALEN</key>
                      <value>invalid-duration</value>
                    </property>
                  </trace>
                </traces>
                """;

            using MemoryStream stream =
                CreateStream(xml);

            XmlTraceParseResult result =
                await _parser.ParseAsync(
                    stream,
                    CancellationToken.None);

            ParsedTraceData trace =
                Assert.Single(result.Traces);

            Assert.Equal(
                "not-an-integer",
                trace.TraceTypeRaw);

            Assert.Null(trace.TraceType);

            Assert.Equal(
                "not-a-date",
                trace.TechnicalTimeRaw);

            Assert.Null(trace.TechnicalTime);

            Assert.Equal(
                "invalid-latitude",
                trace.LatitudeRaw);

            Assert.Null(trace.Latitude);

            Assert.Equal(
                "invalid-duration",
                trace.ActivityLengthMillisecondsRaw);

            Assert.Null(
                trace.ActivityLengthMilliseconds);

            Assert.Collection(
                result.Errors,
                error => Assert.Equal(
                    "TRACE_TYPE_INVALID",
                    error.Code),
                error => Assert.Equal(
                    "TECHNICAL_TIME_INVALID",
                    error.Code),
                error => Assert.Equal(
                    "LATITUDE_INVALID",
                    error.Code),
                error => Assert.Equal(
                    "ACTIVITY_LENGTH_INVALID",
                    error.Code));

            Assert.All(
                result.Errors,
                error =>
                {
                    Assert.Equal(
                        1,
                        error.TracePosition);

                    Assert.Equal(
                        ImportErrorSeverity.Warning,
                        error.Severity);

                    Assert.False(error.RejectsTrace);
                });
        }

        [Fact]
        public async Task
            ParseAsync_ShouldAcceptMissingOptionalElements()
        {
            const string xml = """
                <traces>
                  <trace>
                    <type>10</type>
                    <time>2026-01-18T08:00:00</time>
                  </trace>
                </traces>
                """;

            using MemoryStream stream =
                CreateStream(xml);

            XmlTraceParseResult result =
                await _parser.ParseAsync(
                    stream,
                    CancellationToken.None);

            ParsedTraceData trace =
                Assert.Single(result.Traces);

            Assert.Empty(result.Errors);
            Assert.Null(trace.SourceRaw);
            Assert.Null(trace.LatitudeRaw);
            Assert.Null(trace.LinkId);
            Assert.Empty(trace.Properties);
        }

        [Fact]
        public async Task
            ParseAsync_ShouldReportMissingRequiredTraceData()
        {
            const string xml = """
                <traces>
                  <trace>
                    <source></source>
                  </trace>
                </traces>
                """;

            using MemoryStream stream =
                CreateStream(xml);

            XmlTraceParseResult result =
                await _parser.ParseAsync(
                    stream,
                    CancellationToken.None);

            ParsedTraceData trace =
                Assert.Single(result.Traces);

            Assert.Null(trace.TraceTypeRaw);
            Assert.Null(trace.TraceType);
            Assert.Null(trace.TechnicalTimeRaw);
            Assert.Null(trace.TechnicalTime);

            Assert.Collection(
                result.Errors,
                error => Assert.Equal(
                    "TRACE_TYPE_MISSING",
                    error.Code),
                error => Assert.Equal(
                    "TECHNICAL_TIME_MISSING",
                    error.Code));
        }

        [Fact]
        public async Task
            ParseAsync_ShouldConvertExplicitTimeZoneToUtc()
        {
            const string xml = """
                <traces>
                  <trace>
                    <type>10</type>
                    <time>2026-01-18T08:00:00+02:00</time>
                  </trace>
                </traces>
                """;

            using MemoryStream stream =
                CreateStream(xml);

            XmlTraceParseResult result =
                await _parser.ParseAsync(
                    stream,
                    CancellationToken.None);

            DateTime technicalTime =
                Assert.Single(result.Traces)
                    .TechnicalTime!
                    .Value;

            Assert.Equal(
                DateTimeKind.Utc,
                technicalTime.Kind);

            Assert.Equal(
                new DateTime(
                    2026,
                    1,
                    18,
                    6,
                    0,
                    0,
                    DateTimeKind.Utc),
                technicalTime);
        }

        [Fact]
        public async Task
            ParseAsync_ShouldCreateSameHashWhenOnlyIndentationChanges()
        {
            const string compactXml =
                "<traces><trace><type>10</type>"
                + "<time>2026-01-18T08:00:00</time>"
                + "</trace></traces>";

            const string indentedXml = """
                <traces>
                  <trace>
                    <type>10</type>
                    <time>2026-01-18T08:00:00</time>
                  </trace>
                </traces>
                """;

            using MemoryStream compactStream =
                CreateStream(compactXml);

            using MemoryStream indentedStream =
                CreateStream(indentedXml);

            XmlTraceParseResult compactResult =
                await _parser.ParseAsync(
                    compactStream,
                    CancellationToken.None);

            XmlTraceParseResult indentedResult =
                await _parser.ParseAsync(
                    indentedStream,
                    CancellationToken.None);

            Assert.Equal(
                Assert.Single(
                    compactResult.Traces)
                    .TraceHash,
                Assert.Single(
                    indentedResult.Traces)
                    .TraceHash);
        }

        [Fact]
        public async Task
            ParseAsync_ShouldRejectUnexpectedRootElement()
        {
            const string xml = "<items />";

            using MemoryStream stream =
                CreateStream(xml);

            await Assert.ThrowsAsync<XmlException>(
                () => _parser.ParseAsync(
                    stream,
                    CancellationToken.None));
        }

        [Fact]
        public async Task
            ParseAsync_ShouldRejectMalformedXml()
        {
            const string xml =
                "<traces><trace></traces>";

            using MemoryStream stream =
                CreateStream(xml);

            await Assert.ThrowsAsync<XmlException>(
                () => _parser.ParseAsync(
                    stream,
                    CancellationToken.None));
        }

        [Fact]
        public async Task
            ParseAsync_ShouldRejectDtdAndExternalEntities()
        {
            const string xml = """
                <!DOCTYPE traces [
                  <!ENTITY external SYSTEM "file:///confidential.txt">
                ]>
                <traces>
                  <trace>
                    <type>10</type>
                    <time>&external;</time>
                  </trace>
                </traces>
                """;

            using MemoryStream stream =
                CreateStream(xml);

            await Assert.ThrowsAsync<XmlException>(
                () => _parser.ParseAsync(
                    stream,
                    CancellationToken.None));
        }

        private static MemoryStream CreateStream(
            string xml)
        {
            return new MemoryStream(
                Encoding.UTF8.GetBytes(xml));
        }
    }
}