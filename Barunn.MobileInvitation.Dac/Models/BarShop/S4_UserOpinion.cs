using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_UserOpinion
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public short? cs_seq { get; set; }
        public string wr_div { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public string hot_line { get; set; }
        public string view_div { get; set; }
        public DateTime reg_date { get; set; }
    }
}
