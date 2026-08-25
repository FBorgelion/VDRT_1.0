using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public class ActivityAnomaly
    {
        public ActivityAnomaly(ActivityAnomalyCode code, string message, bool requiresManualReview)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(
                    "Anomaly message is required.",
                    nameof(message));
            }

            Code = code;
            Message = message;
            RequiresManualReview = requiresManualReview;
        }

        public ActivityAnomalyCode Code { get; }

        public string Message { get; }

        public bool RequiresManualReview { get; }
    }
}
