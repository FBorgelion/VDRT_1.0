using Domain.Activities;

namespace BL.Interfaces.Services
{
    public interface IActivityAnomalyDetector
    {
        IReadOnlyList<ActivityAnomaly> Detect(ReconstructedActivity activity);
    }
}