using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_MyCoupon
    {
        public string coupon_code { get; set; }
        public string uid { get; set; }
        public int company_seq { get; set; }
        public int id { get; set; }
        public string isMyYN { get; set; }
        public DateTime? end_date { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
