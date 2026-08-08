using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Position
{
    public class PositionDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Speed { get; set; }

    }
}
