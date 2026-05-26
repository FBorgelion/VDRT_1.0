using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.InvoiceLine
{
    public class InvoiceLineDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ActivityId { get; set; }
        public decimal Hours { get; set; }
        public decimal Amount { get; set; }

    }
}
