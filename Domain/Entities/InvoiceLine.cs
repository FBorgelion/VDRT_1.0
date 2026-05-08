using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class InvoiceLine
    {

        public int Id { get; set; }
        public int Invoice_Id { get; set; }
        public int Activity_Id { get; set; }
        public decimal Hours { get; set; }
        public decimal Amount { get; set; }



        //Navigation properties
        public Invoice Invoice { get; set; } = null!;
        public Activity? Activity { get; set; }
    }
}
