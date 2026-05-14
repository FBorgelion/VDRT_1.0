using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Site
{
    public class UpdateSiteDto
    {
        public string ClientName { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Geofence_radius { get; set; }
    }
}
