using Domain.Activities;

namespace BL.Interfaces.Services
{
    public interface IActivityReconstructionService
    {
        IReadOnlyList<ReconstructedActivity> Reconstruct(IEnumerable<RawActivityTrace> rawTraces);
    }
}