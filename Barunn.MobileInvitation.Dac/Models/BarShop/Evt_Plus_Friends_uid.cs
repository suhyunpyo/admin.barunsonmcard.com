using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Evt_Plus_Friends_uid
    {
        public int p_idx { get; set; }
        public string p_uid { get; set; }
        public string p_coupon_status { get; set; }
        public DateTime? p_regDate { get; set; }
        public DateTime? p_coupon_regdate { get; set; }
        public string service { get; set; }
    }
}
