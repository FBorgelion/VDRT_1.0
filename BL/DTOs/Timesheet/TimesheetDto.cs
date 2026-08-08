using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Timesheet
{
    public class TimesheetDto
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalHours { get; set; }
        public decimal RegularHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public int? ApproverId { get; set; }
        public DateTime ApprovedAt { get; set; }
    }
}
