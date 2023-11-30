using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class VW_COUPON_USER_LIST
    {
        public int COUPON_MST_SEQ { get; set; }
        public int COUPON_DETAIL_SEQ { get; set; }
        public int COUPON_ISSUE_SEQ { get; set; }
        public string COUPON_CODE { get; set; }
        public string UID { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string ACTIVE_YN { get; set; }
        public DateTime? END_DATE { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string EXPIRY_TYPE { get; set; }
        public int EXPIRY_CUSTOM_VALUE { get; set; }
        public DateTime? EXPIRY_START_DATE { get; set; }
        public DateTime? EXPIRY_END_DATE { get; set; }
        public string COUPON_NAME { get; set; }
        public string COUPON_DESC { get; set; }
        public string COUPON_GROUP_CODE { get; set; }
        public string COUPON_TYPE_CODE { get; set; }
        public string ORDER_TYPE_CODE { get; set; }
        public string DUP_COUPON_ALLOW_YN { get; set; }
        public string AD_COUPON_ALLOW_YN { get; set; }
        public string ADD_COUPON_ALLOW_YN { get; set; }
        public string ORDER_APPLY_TYPE { get; set; }
        public string DOWNLOAD_KIND { get; set; }
        public string DOWNLOAD_KIND_ETC_CODE { get; set; }
        public string DOWNLOAD_USER_GB { get; set; }
        public string USE_PLACE { get; set; }
        public string USE_DEVICE { get; set; }
        public int DISCOUNT_VALUE { get; set; }
        public string DISCOUNT_FIXED_RATE_TYPE { get; set; }
        public int DISCOUNT_MAX_AMT { get; set; }
        public int ORDER_AMT { get; set; }
        public int ORDER_CNT { get; set; }
        public int CARD_ORDER_CNT { get; set; }
        public string STATUS_ACTIVE_YN { get; set; }
    }
}
