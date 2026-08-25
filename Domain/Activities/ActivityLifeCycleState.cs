using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public enum ActivityLifecycleState
    {
        Complete = 0,
        OpenAtImportBoundary = 1
    }
}
