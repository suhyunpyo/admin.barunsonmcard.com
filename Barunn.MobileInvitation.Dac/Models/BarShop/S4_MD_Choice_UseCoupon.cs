using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_MD_Choice_UseCoupon
    {
        public int seq { get; set; }
        public int choice_seq { get; set; }
        public string coupon_code { get; set; }
        public int? view_coupon_downcnt { get; set; }
        public DateTime? down_start_dt { get; set; }
        public DateTime? down_end_dt { get; set; }
    }
}
