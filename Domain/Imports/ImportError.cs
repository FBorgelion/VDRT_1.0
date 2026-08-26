namespace Domain.Imports
{
    public class ImportError
    {
        public int Id { get; set; }

        public int ImportBatchId { get; set; }

        public int? ImportSourceFileId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string? FileName { get; set; }

        public int? TracePosition { get; set; }

        public ImportErrorSeverity Severity { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public ImportBatch ImportBatch { get; set; } = null!;

        public ImportSourceFile? ImportSourceFile { get; set; }
    }
}