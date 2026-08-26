using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Imports
{
    public class ImportedTrace
    {
        private readonly List<ImportedTraceProperty> _properties = new();

        private ImportedTrace()
        {
        }

        public ImportedTrace(ImportedTraceCreationData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (data.Position <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(data.Position), "The trace position must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(data.RawXml))
            {
                throw new ArgumentException("The raw XML is required.",nameof(data.RawXml));
            }

            Position = data.Position;

            TraceTypeRaw = data.TraceTypeRaw;
            TraceType = data.TraceType;

            SourceRaw = data.SourceRaw;

            TechnicalTimeRaw = data.TechnicalTimeRaw;
            TechnicalTime = data.TechnicalTime;

            LatitudeRaw = data.LatitudeRaw;
            Latitude = data.Latitude;

            LongitudeRaw = data.LongitudeRaw;
            Longitude = data.Longitude;

            MileageRaw = data.MileageRaw;
            Mileage = data.Mileage;

            HeadingRaw = data.HeadingRaw;
            Heading = data.Heading;

            SpeedRaw = data.SpeedRaw;
            Speed = data.Speed;

            LinkId = data.LinkId;
            ActivityCode = data.ActivityCode;
            DriverIdsRaw = data.DriverIdsRaw;

            SequenceRaw = data.SequenceRaw;
            Sequence = data.Sequence;

            ActivityStartTimeRaw = data.ActivityStartTimeRaw;
            ActivityStartTime = data.ActivityStartTime;

            ActivityLengthMillisecondsRaw =
                data.ActivityLengthMillisecondsRaw;

            ActivityLengthMilliseconds =
                data.ActivityLengthMilliseconds;

            DrivingLengthMillisecondsRaw =
                data.DrivingLengthMillisecondsRaw;

            DrivingLengthMilliseconds =
                data.DrivingLengthMilliseconds;

            DeviceRaw = data.DeviceRaw;

            ActivityReportRaw = data.ActivityReportRaw;
            ActivityFinalReportRaw = data.ActivityFinalReportRaw;

            TraceHash = NormalizeAndValidateHash(data.TraceHash);
            RawXml = data.RawXml;
        }

        public int Id { get; private set; }

        public int ImportSourceFileId { get; private set; }

        public int Position { get; private set; }

        public string? TraceTypeRaw { get; private set; }

        public int? TraceType { get; private set; }

        public string? SourceRaw { get; private set; }

        public string? TechnicalTimeRaw { get; private set; }

        public DateTime? TechnicalTime { get; private set; }

        public string? LatitudeRaw { get; private set; }

        public decimal? Latitude { get; private set; }

        public string? LongitudeRaw { get; private set; }

        public decimal? Longitude { get; private set; }

        public string? MileageRaw { get; private set; }

        public long? Mileage { get; private set; }

        public string? HeadingRaw { get; private set; }

        public decimal? Heading { get; private set; }

        public string? SpeedRaw { get; private set; }

        public decimal? Speed { get; private set; }

        public string? LinkId { get; private set; }

        public string? ActivityCode { get; private set; }

        public string? DriverIdsRaw { get; private set; }

        public string? SequenceRaw { get; private set; }

        public long? Sequence { get; private set; }

        public string? ActivityStartTimeRaw { get; private set; }

        public DateTime? ActivityStartTime { get; private set; }

        public string? ActivityLengthMillisecondsRaw
        {
            get;
            private set;
        }

        public long? ActivityLengthMilliseconds
        {
            get;
            private set;
        }

        public string? DrivingLengthMillisecondsRaw
        {
            get;
            private set;
        }

        public long? DrivingLengthMilliseconds
        {
            get;
            private set;
        }

        public string? DeviceRaw { get; private set; }

        public string? ActivityReportRaw { get; private set; }

        public string? ActivityFinalReportRaw { get; private set; }

        public string TraceHash { get; private set; } = string.Empty;

        public string RawXml { get; private set; } = string.Empty;

        public ImportSourceFile ImportSourceFile
        {
            get;
            private set;
        } = null!;

        public IReadOnlyCollection<ImportedTraceProperty> Properties => _properties.AsReadOnly();

        public void AddProperty(int position, string? keyRaw, string? valueRaw)
        {
            if (ImportSourceFile is not null)
            {
                throw new InvalidOperationException("A property cannot be added after the trace " + "has been attached to a source file.");
            }

            int expectedPosition = _properties.Count + 1;

            if (position != expectedPosition)
            {
                throw new ArgumentOutOfRangeException(nameof(position), $"The next property position must be " + $"{expectedPosition}.");
            }

            ImportedTraceProperty property = new(position, keyRaw, valueRaw, this);

            _properties.Add(property);
        }

        internal void AttachToSourceFile(ImportSourceFile sourceFile)
        {
            ArgumentNullException.ThrowIfNull(sourceFile);

            if (ImportSourceFile is not null && !ReferenceEquals(ImportSourceFile, sourceFile))
            {
                throw new InvalidOperationException("The trace is already attached " + "to another source file.");
            }

            ImportSourceFile = sourceFile;
        }

        private static string NormalizeAndValidateHash(string traceHash)
        {
            if (string.IsNullOrWhiteSpace(traceHash))
            {
                throw new ArgumentException("The trace hash is required.", nameof(traceHash));
            }

            string normalizedHash = traceHash.Trim().ToLowerInvariant();

            bool containsInvalidCharacter = normalizedHash.Any
                (character =>!IsHexadecimalCharacter(character));

            if (normalizedHash.Length != 64 || containsInvalidCharacter)
            {
                throw new ArgumentException( "The trace hash must be a 64-character " + "SHA-256 hexadecimal value.", nameof(traceHash));
            }

            return normalizedHash;
        }

        private static bool IsHexadecimalCharacter(char character)
        {
            return character >= '0' && character <= '9' || character >= 'a' && character <= 'f';
        }
    }
}