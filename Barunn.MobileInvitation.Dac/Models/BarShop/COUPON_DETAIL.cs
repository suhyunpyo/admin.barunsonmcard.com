using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COUPON_DETAIL
    {
        public COUPON_DETAIL()
        {
            COUPON_ISSUEs = new HashSet<COUPON_ISSUE>();
        }

        public int COUPON_DETAIL_SEQ { get; set; }
        public int COUPON_MST_SEQ { get; set; }
        public string COUPON_CODE { get; set; }
        public string DOWNLOAD_ACTIVE_YN { get; set; }

        public virtual COUPON_MST COUPON_MST_SEQNavigation { get; set; }
        public virtual ICollection<COUPON_ISSUE> COUPON_ISSUEs { get; set; }
    }
}
