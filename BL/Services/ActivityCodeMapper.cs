using BL.Interfaces.Services;
using Domain.Activities;

namespace BL.Services
{
    public class ActivityCodeMapper : IActivityCodeMapper
    {
        private static readonly IReadOnlyDictionary<string, ActivityKind> Mappings = new Dictionary<string, ActivityKind>(StringComparer.OrdinalIgnoreCase)
            {
                    ["UN"] = ActivityKind.Unknown,
                    ["DR"] = ActivityKind.Driving,

                    ["attente"] = ActivityKind.Waiting,

                    ["charger"] = ActivityKind.Loading,
                    ["Charger Regie"] = ActivityKind.Loading,

                    ["decharger"] = ActivityKind.Unloading,
                    ["Décharger Regie"] = ActivityKind.Unloading,

                    ["Bascule"] = ActivityKind.Weighing,
                    ["divers"] = ActivityKind.Miscellaneous,

                    ["faire le plain"] = ActivityKind.Refueling,
                    ["faire le plein"] = ActivityKind.Refueling,

                    ["coupure"] = ActivityKind.Break,
                    ["coupure prive"] = ActivityKind.PrivateBreak,
                    ["coupure privée"] = ActivityKind.PrivateBreak,

                    ["lavage"] = ActivityKind.Washing,
                    ["bureau"] = ActivityKind.Office,
                    ["garage"] = ActivityKind.Garage,

                    ["accrocher"] = ActivityKind.Hooking,

                    ["Decrocher"] = ActivityKind.Unhooking,
                    ["Décrocher"] = ActivityKind.Unhooking,

                    ["panne"] = ActivityKind.Breakdown,

                    ["nuittee"] = ActivityKind.OvernightStay,
                    ["nuitée"] = ActivityKind.OvernightStay,

                    ["bacher"] = ActivityKind.Covering,
                    ["bâcher"] = ActivityKind.Covering
                };

        public ActivityKind Map(string? rawActivityCode)
        {
            if (string.IsNullOrWhiteSpace(rawActivityCode))
            {
                return ActivityKind.Unmapped;
            }

            string normalizedCode = rawActivityCode.Trim();

            bool mappingExists = Mappings.TryGetValue(normalizedCode, out ActivityKind activityKind);

            if (mappingExists)
            {
                return activityKind;
            }

            return ActivityKind.Unmapped;
        }
    }
}