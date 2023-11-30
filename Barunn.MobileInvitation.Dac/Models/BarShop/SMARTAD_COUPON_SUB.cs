using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SMARTAD_COUPON_SUB
    {
        public int SMARTAD_COUPON_SUB_SEQ { get; set; }
        public int? SMARTAD_COUPON_MST_SEQ { get; set; }
        public string COUPON_CODE { get; set; }
        public string USER_ID { get; set; }
        public string ISSUE_YORN { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
