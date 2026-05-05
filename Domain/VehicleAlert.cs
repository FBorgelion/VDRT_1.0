using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class VehicleAlert
    {

        public int Id { get; set; }
        public int Vehicle_Id { get; set; }
        public string AlertType { get; set; }
        public string Severity { get; set; }
        public decimal MetricValue { get; set; }
        public DateTime Timestamp { get; set; }


        // Navigation properties
        public Vehicle Vehicle { get; set; } = null!;
        public User? Acknowledger { get; set; }
        public User? Resolver { get; set; }
    }
}
