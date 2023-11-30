using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_PhotoStory
    {
        public int event_num { get; set; }
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string addr { get; set; }
        public string addr2 { get; set; }
        public int? zipcode { get; set; }
        public string comment { get; set; }
        public string image_url { get; set; }
        public int? img_width { get; set; }
        public int? img_height { get; set; }
        public string url { get; set; }
        public string isVisible { get; set; }
        public int cnt { get; set; }
        public DateTime regdate { get; set; }
        public DateTime? MOD_DATE { get; set; }
        public int? UP_CNT { get; set; }
    }
}
