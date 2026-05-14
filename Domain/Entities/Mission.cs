using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Mission
    {

        public int Id { get; set; }
        public int SiteId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }


        //Navigation properties
        public Sites Site { get; set; } = null!;
        public Vehicle Vehicle { get; set; } = null!;
        public Driver Driver { get; set; } = null!;
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<TripAnomaly> TripAnomalies { get; set; } = new List<TripAnomaly>();
    }
}
