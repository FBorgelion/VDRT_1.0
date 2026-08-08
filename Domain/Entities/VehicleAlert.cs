using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class VehicleAlert
    {

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string AlertType { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public decimal MetricValue { get; set; }
        public DateTime Timestamp { get; set; }


        // Navigation properties
        public Vehicle Vehicle { get; set; } = null!;
        public User? Acknowledger { get; set; }
        public User? Resolver { get; set; }
    }
}
