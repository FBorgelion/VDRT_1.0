using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Timesheet
    {

        public int Id { get; set; }
        public int Employee_Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalHours { get; set; }
        public decimal RegularHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedAt { get; set; }



        // Navigation properties
        public Driver Employee { get; set; } = null!;
        public User? Approver { get; set; }
    }
}
