using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public enum ActivityAnomalyCode
    {
        MissingOpeningTrace,
        MissingClosingTrace,
        MissingStartTime,
        MissingDuration,
        NonPositiveDuration,
        ExcessiveDuration,
        DurationOverflow,
        ConflictingStartTime,
        ConflictingFinalDuration,
        ActivityCodeChanged,
        UnmappedActivityCode
    }
}
