using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class The_Ewed_Coupon
    {
        public int seq { get; set; }
        public string coupon_code { get; set; }
        public int? order_id { get; set; }
        public string iscoupon_kind { get; set; }
        public string isuse_yn { get; set; }
        public string description { get; set; }
        public DateTime rdate { get; set; }
    }
}
