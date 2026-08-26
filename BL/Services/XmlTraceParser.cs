using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BL.Interfaces.Services;
using BL.Models.Imports;
using BL.Settings;
using Domain.Imports;

namespace BL.Services
{
    public class XmlTraceParser : IXmlTraceParser
    {
        private readonly long _maxCharactersInDocument;

        public XmlTraceParser(XmlTraceParserSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            if (settings.MaxCharactersInDocument <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(settings.MaxCharactersInDocument),
                    "The maximum XML character count must be greater than zero.");
            }

            _maxCharactersInDocument = settings.MaxCharactersInDocument;
        }

        public async Task<XmlTraceParseResult> ParseAsync(Stream xmlStream, CancellationToken cancellationToken)
        {
            ValidateStream(xmlStream);

            cancellationToken.ThrowIfCancellationRequested();

            XmlReaderSettings readerSettings = BuildReaderSettings();

            using XmlReader xmlReader = XmlReader.Create(xmlStream, readerSettings);

            XmlNodeType rootNodeType = await xmlReader.MoveToContentAsync();

            ValidateRootElement(xmlReader, rootNodeType);

            int rootDepth = xmlReader.Depth;
            int tracePosition = 0;

            List<ParsedTraceData> traces = new();
            List<XmlTraceParseError> errors = new();

            while (await xmlReader.ReadAsync())
            {
                cancellationToken.ThrowIfCancellationRequested();

                bool isDirectTraceElement =
                    xmlReader.NodeType == XmlNodeType.Element
                    && xmlReader.Depth == rootDepth + 1
                    && HasLocalName(xmlReader, "trace");

                if (!isDirectTraceElement)
                {
                    continue;
                }

                tracePosition++;

                XElement traceElement =
                    await ReadCurrentElementAsync(
                        xmlReader,
                        cancellationToken);

                ParsedTraceData trace = ParseTrace(
                    traceElement,
                    tracePosition,
                    errors);

                traces.Add(trace);
            }

            return new XmlTraceParseResult(traces.AsReadOnly(), errors.AsReadOnly());
        }

        private static void ValidateStream(
            Stream xmlStream)
        {
            ArgumentNullException.ThrowIfNull(xmlStream);

            if (!xmlStream.CanRead)
            {
                throw new ArgumentException("The XML stream must be readable.", nameof(xmlStream));
            }
        }

        private XmlReaderSettings BuildReaderSettings()
        {
            return new XmlReaderSettings
            {
                Async = true,
                CloseInput = false,
                ConformanceLevel = ConformanceLevel.Document,
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null,
                MaxCharactersInDocument =
                    _maxCharactersInDocument,
                IgnoreComments = false,
                IgnoreWhitespace = false
            };
        }

        private static void ValidateRootElement(XmlReader xmlReader, XmlNodeType rootNodeType)
        {
            bool hasExpectedRoot =
                rootNodeType == XmlNodeType.Element
                && HasLocalName(xmlReader, "traces");

            if (!hasExpectedRoot)
            {
                throw new XmlException("The XML document root must be <traces>.");
            }
        }

        private static async Task<XElement>
            ReadCurrentElementAsync(XmlReader xmlReader, CancellationToken cancellationToken)
        {
            using XmlReader elementReader = xmlReader.ReadSubtree();

            return await XElement.LoadAsync(elementReader, LoadOptions.PreserveWhitespace, cancellationToken);
        }

        private static ParsedTraceData ParseTrace(XElement traceElement, int tracePosition, ICollection<XmlTraceParseError> errors)
        {
            string? traceTypeRaw =
                GetDirectElementValue(
                    traceElement,
                    "type");

            string? sourceRaw =
                GetDirectElementValue(
                    traceElement,
                    "source");

            string? technicalTimeRaw =
                GetDirectElementValue(
                    traceElement,
                    "time");

            XElement? coordinateElement =
                GetDirectElement(
                    traceElement,
                    "coordinate");

            string? latitudeRaw =
                coordinateElement is null
                    ? null
                    : GetDirectElementValue(
                        coordinateElement,
                        "latitude");

            string? longitudeRaw =
                coordinateElement is null
                    ? null
                    : GetDirectElementValue(
                        coordinateElement,
                        "longitude");

            string? mileageRaw =
                GetDirectElementValue(
                    traceElement,
                    "mileage");

            string? headingRaw =
                GetDirectElementValue(
                    traceElement,
                    "heading");

            string? speedRaw =
                GetDirectElementValue(
                    traceElement,
                    "speed");

            IReadOnlyList<ParsedTracePropertyData> properties = ParseProperties(traceElement);

            string? sequenceRaw =
                GetLastPropertyValue(
                    properties,
                    "SEQ");

            string? activityStartTimeRaw =
                GetLastPropertyValue(
                    properties,
                    "AST");

            string? activityLengthRaw =
                GetLastPropertyValue(
                    properties,
                    "ALEN");

            string? drivingLengthRaw =
                GetLastPropertyValue(
                    properties,
                    "DRLEN");

            return new ParsedTraceData
            {
                Position = tracePosition,

                TraceTypeRaw = traceTypeRaw,

                TraceType = ParseRequiredTraceType(
                    traceTypeRaw,
                    tracePosition,
                    errors),

                SourceRaw = sourceRaw,

                TechnicalTimeRaw = technicalTimeRaw,

                TechnicalTime = ParseRequiredTechnicalTime(
                    technicalTimeRaw,
                    tracePosition,
                    errors),

                LatitudeRaw = latitudeRaw,

                Latitude = ParseOptionalDecimal(
                    latitudeRaw,
                    "LATITUDE_INVALID",
                    "The trace latitude is invalid.",
                    tracePosition,
                    errors),

                LongitudeRaw = longitudeRaw,

                Longitude = ParseOptionalDecimal(
                    longitudeRaw,
                    "LONGITUDE_INVALID",
                    "The trace longitude is invalid.",
                    tracePosition,
                    errors),

                MileageRaw = mileageRaw,

                Mileage = ParseOptionalLong(
                    mileageRaw,
                    "MILEAGE_INVALID",
                    "The raw trace mileage is not a valid integer.",
                    tracePosition,
                    errors),

                HeadingRaw = headingRaw,

                Heading = ParseOptionalDecimal(
                    headingRaw,
                    "HEADING_INVALID",
                    "The trace heading is invalid.",
                    tracePosition,
                    errors),

                SpeedRaw = speedRaw,

                Speed = ParseOptionalDecimal(
                    speedRaw,
                    "SPEED_INVALID",
                    "The trace speed is invalid.",
                    tracePosition,
                    errors),

                LinkId = GetLastPropertyValue(
                    properties,
                    "LID"),

                ActivityCode = GetLastPropertyValue(
                    properties,
                    "ATY"),

                DriverIdsRaw = GetLastPropertyValue(
                    properties,
                    "DID"),

                SequenceRaw = sequenceRaw,

                Sequence = ParseOptionalLong(
                    sequenceRaw,
                    "SEQUENCE_INVALID",
                    "The SEQ property is not a valid long integer.",
                    tracePosition,
                    errors),

                ActivityStartTimeRaw =
                    activityStartTimeRaw,

                ActivityStartTime = ParseOptionalDateTime(
                    activityStartTimeRaw,
                    "ACTIVITY_START_INVALID",
                    "The AST property is not a valid date.",
                    tracePosition,
                    errors),

                ActivityLengthMillisecondsRaw =
                    activityLengthRaw,

                ActivityLengthMilliseconds =
                    ParseOptionalLong(
                        activityLengthRaw,
                        "ACTIVITY_LENGTH_INVALID",
                        "The ALEN property is not a valid long integer.",
                        tracePosition,
                        errors),

                DrivingLengthMillisecondsRaw =
                    drivingLengthRaw,

                DrivingLengthMilliseconds =
                    ParseOptionalLong(
                        drivingLengthRaw,
                        "DRIVING_LENGTH_INVALID",
                        "The DRLEN property is not a valid long integer.",
                        tracePosition,
                        errors),

                DeviceRaw = GetLastPropertyValue(
                    properties,
                    "DEVICE"),

                ActivityReportRaw =
                    GetLastPropertyValue(
                        properties,
                        "ARE"),

                ActivityFinalReportRaw =
                    GetLastPropertyValue(
                        properties,
                        "AFRE"),

                TraceHash =
                    CalculateTraceHash(traceElement),

                RawXml = traceElement.ToString(
                    SaveOptions.DisableFormatting),

                Properties = properties
            };
        }

        private static IReadOnlyList<ParsedTracePropertyData>
            ParseProperties(XElement traceElement)
        {
            List<ParsedTracePropertyData> properties = new();

            int propertyPosition = 0;

            foreach (XElement propertyElement
                in traceElement.Elements()
                    .Where(element =>
                        HasLocalName(
                            element,
                            "property")))
            {
                propertyPosition++;

                properties.Add(
                    new ParsedTracePropertyData
                    {
                        Position = propertyPosition,

                        KeyRaw = GetDirectElementValue(
                            propertyElement,
                            "key"),

                        ValueRaw = GetDirectElementValue(
                            propertyElement,
                            "value")
                    });
            }

            return properties.AsReadOnly();
        }

        private static int? ParseRequiredTraceType(string? rawValue, int tracePosition, ICollection<XmlTraceParseError> errors)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                AddConversionError(
                    "TRACE_TYPE_MISSING",
                    "The trace type is missing.",
                    tracePosition,
                    errors);

                return null;
            }

            if (int.TryParse(
                rawValue,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out int parsedValue))
            {
                return parsedValue;
            }

            AddConversionError(
                "TRACE_TYPE_INVALID",
                "The trace type is not a valid integer.",
                tracePosition,
                errors);

            return null;
        }

        private static DateTime?
            ParseRequiredTechnicalTime(
                string? rawValue,
                int tracePosition,
                ICollection<XmlTraceParseError> errors)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                AddConversionError(
                    "TECHNICAL_TIME_MISSING",
                    "The technical trace time is missing.",
                    tracePosition,
                    errors);

                return null;
            }

            return ParseOptionalDateTime(
                rawValue,
                "TECHNICAL_TIME_INVALID",
                "The technical trace time is invalid.",
                tracePosition,
                errors);
        }

        private static long? ParseOptionalLong(string? rawValue, string errorCode, string errorMessage, int tracePosition, ICollection<XmlTraceParseError> errors)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return null;
            }

            if (long.TryParse(
                rawValue,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out long parsedValue))
            {
                return parsedValue;
            }

            AddConversionError(
                errorCode,
                errorMessage,
                tracePosition,
                errors);

            return null;
        }

        private static decimal? ParseOptionalDecimal( string? rawValue, string errorCode, string errorMessage,int tracePosition, ICollection<XmlTraceParseError> errors)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return null;
            }

            if (decimal.TryParse(
                rawValue,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out decimal parsedValue))
            {
                return parsedValue;
            }

            AddConversionError(
                errorCode,
                errorMessage,
                tracePosition,
                errors);

            return null;
        }

        private static DateTime? ParseOptionalDateTime(string? rawValue, string errorCode, string errorMessage, int tracePosition, ICollection<XmlTraceParseError> errors)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return null;
            }

            if (HasExplicitTimeZone(rawValue))
            {
                if (DateTimeOffset.TryParse(
                    rawValue,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces,
                    out DateTimeOffset parsedOffset))
                {
                    return parsedOffset.UtcDateTime;
                }
            }
            else if (DateTime.TryParse(
                rawValue,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out DateTime parsedDateTime))
            {
                return DateTime.SpecifyKind(
                    parsedDateTime,
                    DateTimeKind.Unspecified);
            }

            AddConversionError(
                errorCode,
                errorMessage,
                tracePosition,
                errors);

            return null;
        }

        private static bool HasExplicitTimeZone(string rawValue)
        {
            string trimmedValue = rawValue.Trim();

            if (trimmedValue.EndsWith('Z') || trimmedValue.EndsWith('z'))
            {
                return true;
            }

            int timeSeparatorPosition = trimmedValue.IndexOf('T');

            if (timeSeparatorPosition < 0)
            {
                timeSeparatorPosition =
                    trimmedValue.IndexOf(' ');
            }

            if (timeSeparatorPosition < 0)
            {
                return false;
            }

            int plusPosition = trimmedValue.LastIndexOf('+');

            int minusPosition = trimmedValue.LastIndexOf('-');

            return plusPosition > timeSeparatorPosition || minusPosition > timeSeparatorPosition;
        }

        private static void AddConversionError(string errorCode, string errorMessage, int tracePosition, ICollection<XmlTraceParseError> errors)
        {
            errors.Add(
                new XmlTraceParseError
                {
                    Code = errorCode,
                    Message = errorMessage,
                    TracePosition = tracePosition,
                    Severity = ImportErrorSeverity.Warning,
                    RejectsTrace = false
                });
        }

        private static string CalculateTraceHash(XElement traceElement)
        {
            XElement canonicalElement = new(traceElement);

            List<XText> insignificantWhitespaceNodes =
                canonicalElement
                    .DescendantNodes()
                    .OfType<XText>()
                    .Where(node =>
                        string.IsNullOrWhiteSpace(
                            node.Value))
                    .ToList();

            foreach (XText whitespaceNode
                in insignificantWhitespaceNodes)
            {
                whitespaceNode.Remove();
            }

            string canonicalXml =
                canonicalElement.ToString(
                    SaveOptions.DisableFormatting);

            byte[] canonicalBytes =
                Encoding.UTF8.GetBytes(canonicalXml);

            byte[] hashBytes =
                SHA256.HashData(canonicalBytes);

            return Convert
                .ToHexString(hashBytes)
                .ToLowerInvariant();
        }

        private static string? GetLastPropertyValue(IEnumerable<ParsedTracePropertyData> properties, string expectedKey)
        {
            return properties
                .LastOrDefault(property =>
                    string.Equals(
                        property.KeyRaw,
                        expectedKey,
                        StringComparison.Ordinal))
                ?.ValueRaw;
        }

        private static XElement? GetDirectElement(XElement parentElement, string localName)
        {
            return parentElement
                .Elements()
                .FirstOrDefault(element =>
                    HasLocalName(
                        element,
                        localName));
        }

        private static string? GetDirectElementValue(XElement parentElement, string localName)
        {
            return GetDirectElement(
                parentElement,
                localName)
                ?.Value;
        }

        private static bool HasLocalName(XElement element, string expectedLocalName)
        {
            return string.Equals(
                element.Name.LocalName,
                expectedLocalName,
                StringComparison.Ordinal);
        }

        private static bool HasLocalName(XmlReader xmlReader, string expectedLocalName)
        {
            return string.Equals(
                xmlReader.LocalName,
                expectedLocalName,
                StringComparison.Ordinal);
        }
    }
}