using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServicesApplication.Models
{
    public class UserRegistration
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
    }
}