using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public sealed class ReconstructedActivity
    {
        public required string ExternalActivityId { get; init; }

        public required string RawSourceReference { get; init; }

        public string? RawActivityCode { get; init; }

        public required IReadOnlyList<string> ObservedRawActivityCodes { get; init; }

        public required ActivityKind Kind { get; init; }

        public DateTime? StartTime { get; init; }

        /// <summary>
        /// Résultat de AST + ALEN.
        /// Il ne provient jamais de time.
        /// </summary>
        public DateTime? CalculatedEndTime { get; init; }

        public long? DurationMilliseconds { get; init; }

        public TimeSpan? Duration =>
            DurationMilliseconds is >= 0
                ? TimeSpan.FromMilliseconds(DurationMilliseconds.Value)
                : null;

        public required IReadOnlyList<string> ExternalDriverIds { get; init; }

        public required ActivityLifecycleState LifecycleState { get; init; }

        public required ActivityCandidateStatus CandidateStatus { get; init; }

        public required IReadOnlyList<ActivityAnomaly> Anomalies { get; init; }

        public required IReadOnlyList<RawActivityTrace> SourceTraces { get; init; }

        /// <summary>
        /// Signifie uniquement que la reconstruction est techniquement cohérente.
        /// Cela ne remplace pas la validation métier avant le calcul du coût.
        /// </summary>
        public bool IsStructurallyComplete =>
            LifecycleState == ActivityLifecycleState.Closed
            && CandidateStatus == ActivityCandidateStatus.Recognized
            && Anomalies.All(anomaly => !anomaly.BlocksProcessing);
    }
}
