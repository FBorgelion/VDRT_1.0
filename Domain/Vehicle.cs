using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Vehicle
    {

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public bool Active { get; set; }


        //Navigation properties
        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
        public ICollection<Position> Positions { get; set; } = new List<Position>();
        public ICollection<VehicleAlert> VehicleAlerts { get; set; } = new List<VehicleAlert>();

    }
}
