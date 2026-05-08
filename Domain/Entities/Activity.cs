using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Activity
    {

        public int Id { get; set; }
        public int Mission_Id { get; set; }
        public int ActivityType_Id { get; set; }
        public int Driving_Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string ValidationStatus { get; set; }
        public int ValidatedBy { get; set; }
        public DateTime ValidatedAt{ get; set; }


        //Navigation properties
        public Mission? Mission { get; set; }
        public ActivityType ActivityType { get; set; } = null!;
        public Driver Driver { get; set; } = null!;
        public User? Validator { get; set; }
        public TripAnomaly? TripAnomaly { get; set; }
        public InvoiceLine? InvoiceLine { get; set; }
    }
}
