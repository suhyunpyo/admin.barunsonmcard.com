using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COUPON_APPLY_SERVICE
    {
        public int COUPON_APPLY_SERVICE_SEQ { get; set; }
        public int COUPON_MST_SEQ { get; set; }
        public string CLSS_CODE { get; set; }
        public string CMMN_CODE { get; set; }

        public virtual COUPON_MST COUPON_MST_SEQNavigation { get; set; }
    }
}
