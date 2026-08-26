using System;

namespace Domain.Imports
{
    public class ImportedTraceProperty
    {
        private ImportedTraceProperty()
        {
        }

        internal ImportedTraceProperty(int position, string? keyRaw, string? valueRaw, ImportedTrace importedTrace)
        {
            if (position <= 0)
            {
                throw new ArgumentOutOfRangeException( nameof(position), "The property position must be greater than zero.");
            }

            ArgumentNullException.ThrowIfNull(importedTrace);

            Position = position;
            KeyRaw = keyRaw;
            ValueRaw = valueRaw;
            ImportedTrace = importedTrace;
        }

        public int Id { get; private set; }

        public int ImportedTraceId { get; private set; }

        public int Position { get; private set; }

        public string? KeyRaw { get; private set; }

        public string? ValueRaw { get; private set; }

        public ImportedTrace ImportedTrace
        {
            get;
            private set;
        } = null!;
    }
}