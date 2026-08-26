using BL.Models.Imports;
using Domain.Imports;

namespace BL.Mappers.Imports
{
    public sealed class ImportedTraceMapper
    {
        public ImportedTrace Map(ParsedTraceData parsedTrace)
        {
            ArgumentNullException.ThrowIfNull(parsedTrace);
            ArgumentNullException.ThrowIfNull(parsedTrace.Properties);

            ImportedTraceCreationData creationData = new()
            {
                Position = parsedTrace.Position,

                TraceTypeRaw = parsedTrace.TraceTypeRaw,
                TraceType = parsedTrace.TraceType,

                SourceRaw = parsedTrace.SourceRaw,

                TechnicalTimeRaw = parsedTrace.TechnicalTimeRaw,
                TechnicalTime = parsedTrace.TechnicalTime,

                LatitudeRaw = parsedTrace.LatitudeRaw,
                Latitude = parsedTrace.Latitude,

                LongitudeRaw = parsedTrace.LongitudeRaw,
                Longitude = parsedTrace.Longitude,

                MileageRaw = parsedTrace.MileageRaw,
                Mileage = parsedTrace.Mileage,

                HeadingRaw = parsedTrace.HeadingRaw,
                Heading = parsedTrace.Heading,

                SpeedRaw = parsedTrace.SpeedRaw,
                Speed = parsedTrace.Speed,

                LinkId = parsedTrace.LinkId,
                ActivityCode = parsedTrace.ActivityCode,
                DriverIdsRaw = parsedTrace.DriverIdsRaw,

                SequenceRaw = parsedTrace.SequenceRaw,
                Sequence = parsedTrace.Sequence,

                ActivityStartTimeRaw =
                    parsedTrace.ActivityStartTimeRaw,

                ActivityStartTime =
                    parsedTrace.ActivityStartTime,

                ActivityLengthMillisecondsRaw =
                    parsedTrace.ActivityLengthMillisecondsRaw,

                ActivityLengthMilliseconds =
                    parsedTrace.ActivityLengthMilliseconds,

                DrivingLengthMillisecondsRaw =
                    parsedTrace.DrivingLengthMillisecondsRaw,

                DrivingLengthMilliseconds =
                    parsedTrace.DrivingLengthMilliseconds,

                DeviceRaw = parsedTrace.DeviceRaw,

                ActivityReportRaw =
                    parsedTrace.ActivityReportRaw,

                ActivityFinalReportRaw =
                    parsedTrace.ActivityFinalReportRaw,

                TraceHash = parsedTrace.TraceHash,
                RawXml = parsedTrace.RawXml
            };

            ImportedTrace importedTrace = new(creationData);

            foreach (ParsedTracePropertyData property
                in parsedTrace.Properties)
            {
                importedTrace.AddProperty(
                    property.Position,
                    property.KeyRaw,
                    property.ValueRaw);
            }

            return importedTrace;
        }
    }
}