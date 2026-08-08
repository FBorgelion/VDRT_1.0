using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.TripAnomaly
{
    public class UpdateTripAnomalyDto
    {
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int ExpectedDuration { get; set; }
        public int ActualDuration { get; set; }
        public decimal DurationDiffPercentage { get; set; }
        public string Severity { get; set; } = string.Empty;
        public int? ReviewerId { get; set; }
        public DateTime ReviewedAt { get; set; }
        public string? ReviewComments { get; set; }

    }
}
