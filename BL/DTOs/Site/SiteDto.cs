using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Site
{
    public class SiteDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Geofence_radius { get; set; }
    }
}
