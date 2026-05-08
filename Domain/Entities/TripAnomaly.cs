using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TripAnomaly
    {

        public int Id { get; set; }
        public int Mission_Id { get; set; }
        public int Activity_Id { get; set; }
        public int Vehicle_Id { get; set; }
        public int Driver_Id { get; set; }
        public int ExpectedDuration { get; set; }
        public int ActualDuration { get; set; }
        public decimal DurationDiffPercentage { get; set; }
        public string Severity { get; set; }



        // Navigation properties
        public Mission? Mission { get; set; }

        public Activity? Activity { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public Driver Driver { get; set; } = null!;
        public User? Reviewer { get; set; }
    }
}
