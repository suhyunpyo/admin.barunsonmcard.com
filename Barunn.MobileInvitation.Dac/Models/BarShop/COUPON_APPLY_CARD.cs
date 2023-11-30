using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COUPON_APPLY_CARD
    {
        public int COUPON_APPLY_CARD_SEQ { get; set; }
        public int COUPON_MST_SEQ { get; set; }
        public int CARD_SEQ { get; set; }
        public string CARD_ALLOW_YN { get; set; }

        public virtual COUPON_MST COUPON_MST_SEQNavigation { get; set; }
    }
}
