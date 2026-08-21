using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public enum ActivityKind
    {
        Unmapped,
        Unknown,
        Driving,
        Login,
        Logout,
        Traffic,
        Waiting,
        Loading,
        Unloading,
        LoadingRegie,
        UnloadingRegie,
        Refuelling,
        Break,
        PrivateBreak,
        Washing,
        OvernightStay,
        Office,
        Garage,
        Breakdown,
        Weighbridge,
        Coupling,
        Tarping,
        Other
    }
}
