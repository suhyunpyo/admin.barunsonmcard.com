using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Evt_Plus_Friend
    {
        public int pf_idx { get; set; }
        public string pf_uid { get; set; }
        public string pf_fuid { get; set; }
        public DateTime? pf_regDate { get; set; }
        public string pf_coupon_status_f { get; set; }
        public string service { get; set; }
    }
}
