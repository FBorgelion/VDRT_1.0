using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public enum ActivityAnomalyCode
    {
        MissingOpeningTrace = 0,
        MissingClosingTrace = 1,
        MissingActivityCode = 2,
        MissingStartTime = 3,
        MissingDuration = 4,
        InvalidDuration = 5,
        EndTimeCalculationFailed = 6,
        DurationExceedsMaximum = 7,
        MissingDriverIdentifier = 8,
        MultipleDriverIdentifiers = 9,
        UnknownActivityCode = 10,
        UnmappedActivityCode = 11
    }
}
