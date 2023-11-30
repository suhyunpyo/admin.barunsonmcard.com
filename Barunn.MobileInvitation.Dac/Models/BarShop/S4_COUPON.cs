using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_COUPON
    {
        public int SEQ { get; set; }
        public string coupon_code { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public DateTime reg_date { get; set; }
        public string discount_type { get; set; }
        public int discount_value { get; set; }
        public int? limit_price { get; set; }
        public string isYN { get; set; }
        public string coupon_desc { get; set; }
        public string isRecycle { get; set; }
        public string isWeddingCoupon { get; set; }
        public string isJehu { get; set; }
        public int? id { get; set; }
        public DateTime? end_date { get; set; }
        public string cardbrand { get; set; }
        public string item_type { get; set; }
        public string COUPON_TYPE_CODE { get; set; }
        public int? limit_order_count { get; set; }
        public string DEVICE_TYPE_CODE { get; set; }
        public string dup_ind { get; set; }
    }
}
