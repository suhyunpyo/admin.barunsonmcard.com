using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Coupon_OKC
    {
        public int seq { get; set; }
        public string ckey { get; set; }
        public string isYN { get; set; }
        public DateTime? use_date { get; set; }
        public string isAdminYN { get; set; }
    }
}
