using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_Notice
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string writer { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int viewcnt { get; set; }
        public string notice_div { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public DateTime reg_date { get; set; }
        public string blank_ { get; set; }
    }
}
