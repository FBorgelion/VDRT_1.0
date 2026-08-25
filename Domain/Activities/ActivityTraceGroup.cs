namespace Domain.Activities
{
    public class ActivityTraceGroup
    {
        public ActivityTraceGroup(string sourceId, string linkId, IEnumerable<RawActivityTrace> traces)
        {
            if (string.IsNullOrWhiteSpace(sourceId))
            {
                throw new ArgumentException(
                    "Source is required.",
                    nameof(sourceId));
            }

            if (string.IsNullOrWhiteSpace(linkId))
            {
                throw new ArgumentException(
                    "Link ID is required.",
                    nameof(linkId));
            }

            ArgumentNullException.ThrowIfNull(traces);

            SourceId = sourceId;
            LinkId = linkId;
            Traces = traces.ToList().AsReadOnly();
        }

        public string SourceId { get; }

        public string LinkId { get; }

        public IReadOnlyList<RawActivityTrace> Traces { get; }
    }
}