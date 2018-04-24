using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace HangfireAndPostal.Models
{
    public class RegisterUserEmail : Email
    {
        public string FirstName { get; set; }

        public string Email { get; set; }
    }
}