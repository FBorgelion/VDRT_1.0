using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }


        //Navigation 
        public ICollection<Activity> ValidatedActivities { get; set; }
        public ICollection<Timesheet> ApprovedTimesheets { get; set; }
        public ICollection<Invoice> CreatedInvoices { get; set; }

    }
}
