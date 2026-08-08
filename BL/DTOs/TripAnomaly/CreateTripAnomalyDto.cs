using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.TripAnomaly
{
    public class CreateTripAnomalyDto
    {

        public int MissionId { get; set; }
        public int ActivityId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int ExpectedDuration { get; set; }
        public int ActualDuration { get; set; }
        public decimal DurationDiffPercentage { get; set; }
        public string Severity { get; set; } = string.Empty;

    }
}
