using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COUPON_APPLY_SITE
    {
        public int COUPON_APPLY_SITE_SEQ { get; set; }
        public int COUPON_MST_SEQ { get; set; }
        public int COMPANY_SEQ { get; set; }

        public virtual COUPON_MST COUPON_MST_SEQNavigation { get; set; }
    }
}
