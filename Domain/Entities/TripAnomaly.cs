using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TripAnomaly
    {

        public int Id { get; set; }
        public int MissionId { get; set; }
        public int ActivityId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int ExpectedDuration { get; set; }
        public int ActualDuration { get; set; }
        public decimal DurationDiffPercentage { get; set; }
        public string Severity { get; set; } = string.Empty;
        public int? ReviewerId { get; set; }
        public DateTime ReviewedAt { get; set; }
        public string? ReviewComments { get; set; }



        // Navigation properties
        public Mission Mission { get; set; } = null!;

        public Activity Activity { get; set; } = null!;
        public Vehicle Vehicle { get; set; } = null!;
        public Driver Driver { get; set; } = null!;
        public User? Reviewer { get; set; }
    }
}
