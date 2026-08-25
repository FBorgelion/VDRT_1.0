using BL.Interfaces.Services;
using BL.Settings;
using Domain.Activities;

namespace BL.Services
{
    public class ActivityAnomalyDetector : IActivityAnomalyDetector
    {
        private readonly ActivityReconstructionSettings _settings;

        public ActivityAnomalyDetector(ActivityReconstructionSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public IReadOnlyList<ActivityAnomaly> Detect(ReconstructedActivity activity)
        {
            ArgumentNullException.ThrowIfNull(activity);

            List<ActivityAnomaly> anomalies = new();

            DetectMissingOpeningTrace(activity, anomalies);
            DetectMissingClosingTrace(activity, anomalies);
            DetectActivityCodeAnomaly(activity, anomalies);
            DetectMissingStartTime(activity, anomalies);
            DetectMissingDuration(activity, anomalies);
            DetectInvalidDuration(activity, anomalies);
            DetectEndTimeCalculationFailure(activity, anomalies);
            DetectExcessiveDuration(activity, anomalies);
            DetectDriverAnomalies(activity, anomalies);

            return anomalies.AsReadOnly();
        }

        private static void DetectMissingOpeningTrace(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            bool hasOpeningTrace = activity.SourceTraces.Any(
                trace => trace.TraceType == ActivityTraceTypes.Opening);

            if (hasOpeningTrace)
            {
                return;
            }

            anomalies.Add(new ActivityAnomaly(
                    ActivityAnomalyCode.MissingOpeningTrace,
                    "No opening trace 10 found.",
                    true));
        }

        private static void DetectMissingClosingTrace(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            if (activity.LifecycleState
                != ActivityLifecycleState.OpenAtImportBoundary)
            {
                return;
            }

            anomalies.Add(new ActivityAnomaly(
                    ActivityAnomalyCode.MissingClosingTrace,
                    "No closing trace 11 or 13 available.",
                    false));
        }

        private static void DetectActivityCodeAnomaly(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            if (string.IsNullOrWhiteSpace(activity.RawActivityCode))
            {
                anomalies.Add(new ActivityAnomaly(
                        ActivityAnomalyCode.MissingActivityCode,
                        "The trace does not contain any ATY activity code.",
                        true));

                return;
            }

            if (activity.ActivityKind == ActivityKind.Unknown)
            {
                anomalies.Add(new ActivityAnomaly(
                        ActivityAnomalyCode.UnknownActivityCode,
                        "The Fleetworks code is UN and requires manual interpretation.",
                        true));

                return;
            }

            if (activity.ActivityKind == ActivityKind.Unmapped)
            {
                anomalies.Add(new ActivityAnomaly(
                        ActivityAnomalyCode.UnmappedActivityCode,
                        $"The code '{activity.RawActivityCode}' is not yet mapped.",
                        true));
            }
        }

        private static void DetectMissingStartTime(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            if (activity.StartTime.HasValue)
            {
                return;
            }

            anomalies.Add(new ActivityAnomaly(
                    ActivityAnomalyCode.MissingStartTime,
                    "No usable AST value found.",
                    true));
        }

        private static void DetectMissingDuration(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            bool activityIsComplete = activity.LifecycleState == ActivityLifecycleState.Complete;

            if (!activityIsComplete)
            {
                return;
            }

            if (activity.DurationMilliseconds.HasValue)
            {
                return;
            }

            anomalies.Add(new ActivityAnomaly(
                    ActivityAnomalyCode.MissingDuration,
                    "The activity is complete, but the closure trace does not contain ALEN.",
                    true));
        }

        private static void DetectInvalidDuration(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            if (!activity.DurationMilliseconds.HasValue)
            {
                return;
            }

            if (activity.DurationMilliseconds.Value >= 0)
            {
                return;
            }

            anomalies.Add(
                new ActivityAnomaly(
                    ActivityAnomalyCode.InvalidDuration,
                    "The duration ALEN cannot be negative.",
                    true));
        }

        private static void DetectEndTimeCalculationFailure(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            if (activity.LifecycleState
                != ActivityLifecycleState.Complete)
            {
                return;
            }

            if (!activity.StartTime.HasValue
                || !activity.DurationMilliseconds.HasValue)
            {
                return;
            }

            if (activity.DurationMilliseconds.Value < 0)
            {
                return;
            }

            if (activity.EndTime.HasValue)
            {
                return;
            }

            anomalies.Add(
                new ActivityAnomaly(
                    ActivityAnomalyCode.EndTimeCalculationFailed,
                    "The end time could not be calculated from AST and ALEN.",
                    true));
        }

        private void DetectExcessiveDuration(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            if (!_settings.MaximumActivityDurationMilliseconds.HasValue)
            {
                return;
            }

            if (!activity.DurationMilliseconds.HasValue)
            {
                return;
            }

            long maximumDuration = _settings.MaximumActivityDurationMilliseconds.Value;

            if (activity.DurationMilliseconds.Value <= maximumDuration)
            {
                return;
            }

            anomalies.Add(new ActivityAnomaly(
                    ActivityAnomalyCode.DurationExceedsMaximum,
                    "The duration of the activity exceeds the configured maximum.",
                    true));
        }

        private static void DetectDriverAnomalies(ReconstructedActivity activity, List<ActivityAnomaly> anomalies)
        {
            if (activity.DriverIds.Count == 0)
            {
                anomalies.Add(new ActivityAnomaly(
                        ActivityAnomalyCode.MissingDriverIdentifier,
                        "No driver found in DID.",
                        true));

                return;
            }

            if (activity.DriverIds.Count > 1)
            {
                anomalies.Add(new ActivityAnomaly(
                        ActivityAnomalyCode.MultipleDriverIdentifiers,
                        "Multiple drivers are associated with this activity.",
                        true));
            }
        }
    }
}