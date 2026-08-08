using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.VehicleAlert
{
    public class CreateVehicleAlertDto
    {
        public int VehicleId { get; set; }
        public string AlertType { get; set; }
        public string Severity { get; set; }
        public decimal MetricValue { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
