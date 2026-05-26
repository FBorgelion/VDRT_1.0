using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Position
{
    public class CreatePositionDto
    {
        public int VehicleId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Speed { get; set; }
    }
}
