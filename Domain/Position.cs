using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public  class Position
    {

        public int Id { get; set; }
        public int Vehicle_Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Speed { get; set; }


        // Navigation properties
        public Vehicle Vehicle { get; set; } = null!;
    }
}
