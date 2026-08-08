using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Activity
    {

        public int Id { get; set; }
        public int MissionId { get; set; }
        public int ActivityTypeId { get; set; }
        public int DriverId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ValidationStatus { get; set; } = string.Empty;
        public int? ValidatorId { get; set; }
        public DateTime? ValidatedAt{ get; set; }


        //Navigation properties
        public Mission? Mission { get; set; }
        public ActivityType ActivityType { get; set; } = null!;
        public Driver Driver { get; set; } = null!;
        public User? Validator { get; set; }

        public ICollection<TripAnomaly> TripAnomalies { get; set; } = new List<TripAnomaly>();
        public InvoiceLine? InvoiceLine { get; set; }
    }
}
