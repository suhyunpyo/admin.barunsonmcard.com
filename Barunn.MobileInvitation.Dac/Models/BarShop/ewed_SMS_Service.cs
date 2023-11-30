using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_SMS_Service
    {
        public string uid { get; set; }
        public string isMember { get; set; }
        public short sms_count { get; set; }
        public short send_sms_count { get; set; }
        public string etc { get; set; }
        public DateTime? regdate { get; set; }
    }
}
