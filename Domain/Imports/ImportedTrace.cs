namespace Domain.Imports
{
    public class ImportedTrace
    {
        public int Id { get; set; }

        public int ImportSourceFileId { get; set; }

        public int Position { get; set; }

        public string? TraceTypeRaw { get; set; }

        public int? TraceType { get; set; }

        public string? SourceRaw { get; set; }

        public string? TechnicalTimeRaw { get; set; }

        public DateTime? TechnicalTime { get; set; }

        public string? LatitudeRaw { get; set; }

        public decimal? Latitude { get; set; }

        public string? LongitudeRaw { get; set; }

        public decimal? Longitude { get; set; }

        public string? MileageRaw { get; set; }

        public long? Mileage { get; set; }

        public string? HeadingRaw { get; set; }

        public decimal? Heading { get; set; }

        public string? SpeedRaw { get; set; }

        public decimal? Speed { get; set; }

        public string? LinkId { get; set; }

        public string? ActivityCode { get; set; }

        public string? DriverIdsRaw { get; set; }

        public string? SequenceRaw { get; set; }

        public long? Sequence { get; set; }

        public string? ActivityStartTimeRaw { get; set; }

        public DateTime? ActivityStartTime { get; set; }

        public string? ActivityLengthMillisecondsRaw { get; set; }

        public long? ActivityLengthMilliseconds { get; set; }

        public string? DrivingLengthMillisecondsRaw { get; set; }

        public long? DrivingLengthMilliseconds { get; set; }

        public string? DeviceRaw { get; set; }

        public string? ActivityReportRaw { get; set; }

        public string? ActivityFinalReportRaw { get; set; }

        public string TraceHash { get; set; } = string.Empty;

        public string RawXml { get; set; } = string.Empty;

        public ImportSourceFile ImportSourceFile { get; set; } = null!;

        public ICollection<ImportedTraceProperty> Properties { get; set; } = new List<ImportedTraceProperty>();
    }
}