namespace Domain.Activities
{
    public class ReconstructedActivity
    {
        public string SourceId { get; init; } = string.Empty;

        public string LinkId { get; init; } = string.Empty;

        public string? RawActivityCode { get; init; }

        public ActivityKind ActivityKind { get; init; }

        public DateTime? StartTime { get; init; }

        public DateTime? EndTime { get; init; }

        public long? DurationMilliseconds { get; init; }

        public IReadOnlyList<string> DriverIds { get; init; }
            = Array.Empty<string>();

        public ActivityLifecycleState LifecycleState { get; init; }

        public ActivityCandidateStatus CandidateStatus { get; private set; }
            = ActivityCandidateStatus.RequiresReview;

        public IReadOnlyList<ActivityAnomaly> Anomalies { get; private set; }
            = Array.Empty<ActivityAnomaly>();

        public IReadOnlyList<RawActivityTrace> SourceTraces { get; init; }
            = Array.Empty<RawActivityTrace>();

        public void ApplyAnalysis(ActivityCandidateStatus candidateStatus, IEnumerable<ActivityAnomaly> anomalies)
        {
            ArgumentNullException.ThrowIfNull(anomalies);

            CandidateStatus = candidateStatus;
            Anomalies = anomalies.ToList().AsReadOnly();
        }
    }
}