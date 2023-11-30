using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COUPON_ISSUE
    {
        public int COUPON_ISSUE_SEQ { get; set; }
        public int COUPON_DETAIL_SEQ { get; set; }
        public string UID { get; set; }
        public string ACTIVE_YN { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public DateTime? END_DATE { get; set; }
        public DateTime? REG_DATE { get; set; }

        public virtual COUPON_DETAIL COUPON_DETAIL_SEQNavigation { get; set; }
    }
}
