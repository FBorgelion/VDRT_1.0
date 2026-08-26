namespace Domain.Imports
{
    public sealed class ImportedTraceCreationData
    {
        public int Position { get; init; }

        public string? TraceTypeRaw { get; init; }

        public int? TraceType { get; init; }

        public string? SourceRaw { get; init; }

        public string? TechnicalTimeRaw { get; init; }

        public DateTime? TechnicalTime { get; init; }

        public string? LatitudeRaw { get; init; }

        public decimal? Latitude { get; init; }

        public string? LongitudeRaw { get; init; }

        public decimal? Longitude { get; init; }

        public string? MileageRaw { get; init; }

        public long? Mileage { get; init; }

        public string? HeadingRaw { get; init; }

        public decimal? Heading { get; init; }

        public string? SpeedRaw { get; init; }

        public decimal? Speed { get; init; }

        public string? LinkId { get; init; }

        public string? ActivityCode { get; init; }

        public string? DriverIdsRaw { get; init; }

        public string? SequenceRaw { get; init; }

        public long? Sequence { get; init; }

        public string? ActivityStartTimeRaw { get; init; }

        public DateTime? ActivityStartTime { get; init; }

        public string? ActivityLengthMillisecondsRaw { get; init; }

        public long? ActivityLengthMilliseconds { get; init; }

        public string? DrivingLengthMillisecondsRaw { get; init; }

        public long? DrivingLengthMilliseconds { get; init; }

        public string? DeviceRaw { get; init; }

        public string? ActivityReportRaw { get; init; }

        public string? ActivityFinalReportRaw { get; init; }

        public string TraceHash { get; init; } = string.Empty;

        public string RawXml { get; init; } = string.Empty;
    }
}