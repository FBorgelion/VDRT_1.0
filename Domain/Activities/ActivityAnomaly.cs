using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public sealed record ActivityAnomaly(
        ActivityAnomalyCode Code,
        string Message,
        bool BlocksProcessing);
}
