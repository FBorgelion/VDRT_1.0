using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Invoice
    {

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string ClientName { get; set; }
        public int Site_Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }


        //Navigation properties
        public Vehicle Vehicle { get; set; } = null!;
        public Driver Driver { get; set; } = null!;
    }
}
