namespace BL.Models.Imports
{
    public sealed class XmlTraceParseResult
    {
        public XmlTraceParseResult(IReadOnlyList<ParsedTraceData> traces, IReadOnlyList<XmlTraceParseError> errors)
        {
            Traces = traces ?? throw new ArgumentNullException(nameof(traces));

            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public IReadOnlyList<ParsedTraceData> Traces { get; }

        public IReadOnlyList<XmlTraceParseError> Errors { get; }
    }
}