using Domain.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.Services
{
    public interface IActivityReconstructionService
    {

        IReadOnlyList<ReconstructedActivity> Reconstruct(IEnumerable<RawActivityTrace> traces);

    }
}
