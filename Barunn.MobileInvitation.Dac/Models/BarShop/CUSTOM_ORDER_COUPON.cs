using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ORDER_COUPON
    {
        public int ORDER_COUPON_SEQ { get; set; }
        public int COUPON_ISSUE_SEQ { get; set; }
        public int ORDER_SEQ { get; set; }
        public int COUPON_AMT { get; set; }
        public DateTime REG_DATE { get; set; }
    }
}
