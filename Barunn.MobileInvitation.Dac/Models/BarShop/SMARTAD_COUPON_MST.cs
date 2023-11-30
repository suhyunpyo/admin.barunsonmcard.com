using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SMARTAD_COUPON_MST
    {
        public int SMARTAD_COUPON_MST_SEQ { get; set; }
        public int? AD_SEQ { get; set; }
        public string COUPON_NAME { get; set; }
        public string COUPON_CD { get; set; }
        public string DISCOUNT_TYPE { get; set; }
        public decimal? DISCOUNT_PRICE { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string USE_YORN { get; set; }
        public int ISSUE_CNT { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string PROMOTION_TYPE { get; set; }
    }
}
