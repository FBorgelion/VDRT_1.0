using BL.Mappers.Imports;
using BL.Models.Imports;
using Domain.Imports;

namespace BL.Tests.Mappers.Imports
{
    public class ImportedTraceMapperTests
    {
        private readonly ImportedTraceMapper _mapper = new();

        [Fact]
        public void Map_ShouldCopyValuesAndBuildRelationships()
        {
            DateTime technicalTime = new(
                2026,
                8,
                26,
                8,
                30,
                0,
                DateTimeKind.Utc);

            DateTime activityStartTime = new(
                2026,
                8,
                26,
                8,
                0,
                0,
                DateTimeKind.Unspecified);

            ParsedTraceData parsedTrace = new()
            {
                Position = 1,

                TraceTypeRaw = "10",
                TraceType = 10,

                SourceRaw = "vehicle-42",

                TechnicalTimeRaw =
                    "2026-08-26T08:30:00Z",

                TechnicalTime = technicalTime,

                LatitudeRaw = "50.8503",
                Latitude = 50.8503m,

                LongitudeRaw = "4.3517",
                Longitude = 4.3517m,

                MileageRaw = "125000",
                Mileage = 125000,

                HeadingRaw = "180.5",
                Heading = 180.5m,

                SpeedRaw = "72.4",
                Speed = 72.4m,

                LinkId = "activity-123",
                ActivityCode = "DR",
                DriverIdsRaw = "787;325",

                SequenceRaw = "15",
                Sequence = 15,

                ActivityStartTimeRaw =
                    "2026-08-26T08:00:00",

                ActivityStartTime = activityStartTime,

                ActivityLengthMillisecondsRaw =
                    "1800000",

                ActivityLengthMilliseconds =
                    1_800_000,

                DrivingLengthMillisecondsRaw =
                    "1700000",

                DrivingLengthMilliseconds =
                    1_700_000,

                DeviceRaw = "CarCube",

                ActivityReportRaw = "report",
                ActivityFinalReportRaw = "final-report",

                TraceHash = new string('a', 64),
                RawXml = "<trace><type>10</type></trace>",

                Properties = new[]
                {
                    new ParsedTracePropertyData
                    {
                        Position = 1,
                        KeyRaw = "LID",
                        ValueRaw = "activity-123"
                    },
                    new ParsedTracePropertyData
                    {
                        Position = 2,
                        KeyRaw = "DID",
                        ValueRaw = "787;325"
                    }
                }
            };

            ImportedTrace importedTrace =
                _mapper.Map(parsedTrace);

            Assert.Equal(1, importedTrace.Position);

            Assert.Equal("10", importedTrace.TraceTypeRaw);
            Assert.Equal(10, importedTrace.TraceType);

            Assert.Equal(
                "vehicle-42",
                importedTrace.SourceRaw);

            Assert.Equal(
                technicalTime,
                importedTrace.TechnicalTime);

            Assert.Equal(
                (decimal?)50.8503m,
                importedTrace.Latitude);

            Assert.Equal(
                (decimal?)4.3517m,
                importedTrace.Longitude);

            Assert.Equal(
                (long?)125000,
                importedTrace.Mileage);

            Assert.Equal(
                (decimal?)180.5m,
                importedTrace.Heading);

            Assert.Equal(
                (decimal?)72.4m,
                importedTrace.Speed);

            Assert.Equal(
                "activity-123",
                importedTrace.LinkId);

            Assert.Equal(
                "DR",
                importedTrace.ActivityCode);

            Assert.Equal(
                "787;325",
                importedTrace.DriverIdsRaw);

            Assert.Equal(
                (long?)15,
                importedTrace.Sequence);

            Assert.Equal(
                activityStartTime,
                importedTrace.ActivityStartTime);

            Assert.Equal(
                (long?)1_800_000,
                importedTrace.ActivityLengthMilliseconds);

            Assert.Equal(
                (long?)1_700_000,
                importedTrace.DrivingLengthMilliseconds);

            Assert.Equal(
                new string('a', 64),
                importedTrace.TraceHash);

            Assert.Collection(
                importedTrace.Properties,
                firstProperty =>
                {
                    Assert.Equal(1, firstProperty.Position);
                    Assert.Equal("LID", firstProperty.KeyRaw);

                    Assert.Equal(
                        "activity-123",
                        firstProperty.ValueRaw);

                    Assert.Same(
                        importedTrace,
                        firstProperty.ImportedTrace);
                },
                secondProperty =>
                {
                    Assert.Equal(2, secondProperty.Position);
                    Assert.Equal("DID", secondProperty.KeyRaw);

                    Assert.Equal(
                        "787;325",
                        secondProperty.ValueRaw);

                    Assert.Same(
                        importedTrace,
                        secondProperty.ImportedTrace);
                });

            ImportSourceFile sourceFile = new(
                "sample.xml",
                500,
                new string('b', 64));

            sourceFile.AddTrace(importedTrace);

            Assert.Same(
                sourceFile,
                importedTrace.ImportSourceFile);

            Assert.Same(
                importedTrace,
                Assert.Single(sourceFile.Traces));
        }

        [Fact]
        public void Map_ShouldRejectNullData()
        {
            Assert.Throws<ArgumentNullException>(
                () => _mapper.Map(null!));
        }

        [Fact]
        public void Map_ShouldRejectInvalidHash()
        {
            ParsedTraceData parsedTrace = new()
            {
                Position = 1,
                TraceHash = "invalid-hash",
                RawXml = "<trace />"
            };

            Assert.Throws<ArgumentException>(
                () => _mapper.Map(parsedTrace));
        }
    }
}