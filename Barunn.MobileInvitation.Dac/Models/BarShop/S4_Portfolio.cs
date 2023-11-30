using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Portfolio
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string section_gubun { get; set; }
        public string writer { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int? card_seq { get; set; }
        public string list_img { get; set; }
        public string thumb_img { get; set; }
        public DateTime reg_date { get; set; }
        public string sub_title { get; set; }
        public string sub_img { get; set; }
    }
}
