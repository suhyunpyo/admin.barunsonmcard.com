using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SMARTAD_MYCOUPON
    {
        public int MYCOUPON_SEQ { get; set; }
        public int SMARTAD_COUPON_MST_SEQ { get; set; }
        public string COUPON_CD { get; set; }
        public int AD_SEQ { get; set; }
        public string UID { get; set; }
        public string USE_YORN { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
