using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Activity
{
    public class ActivityDto
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
        public DateTime? ValidatedAt { get; set; }
    }
}
