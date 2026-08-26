using Domain.Imports;

namespace BL.Models.Imports
{
    public sealed class XmlTraceParseError
    {
        public string Code { get; init; } = string.Empty;

        public string Message { get; init; } = string.Empty;

        public int TracePosition { get; init; }

        public ImportErrorSeverity Severity { get; init; }

        public bool RejectsTrace { get; init; }
    }
}