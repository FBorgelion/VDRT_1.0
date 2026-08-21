using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Settings
{
    public sealed class ActivityReconstructionSettings
    {

        public TimeSpan ExcessiveDurationThreshold { get; init; } = TimeSpan.FromHours(24);

    }
}
