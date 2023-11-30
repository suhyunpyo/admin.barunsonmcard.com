using System;
using System.Collections.Generic;
using System.Text;

namespace Barunn.MobileInvitation.Common.Models
{
    public class AuthTicketModel
    {
        public string Name { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public  DateTime Issue { get; set; }
    }
}
