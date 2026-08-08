using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Timesheet
{
    public class UpdateTimesheetDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalHours { get; set; }
        public decimal RegularHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public DateTime ApprovedAt { get; set; }
    }
}
