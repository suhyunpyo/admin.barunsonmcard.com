using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_FAQ
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string writer { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int faq_div { get; set; }
        public int display_order { get; set; }
        public int viewcnt { get; set; }
        public DateTime reg_date { get; set; }
        public string blank_ { get; set; }
    }
}
