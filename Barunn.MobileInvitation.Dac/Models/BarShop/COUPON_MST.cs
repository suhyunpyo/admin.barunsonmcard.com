using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COUPON_MST
    {
        public COUPON_MST()
        {
            COUPON_APPLY_CARDs = new HashSet<COUPON_APPLY_CARD>();
            COUPON_APPLY_SERVICEs = new HashSet<COUPON_APPLY_SERVICE>();
            COUPON_APPLY_SITEs = new HashSet<COUPON_APPLY_SITE>();
            COUPON_APPLY_USERs = new HashSet<COUPON_APPLY_USER>();
            COUPON_DETAILs = new HashSet<COUPON_DETAIL>();
        }

        public int COUPON_MST_SEQ { get; set; }
        public string COUPON_NAME { get; set; }
        public string COUPON_DESC { get; set; }
        public int COUPON_ISSUE_CNT { get; set; }
        public int DOWNLOAD_MAX_QTY { get; set; }
        public int DOWNLOAD_LIMIT_PER_PERSON_QTY { get; set; }
        public int DOWNLOADED_CNT { get; set; }
        public string COUPON_GROUP_CODE { get; set; }
        public string COUPON_TYPE_CODE { get; set; }
        public string ORDER_TYPE_CODE { get; set; }
        public string DUP_COUPON_ALLOW_YN { get; set; }
        public string AD_COUPON_ALLOW_YN { get; set; }
        public string ADD_COUPON_ALLOW_YN { get; set; }
        public string ORDER_APPLY_TYPE { get; set; }
        public string EXPIRY_TYPE { get; set; }
        public DateTime? EXPIRY_START_DATE { get; set; }
        public DateTime? EXPIRY_END_DATE { get; set; }
        public int EXPIRY_CUSTOM_VALUE { get; set; }
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
        public string REFERER_SALES_GUBUN { get; set; }
        public DateTime? USER_REG_START_DATE { get; set; }
        public DateTime? USER_REG_END_DATE { get; set; }
        public DateTime? WEDDING_START_DATE { get; set; }
        public DateTime? WEDDING_END_DATE { get; set; }
        public string WEDD_PLACE { get; set; }
        public string SAMPLE_ORDER_TYPE { get; set; }
        public DateTime? SAMPLE_ORDER_START_DATE { get; set; }
        public DateTime? SAMPLE_ORDER_END_DATE { get; set; }
        public string CARD_ORDER_TYPE { get; set; }
        public int CARD_ORDER_CNT { get; set; }
        public DateTime? COUPON_ISSUE_START_DATE { get; set; }
        public DateTime? COUPON_ISSUE_END_DATE { get; set; }
        public string STATUS_ACTIVE_YN { get; set; }
        public DateTime REG_DATE { get; set; }
        public string REG_USER_ID { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER_ID { get; set; }

        public virtual ICollection<COUPON_APPLY_CARD> COUPON_APPLY_CARDs { get; set; }
        public virtual ICollection<COUPON_APPLY_SERVICE> COUPON_APPLY_SERVICEs { get; set; }
        public virtual ICollection<COUPON_APPLY_SITE> COUPON_APPLY_SITEs { get; set; }
        public virtual ICollection<COUPON_APPLY_USER> COUPON_APPLY_USERs { get; set; }
        public virtual ICollection<COUPON_DETAIL> COUPON_DETAILs { get; set; }
    }
}
