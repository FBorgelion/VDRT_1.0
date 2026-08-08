using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Invoice
{
    public class UpdateInvoiceDto
    {
        public string ClientName { get; set; } = string.Empty;
        public int SiteId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
    }
}
