using BL.Settings;
using BL.Interfaces.Services;
using Domain.Activities;
using Domain.Activities.Interfaces;

namespace BL.Services;

public sealed class ActivityReconstructionService
    : IActivityReconstructionService
{
    private readonly IActivityCodeMapper _codeMapper;
    private readonly ActivityReconstructionSettings _settings;

    public ActivityReconstructionService(IActivityCodeMapper codeMapper, ActivityReconstructionSettings settings)
    {
        _codeMapper = codeMapper;
        _settings = settings;
    }

    public IReadOnlyList<ReconstructedActivity> Reconstruct(
        IEnumerable<RawActivityTrace> traces)
    {
        ArgumentNullException.ThrowIfNull(traces);

        return traces
            .Where(trace =>
                !string.IsNullOrWhiteSpace(trace.ExternalActivityId))
            .GroupBy(trace => new ActivityKey(
                trace.RawSourceReference,
                trace.ExternalActivityId))
            .Select(ReconstructGroup)
            .OrderBy(activity => activity.StartTime ?? DateTime.MaxValue)
            .ThenBy(activity => activity.ExternalActivityId)
            .ToList();
    }

    private ReconstructedActivity ReconstructGroup(IGrouping<ActivityKey, RawActivityTrace> group)
    {
        var orderedTraces = group
            .OrderBy(trace => trace.TraceTime.HasValue ? 0 : 1)
            .ThenBy(trace => trace.TraceTime)
            .ThenBy(trace =>
                trace.ExternalSequenceNumber.HasValue ? 0 : 1)
            .ThenBy(trace => trace.ExternalSequenceNumber)
            .ThenBy(trace => trace.PositionInFile)
            .ToList();

        var anomalies = new List<ActivityAnomaly>();

        var hasOpeningTrace = orderedTraces.Any(trace => trace.RawTraceType == 10);

        var hasClosingTrace = orderedTraces.Any(trace => IsClosingTrace(trace.RawTraceType));

        if (!hasOpeningTrace)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.MissingOpeningTrace,
                "Opening trace 10 missing in the import.",
                false));
        }

        if (!hasClosingTrace)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.MissingClosingTrace,
                "No ending trace 11 or 13 present. The activity is open at the limit of the import, but not necessarily still active.",
                true));
        }

        var tracesWithStart = orderedTraces
            .Where(trace => trace.ActivityStartTime.HasValue)
            .ToList();

        var distinctStartTimes = tracesWithStart
            .Select(trace => trace.ActivityStartTime!.Value)
            .Distinct()
            .ToList();

        var startTime = tracesWithStart
            .LastOrDefault()
            ?.ActivityStartTime;

        if (!startTime.HasValue)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.MissingStartTime,
                "The AST property is missing.",
                true));
        }

        if (distinctStartTimes.Count > 1)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.ConflictingStartTime,
                "Multiple different AST values exist for the same LID.",
                true));
        }

        // Une durée portée par une clôture 11/13 est prioritaire.
        var durationTrace = orderedTraces.LastOrDefault(trace => IsClosingTrace(trace.RawTraceType) && trace.DurationMilliseconds.HasValue);

        if (durationTrace == null)
        {
            durationTrace = orderedTraces.LastOrDefault(trace => trace.DurationMilliseconds.HasValue);
        }

        var durationMilliseconds = durationTrace?.DurationMilliseconds;

        var distinctFinalDurations = orderedTraces
            .Where(trace => IsClosingTrace(trace.RawTraceType) && trace.DurationMilliseconds.HasValue)
            .Select(trace => trace.DurationMilliseconds!.Value)
            .Distinct()
            .ToList();

        if (hasClosingTrace && !durationMilliseconds.HasValue)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.MissingDuration,
                "Activity has a closing trace but no duration is available.",
                true));
        }

        if (durationMilliseconds < 0)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.NonPositiveDuration,
                "The ALEN duration is negative.",
                true));
        }

        if (hasClosingTrace && durationMilliseconds == 0)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.NonPositiveDuration,
                "The activity is closed with a zero duration.",
                true));
        }

        if (durationMilliseconds >
            _settings.ExcessiveDurationThreshold.TotalMilliseconds)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.ExcessiveDuration,
                $"The duration exceeds the threshold of {_settings.ExcessiveDurationThreshold.TotalHours:N0} hours.",
                true));
        }

        if (distinctFinalDurations.Count > 1)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.ConflictingFinalDuration,
                "Multiple closing traces provide different ALEN durations.",
                true));
        }

        var tracesWithCode = orderedTraces
            .Where(trace => !string.IsNullOrWhiteSpace(trace.RawActivityCode))
            .ToList();

        var observedCodes = tracesWithCode
            .Select(trace => trace.RawActivityCode!.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var selectedCodeTrace = tracesWithCode
            .LastOrDefault(trace => IsClosingTrace(trace.RawTraceType))
            ?? tracesWithCode.LastOrDefault();

        var rawActivityCode = selectedCodeTrace?.RawActivityCode?.Trim();
        var activityKind = _codeMapper.Map(rawActivityCode);

        if (observedCodes.Count > 1)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.ActivityCodeChanged,
                "The activity code has changed during the lifecycle of the same LID.",
                false));
        }

        if (activityKind == ActivityKind.Unmapped)
        {
            anomalies.Add(new ActivityAnomaly(
                ActivityAnomalyCode.UnmappedActivityCode,
                $"The activity code « {rawActivityCode ?? "(empty)"} » is not yet associated with an internal type.",
                true));
        }

        var externalDriverIds = orderedTraces
            .SelectMany(trace => (trace.RawExternalDriverIds ?? string.Empty)
                .Split(
                    ';',
                    StringSplitOptions.RemoveEmptyEntries
                    | StringSplitOptions.TrimEntries))
            .Distinct(StringComparer.Ordinal)
            .ToList();

        DateTime? calculatedEndTime = null;

        if (startTime.HasValue && durationMilliseconds is >= 0)
        {
            try
            {
                var durationTicks = checked(
                    durationMilliseconds.Value
                    * TimeSpan.TicksPerMillisecond);

                calculatedEndTime = startTime.Value.AddTicks(durationTicks);
            }
            catch (Exception exception)
                when (exception is OverflowException
                    or ArgumentOutOfRangeException)
            {
                anomalies.Add(new ActivityAnomaly(
                    ActivityAnomalyCode.DurationOverflow,
                    "The ALEN duration produces an impossible end date.",
                    true));
            }
        }

        var lifecycleState = hasClosingTrace
            ? ActivityLifecycleState.Closed
            : ActivityLifecycleState.OpenAtImportBoundary;

        var candidateStatus = DetermineStatus(
            activityKind,
            lifecycleState,
            anomalies);

        return new ReconstructedActivity
        {
            ExternalActivityId = group.Key.ExternalActivityId,
            RawSourceReference = group.Key.SourceReference,
            RawActivityCode = rawActivityCode,
            ObservedRawActivityCodes = observedCodes,
            Kind = activityKind,
            StartTime = startTime,
            CalculatedEndTime = calculatedEndTime,
            DurationMilliseconds = durationMilliseconds,
            ExternalDriverIds = externalDriverIds,
            LifecycleState = lifecycleState,
            CandidateStatus = candidateStatus,
            Anomalies = anomalies,
            SourceTraces = orderedTraces
        };
    }

    private static ActivityCandidateStatus DetermineStatus(
        ActivityKind kind,
        ActivityLifecycleState lifecycleState,
        IReadOnlyCollection<ActivityAnomaly> anomalies)
    {
        var structurallyInvalid = anomalies.Any(anomaly =>
            anomaly.Code is
                ActivityAnomalyCode.MissingStartTime
                or ActivityAnomalyCode.MissingDuration
                or ActivityAnomalyCode.NonPositiveDuration
                or ActivityAnomalyCode.DurationOverflow
                or ActivityAnomalyCode.ConflictingStartTime
                or ActivityAnomalyCode.ConflictingFinalDuration);

        if (structurallyInvalid)
            return ActivityCandidateStatus.Invalid;

        if (kind == ActivityKind.Unknown)
            return ActivityCandidateStatus.Unknown;

        if (lifecycleState == ActivityLifecycleState.OpenAtImportBoundary
            || kind == ActivityKind.Unmapped
            || anomalies.Count > 0)
        {
            return ActivityCandidateStatus.PendingReview;
        }

        return ActivityCandidateStatus.Recognized;
    }

    private static bool IsClosingTrace(int traceType)
    {
        return traceType is 11 or 13;
    }

    private readonly record struct ActivityKey(
        string SourceReference,
        string ExternalActivityId);
}