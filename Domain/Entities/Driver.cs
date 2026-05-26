using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Driver
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Matricule { get; set; }
        public bool Active { get; set; } = true;


        //Navigation properties
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
        public ICollection<TripAnomaly> TripAnomalies { get; set; } = new List<TripAnomaly>();


    }
}
