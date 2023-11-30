using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class shakr_coupon_event
    {
        public int seq { get; set; }
        public string coupon_seq { get; set; }
        public string company_seq { get; set; }
        public string id { get; set; }
        public string bhands_used { get; set; }
        public string shakr_used { get; set; }
        public string cp_price { get; set; }
        public DateTime? cp_reg_date { get; set; }
        public DateTime? cp_end_date { get; set; }
        public string temp1 { get; set; }
        public string temp2 { get; set; }
    }
}
