using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class RESEARCH_AN
    {
        public int id { get; set; }
        public int order_seq { get; set; }
        public string isReceive { get; set; }
        public string ans1_str { get; set; }
        public string ans2_str { get; set; }
        public string ans3_str { get; set; }
        public string ans4_str { get; set; }
        public string service_ment { get; set; }
        public DateTime? reg_Date { get; set; }
    }
}
