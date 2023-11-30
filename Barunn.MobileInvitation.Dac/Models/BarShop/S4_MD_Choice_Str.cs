using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_MD_Choice_Str
    {
        public int md_seq { get; set; }
        public string md_text { get; set; }
        public int md_upper_code { get; set; }
        public string choice_div { get; set; }
        public string link_url { get; set; }
        public string link_target { get; set; }
        public int company_seq { get; set; }
        public DateTime reg_date { get; set; }
        public int sorting_num { get; set; }
        public string cardtitle_yn { get; set; }
        public string customimg_yn { get; set; }
        public string md_sub_text { get; set; }
        public string md_image { get; set; }
        public string md_html { get; set; }
    }
}
