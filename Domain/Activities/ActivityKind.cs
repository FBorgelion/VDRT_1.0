using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public enum ActivityKind
    {
        Unmapped = 0,
        Unknown = 1,
        Driving = 2,
        Loading = 3,
        Unloading = 4,
        Waiting = 5,
        Weighing = 6,
        Miscellaneous = 7,
        Refueling = 8,
        Break = 9,
        PrivateBreak = 10,
        Washing = 11,
        Office = 12,
        Garage = 13,
        Hooking = 14,
        Unhooking = 15,
        Breakdown = 16,
        OvernightStay = 17,
        Covering = 18
    }
}
