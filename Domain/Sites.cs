using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Sites
    {

        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Geofence_radius { get; set; }


        //Navigation properties
        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
