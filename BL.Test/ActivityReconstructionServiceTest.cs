using BL.Services;
using BL.Settings;
using Domain.Activities;

namespace BL.Tests;

public sealed class ActivityReconstructionServiceTests
{
    private readonly ActivityReconstructionService _service;

    public ActivityReconstructionServiceTests()
    {
        ActivityCodeMapper mapper = new ActivityCodeMapper();

        ActivityReconstructionSettings settings = new ActivityReconstructionSettings
            {
                ExcessiveDurationThreshold =
                    TimeSpan.FromHours(24)
            };

        _service = new ActivityReconstructionService(mapper, settings);
    }

    [Theory]
    [InlineData(11)]
    [InlineData(13)]
    public void Type11Or13ClosesActivityAndUsesAstPlusAlen(
        int closingType)
    {
        DateTime start = new DateTime(
            2026,
            1,
            17,
            22,
            25,
            0,
            DateTimeKind.Unspecified);

        DateTime technicalTransmissionTime =
            start.AddHours(4);

        List<RawActivityTrace> traces =
            new List<RawActivityTrace>
            {
                CreateTrace(
                    traceType: 10,
                    position: 1,
                    activityStart: start,
                    technicalTime: start.AddMinutes(1),
                    durationMilliseconds: null,
                    activityCode: "DR"),

                CreateTrace(
                    traceType: closingType,
                    position: 2,
                    activityStart: start,
                    technicalTime:
                        technicalTransmissionTime,
                    durationMilliseconds: 1_800_000,
                    activityCode: "DR")
            };

        ReconstructedActivity result =
            _service.Reconstruct(traces).Single();

        Assert.Equal(
            ActivityLifecycleState.Closed,
            result.LifecycleState);

        Assert.Equal(
            ActivityCandidateStatus.Recognized,
            result.CandidateStatus);

        Assert.Equal(
            start.AddMinutes(30),
            result.CalculatedEndTime);

        Assert.NotEqual(
            technicalTransmissionTime,
            result.CalculatedEndTime);

        Assert.Equal(
            TimeSpan.FromMinutes(30),
            result.Duration);
    }

    [Fact]
    public void Type12AndType9DoNotCloseActivity()
    {
        DateTime start = new DateTime(
            2026,
            1,
            17,
            22,
            25,
            0,
            DateTimeKind.Unspecified);

        List<RawActivityTrace> traces =
            new List<RawActivityTrace>
            {
                CreateTrace(
                    traceType: 10,
                    position: 1,
                    activityStart: start,
                    technicalTime: start.AddMinutes(1),
                    durationMilliseconds: null,
                    activityCode: "DR"),

                CreateTrace(
                    traceType: 12,
                    position: 2,
                    activityStart: start,
                    technicalTime: start.AddMinutes(10),
                    durationMilliseconds: 600_000,
                    activityCode: "DR"),

                CreateTrace(
                    traceType: 9,
                    position: 3,
                    activityStart: start,
                    technicalTime: start.AddHours(1),
                    durationMilliseconds: null,
                    activityCode: "DR")
            };

        ReconstructedActivity result =
            _service.Reconstruct(traces).Single();

        Assert.Equal(
            ActivityLifecycleState.OpenAtImportBoundary,
            result.LifecycleState);

        Assert.Equal(
            ActivityCandidateStatus.PendingReview,
            result.CandidateStatus);

        Assert.False(
            result.IsStructurallyComplete);

        Assert.Equal(
            3,
            result.SourceTraces.Count);
    }

    [Fact]
    public void UnknownActivityRequiresReviewAndSplitsDriverIds()
    {
        DateTime start = new DateTime(
            2026,
            1,
            17,
            22,
            25,
            0,
            DateTimeKind.Unspecified);

        List<RawActivityTrace> traces =
            new List<RawActivityTrace>
            {
                CreateTrace(
                    traceType: 10,
                    position: 1,
                    activityStart: start,
                    technicalTime: start.AddMinutes(1),
                    durationMilliseconds: null,
                    activityCode: "UN",
                    rawDriverIds: "877;592"),

                CreateTrace(
                    traceType: 13,
                    position: 2,
                    activityStart: start,
                    technicalTime: start.AddMinutes(31),
                    durationMilliseconds: 1_800_000,
                    activityCode: "UN",
                    rawDriverIds: "877;592")
            };

        ReconstructedActivity result =
            _service.Reconstruct(traces).Single();

        Assert.Equal(
            ActivityCandidateStatus.Unknown,
            result.CandidateStatus);

        Assert.Contains(
            "877",
            result.ExternalDriverIds);

        Assert.Contains(
            "592",
            result.ExternalDriverIds);

        Assert.False(
            result.IsStructurallyComplete);
    }

    private static RawActivityTrace CreateTrace(
        int traceType,
        int position,
        DateTime activityStart,
        DateTime technicalTime,
        long? durationMilliseconds,
        string activityCode,
        string rawDriverIds = "877")
    {
        RawActivityTrace trace =
            new RawActivityTrace
            {
                ImportFingerprint =
                    "test-import-file",

                PositionInFile =
                    position,

                ExternalActivityId =
                    "600008276176869056825837",

                RawSourceReference =
                    "S119",

                RawTraceType =
                    traceType,

                RawTraceTime =
                    technicalTime.ToString("O"),

                TraceTime =
                    technicalTime,

                ExternalSequenceNumber =
                    position,

                RawActivityCode =
                    activityCode,

                RawActivityStartTime =
                    activityStart.ToString("O"),

                ActivityStartTime =
                    activityStart,

                DurationMilliseconds =
                    durationMilliseconds,

                RawExternalDriverIds =
                    rawDriverIds,

                RawXml =
                    "<trace />"
            };

        return trace;
    }
}