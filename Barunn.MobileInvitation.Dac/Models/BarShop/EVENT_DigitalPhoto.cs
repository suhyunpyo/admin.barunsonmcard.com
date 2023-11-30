using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVENT_DigitalPhoto
    {
        public int id { get; set; }
        public string coupon_code { get; set; }
        public string isUse { get; set; }
        public string member_id { get; set; }
        public string member_name { get; set; }
        public string member_email { get; set; }
        public string sales_gubun { get; set; }
        public DateTime? use_date { get; set; }
    }
}
