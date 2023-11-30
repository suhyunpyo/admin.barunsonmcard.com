using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Event_Halloween
    {
        public int event_seq { get; set; }
        public string uid { get; set; }
        public int? company_seq { get; set; }
        public short? random_count { get; set; }
        public DateTime? reg_date { get; set; }
        public short? run_status { get; set; }
        public short? halloween_count { get; set; }
        public string coupon_number { get; set; }
        public short? coupon_count { get; set; }
        public DateTime? coupon_reg_date { get; set; }
    }
}
