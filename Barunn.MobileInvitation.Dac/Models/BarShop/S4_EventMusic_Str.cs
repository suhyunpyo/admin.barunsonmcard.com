using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventMusic_Str
    {
        public int EVENT_MUSIC_SEQ { get; set; }
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string event_title { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string event_url { get; set; }
        public DateTime reg_date { get; set; }
        public int? view_cnt { get; set; }
        public string duplication_yn { get; set; }
    }
}
