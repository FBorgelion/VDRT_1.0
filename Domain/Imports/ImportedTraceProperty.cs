namespace Domain.Imports
{
    public class ImportedTraceProperty
    {
        public int Id { get; set; }

        public int ImportedTraceId { get; set; }

        public int Position { get; set; }

        public string? KeyRaw { get; set; }

        public string? ValueRaw { get; set; }

        public ImportedTrace ImportedTrace { get; set; } = null!;
    }
}