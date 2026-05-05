using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ActivityType
    {

        public int Id { get; set; }
        public string Code { get; set; }


        //Navigation properties
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    }
}
