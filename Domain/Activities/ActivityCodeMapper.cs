using Domain.Activities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public sealed class ActivityCodeMapper : IActivityCodeMapper
    {

        private static readonly IReadOnlyDictionary<string, ActivityKind> Mappings = new Dictionary<string, ActivityKind>(StringComparer.OrdinalIgnoreCase)
            {
                ["UN"] = ActivityKind.Unknown,
                ["DR"] = ActivityKind.Driving,
                ["LI"] = ActivityKind.Login,
                ["LO"] = ActivityKind.Logout,
                ["TJ"] = ActivityKind.Traffic,

                ["attente"] = ActivityKind.Waiting,
                ["charger"] = ActivityKind.Loading,
                ["decharger"] = ActivityKind.Unloading,
                ["Charger Regie"] = ActivityKind.LoadingRegie,
                ["Décharger Regie"] = ActivityKind.UnloadingRegie,
                ["faire le plain"] = ActivityKind.Refuelling,
                ["coupure"] = ActivityKind.Break,
                ["coupure prive"] = ActivityKind.PrivateBreak,
                ["lavage"] = ActivityKind.Washing,
                ["nuittee"] = ActivityKind.OvernightStay,
                ["bureau"] = ActivityKind.Office,
                ["garage"] = ActivityKind.Garage,
                ["panne"] = ActivityKind.Breakdown,
                ["bascule"] = ActivityKind.Weighbridge,
                ["accrochage / décrochage"] = ActivityKind.Coupling,
                ["bâchage"] = ActivityKind.Tarping,
                ["divers"] = ActivityKind.Other
            };

        public ActivityKind Map(string? rawCode)
        {
            if (string.IsNullOrWhiteSpace(rawCode))
                return ActivityKind.Unmapped;

            var normalizedCode = rawCode.Trim();

            if (Mappings.TryGetValue(normalizedCode, out ActivityKind kind))
            {
                return kind;
            }

            return ActivityKind.Unmapped;
        }

    }
}
