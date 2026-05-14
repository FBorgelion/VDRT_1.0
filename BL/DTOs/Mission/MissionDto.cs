using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Mission
{
    public class MissionDto
    {

        public int Id { get; set; }
        public int SiteId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }

    }
}
