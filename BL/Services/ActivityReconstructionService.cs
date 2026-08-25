using BL.Interfaces.Services;
using Domain.Activities;

namespace BL.Services
{
    public class ActivityReconstructionService : IActivityReconstructionService
    {
        private readonly IActivityCodeMapper _activityCodeMapper;
        private readonly IActivityAnomalyDetector _anomalyDetector;

        public ActivityReconstructionService(IActivityCodeMapper activityCodeMapper, IActivityAnomalyDetector anomalyDetector)
        {
            _activityCodeMapper = activityCodeMapper ?? throw new ArgumentNullException(nameof(activityCodeMapper));

            _anomalyDetector = anomalyDetector ?? throw new ArgumentNullException(nameof(anomalyDetector));
        }

        public IReadOnlyList<ReconstructedActivity> Reconstruct(IEnumerable<RawActivityTrace> rawTraces)
        {
            List<RawActivityTrace> traces = ValidateAndMaterialize(rawTraces);

            IReadOnlyList<RawActivityTrace> activityTraces = FindActivityTraces(traces);

            IReadOnlyList<ActivityTraceGroup> traceGroups = GroupTraces(activityTraces);

            List<ReconstructedActivity> reconstructedActivities = new();

            foreach (ActivityTraceGroup traceGroup in traceGroups)
            {
                ReconstructedActivity reconstructedActivity = ReconstructGroup(traceGroup);

                reconstructedActivities.Add(reconstructedActivity);
            }

            return reconstructedActivities.AsReadOnly();
        }

        private static List<RawActivityTrace> ValidateAndMaterialize(IEnumerable<RawActivityTrace> rawTraces)
        {
            ArgumentNullException.ThrowIfNull(rawTraces);

            List<RawActivityTrace> traces = rawTraces.ToList();

            foreach (RawActivityTrace trace in traces)
            {
                if (trace is null)
                {
                    throw new ArgumentException(
                        "Collection contains a null trace.",
                        nameof(rawTraces));
                }

                bool isActivityTrace = ActivityTraceTypes.IsActivityTrace(trace.TraceType);

                if (!isActivityTrace)
                {
                    continue;
                }

                ValidateActivityTraceIdentifiers(trace);
            }

            return traces;
        }

        private static void ValidateActivityTraceIdentifiers(RawActivityTrace trace)
        {
            if (string.IsNullOrWhiteSpace(trace.SourceId))
            {
                throw new ArgumentException("Activity trace must have a source ID.");
            }

            if (string.IsNullOrWhiteSpace(trace.LinkId))
            {
                throw new ArgumentException(
                    "Activity trace must have a link ID.");
            }
        }

        private static IReadOnlyList<RawActivityTrace> FindActivityTraces(IEnumerable<RawActivityTrace> traces)
        {
            List<RawActivityTrace> activityTraces = traces
                .Where(
                    trace => ActivityTraceTypes.IsActivityTrace(trace.TraceType))
                .ToList();

            return activityTraces.AsReadOnly();
        }

        private static IReadOnlyList<ActivityTraceGroup> GroupTraces(IEnumerable<RawActivityTrace> traces)
        {
            List<ActivityTraceGroup> groups = traces
                .GroupBy(
                    trace => new
                    {
                        SourceId = trace.SourceId.Trim(),
                        LinkId = trace.LinkId!.Trim()
                    })
                .OrderBy(group => group.Key.SourceId)
                .ThenBy(group => group.Key.LinkId)
                .Select(
                    group => new ActivityTraceGroup(
                        group.Key.SourceId,
                        group.Key.LinkId,
                        group))
                .ToList();

            return groups.AsReadOnly();
        }

        private ReconstructedActivity ReconstructGroup(ActivityTraceGroup traceGroup)
        {
            IReadOnlyList<RawActivityTrace> orderedTraces = OrderTraces(traceGroup.Traces);

            RawActivityTrace? openingTrace = FindOpeningTrace(orderedTraces);

            RawActivityTrace? closingTrace = FindClosingTrace(orderedTraces);

            string? rawActivityCode = FindLastKnownActivityCode(orderedTraces);

            ActivityKind activityKind = _activityCodeMapper.Map(rawActivityCode);

            DateTime? startTime = DetermineStartTime(openingTrace, orderedTraces);

            long? durationMilliseconds = DetermineDuration(closingTrace);

            DateTime? endTime = DetermineEndTime(startTime, durationMilliseconds);

            IReadOnlyList<string> driverIds = ExtractDriverIds(orderedTraces);

            ActivityLifecycleState lifecycleState = DetermineLifecycleState(closingTrace);

            ReconstructedActivity candidate =
                CreateCandidate(
                    traceGroup,
                    orderedTraces,
                    rawActivityCode,
                    activityKind,
                    startTime,
                    endTime,
                    durationMilliseconds,
                    driverIds,
                    lifecycleState);

            IReadOnlyList<ActivityAnomaly> anomalies = _anomalyDetector.Detect(candidate);

            ActivityCandidateStatus candidateStatus = DetermineCandidateStatus(candidate, anomalies);

            candidate.ApplyAnalysis(candidateStatus, anomalies);

            return candidate;
        }

        private static IReadOnlyList<RawActivityTrace> OrderTraces(IEnumerable<RawActivityTrace> traces)
        {
            List<RawActivityTrace> orderedTraces = traces
                .OrderBy(
                    trace => trace.Sequence ?? long.MaxValue)
                .ThenBy(trace => trace.TechnicalTime)
                .ThenBy(trace => trace.TraceType)
                .ToList();

            return orderedTraces.AsReadOnly();
        }

        private static RawActivityTrace? FindOpeningTrace(IEnumerable<RawActivityTrace> traces)
        {
            return traces.FirstOrDefault(
                trace => trace.TraceType
                    == ActivityTraceTypes.Opening);
        }

        private static RawActivityTrace? FindClosingTrace(IEnumerable<RawActivityTrace> traces)
        {
            RawActivityTrace? validatedClosingTrace = traces
                .LastOrDefault(
                    trace => trace.TraceType
                        == ActivityTraceTypes.ValidatedClosing);

            if (validatedClosingTrace is not null)
            {
                return validatedClosingTrace;
            }

            RawActivityTrace? closingTrace = traces
                .LastOrDefault(
                    trace => trace.TraceType
                        == ActivityTraceTypes.Closing);

            return closingTrace;
        }

        private static string? FindLastKnownActivityCode(IReadOnlyList<RawActivityTrace> traces)
        {
            for (int index = traces.Count - 1; index >= 0; index--)
            {
                string? activityCode = traces[index].ActivityCode;

                if (!string.IsNullOrWhiteSpace(activityCode))
                {
                    return activityCode.Trim();
                }
            }

            return null;
        }

        private static DateTime? DetermineStartTime(RawActivityTrace? openingTrace, IEnumerable<RawActivityTrace> traces)
        {
            if (openingTrace?.ActivityStartTime is not null)
            {
                return openingTrace.ActivityStartTime.Value;
            }

            foreach (RawActivityTrace trace in traces)
            {
                if (trace.ActivityStartTime.HasValue)
                {
                    return trace.ActivityStartTime.Value;
                }
            }

            return null;
        }

        private static long? DetermineDuration(RawActivityTrace? closingTrace)
        {
            if (closingTrace is null)
            {
                return null;
            }

            return closingTrace.ActivityLengthMilliseconds;
        }

        private static DateTime? DetermineEndTime(DateTime? startTime, long? durationMilliseconds)
        {
            if (!startTime.HasValue)
            {
                return null;
            }

            if (!durationMilliseconds.HasValue)
            {
                return null;
            }

            if (durationMilliseconds.Value < 0)
            {
                return null;
            }

            try
            {
                return startTime.Value.AddMilliseconds(durationMilliseconds.Value);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        private static IReadOnlyList<string> ExtractDriverIds(IEnumerable<RawActivityTrace> traces)
        {
            HashSet<string> knownDriverIds = new(StringComparer.OrdinalIgnoreCase);

            List<string> driverIds = new();

            foreach (RawActivityTrace trace in traces)
            {
                if (string.IsNullOrWhiteSpace(trace.DriverId))
                {
                    continue;
                }

                string[] traceDriverIds = trace.DriverId.Split(
                    ';',
                    StringSplitOptions.RemoveEmptyEntries
                    | StringSplitOptions.TrimEntries);

                foreach (string driverId in traceDriverIds)
                {
                    bool isNewDriver = knownDriverIds.Add(driverId);

                    if (isNewDriver)
                    {
                        driverIds.Add(driverId);
                    }
                }
            }

            return driverIds.AsReadOnly();
        }

        private static ActivityLifecycleState DetermineLifecycleState(
            RawActivityTrace? closingTrace)
        {
            if (closingTrace is null)
            {
                return ActivityLifecycleState.OpenAtImportBoundary;
            }

            return ActivityLifecycleState.Complete;
        }

        private static ReconstructedActivity CreateCandidate(
            ActivityTraceGroup traceGroup,
            IReadOnlyList<RawActivityTrace> orderedTraces,
            string? rawActivityCode,
            ActivityKind activityKind,
            DateTime? startTime,
            DateTime? endTime,
            long? durationMilliseconds,
            IReadOnlyList<string> driverIds,
            ActivityLifecycleState lifecycleState)
        {
            return new ReconstructedActivity
            {
                SourceId = traceGroup.SourceId,
                LinkId = traceGroup.LinkId,
                RawActivityCode = rawActivityCode,
                ActivityKind = activityKind,
                StartTime = startTime,
                EndTime = endTime,
                DurationMilliseconds = durationMilliseconds,
                DriverIds = driverIds,
                LifecycleState = lifecycleState,
                SourceTraces = orderedTraces
            };
        }

        private static ActivityCandidateStatus DetermineCandidateStatus(ReconstructedActivity candidate, IEnumerable<ActivityAnomaly> anomalies)
        {
            if (candidate.ActivityKind == ActivityKind.Unmapped)
            {
                return ActivityCandidateStatus.Unmapped;
            }

            if (candidate.LifecycleState
                == ActivityLifecycleState.OpenAtImportBoundary)
            {
                return ActivityCandidateStatus.RequiresReview;
            }

            bool manualReviewIsRequired = anomalies.Any(
                anomaly => anomaly.RequiresManualReview);

            if (manualReviewIsRequired)
            {
                return ActivityCandidateStatus.RequiresReview;
            }

            return ActivityCandidateStatus.Recognized;
        }
    }
}