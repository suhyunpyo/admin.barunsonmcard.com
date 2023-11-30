using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class event_sms_coupon
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public string name { get; set; }
        public string hphone { get; set; }
        public DateTime created_tmstmp { get; set; }
    }
}
