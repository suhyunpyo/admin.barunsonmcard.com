using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_News
    {
        public int seq { get; set; }
        public string category { get; set; }
        public int? company_seq { get; set; }
        public string title { get; set; }
        public string medium_name { get; set; }
        public string url_taget { get; set; }
        public string URL_TARGET_BLANK_YORN { get; set; }
        public string isdp { get; set; }
        public DateTime reg_date { get; set; }
        public string temp1 { get; set; }
        public string temp2 { get; set; }
        public string contents { get; set; }
        public int? viewcnt { get; set; }
    }
}
