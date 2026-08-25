using BL.Interfaces.Services;
using BL.Services;
using BL.Settings;
using Domain.Activities;

namespace BL.Tests.Services
{
    public class ActivityReconstructionServiceTests
    {
        private readonly IActivityReconstructionService _service;

        public ActivityReconstructionServiceTests()
        {
            ActivityReconstructionSettings settings = new()
            {
                MaximumActivityDurationMilliseconds = null
            };

            IActivityCodeMapper activityCodeMapper =
                new ActivityCodeMapper();

            IActivityAnomalyDetector anomalyDetector =
                new ActivityAnomalyDetector(settings);

            _service = new ActivityReconstructionService(
                activityCodeMapper,
                anomalyDetector);
        }

        [Fact]
        public void Reconstruct_ShouldCreateCompleteDrivingActivity()
        {
            DateTime startTime =
                new DateTime(2026, 1, 18, 8, 0, 0);

            List<RawActivityTrace> traces = new()
            {
                new RawActivityTrace
                {
                    TraceType = ActivityTraceTypes.Opening,
                    SourceId = "vehicle-1",
                    LinkId = "activity-123",
                    ActivityCode = "DR",
                    DriverId = "592",
                    Sequence = 100,
                    TechnicalTime = startTime,
                    ActivityStartTime = startTime
                },
                new RawActivityTrace
                {
                    TraceType = ActivityTraceTypes.ValidatedClosing,
                    SourceId = "vehicle-1",
                    LinkId = "activity-123",
                    ActivityCode = "DR",
                    DriverId = "592",
                    Sequence = 100,
                    TechnicalTime = startTime.AddHours(1),
                    ActivityStartTime = startTime,
                    ActivityLengthMilliseconds = 3_600_000
                }
            };

            IReadOnlyList<ReconstructedActivity> result =
                _service.Reconstruct(traces);

            ReconstructedActivity activity =
                Assert.Single(result);

            Assert.Equal(ActivityKind.Driving, activity.ActivityKind);
            Assert.Equal(
                ActivityLifecycleState.Complete,
                activity.LifecycleState);
            Assert.Equal(
                ActivityCandidateStatus.Recognized,
                activity.CandidateStatus);
            Assert.Equal(
                startTime.AddHours(1),
                activity.EndTime);
            Assert.Equal("592", Assert.Single(activity.DriverIds));
        }

        [Fact]
        public void Reconstruct_ShouldKeepActivityOpenWhenClosingTraceIsMissing()
        {
            DateTime startTime =
                new DateTime(2026, 1, 18, 8, 0, 0);

            List<RawActivityTrace> traces = new()
            {
                new RawActivityTrace
                {
                    TraceType = ActivityTraceTypes.Opening,
                    SourceId = "vehicle-1",
                    LinkId = "activity-123",
                    ActivityCode = "DR",
                    DriverId = "592",
                    Sequence = 100,
                    TechnicalTime = startTime,
                    ActivityStartTime = startTime
                }
            };

            ReconstructedActivity activity =
                Assert.Single(_service.Reconstruct(traces));

            Assert.Equal(
                ActivityLifecycleState.OpenAtImportBoundary,
                activity.LifecycleState);

            Assert.Null(activity.EndTime);

            Assert.Contains(
                activity.Anomalies,
                anomaly => anomaly.Code
                    == ActivityAnomalyCode.MissingClosingTrace);
        }

        [Fact]
        public void Reconstruct_ShouldRequireReviewForUnknownActivity()
        {
            DateTime startTime =
                new DateTime(2026, 1, 18, 8, 0, 0);

            List<RawActivityTrace> traces = new()
            {
                new RawActivityTrace
                {
                    TraceType = ActivityTraceTypes.Opening,
                    SourceId = "vehicle-1",
                    LinkId = "activity-123",
                    ActivityCode = "UN",
                    DriverId = "592",
                    TechnicalTime = startTime,
                    ActivityStartTime = startTime
                },
                new RawActivityTrace
                {
                    TraceType = ActivityTraceTypes.Closing,
                    SourceId = "vehicle-1",
                    LinkId = "activity-123",
                    ActivityCode = "UN",
                    DriverId = "592",
                    TechnicalTime = startTime.AddMinutes(10),
                    ActivityStartTime = startTime,
                    ActivityLengthMilliseconds = 600_000
                }
            };

            ReconstructedActivity activity =
                Assert.Single(_service.Reconstruct(traces));

            Assert.Equal(ActivityKind.Unknown, activity.ActivityKind);

            Assert.Equal(
                ActivityCandidateStatus.RequiresReview,
                activity.CandidateStatus);
        }
    }
}