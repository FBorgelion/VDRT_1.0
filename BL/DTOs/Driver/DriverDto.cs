using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Driver
{
    public class DriverDto
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool Active { get; set; }

    }
}
