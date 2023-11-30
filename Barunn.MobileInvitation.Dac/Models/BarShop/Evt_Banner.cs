using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Evt_Banner
    {
        public int seq { get; set; }
        public string imgfile_path { get; set; }
        public string link_url { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string view_div { get; set; }
        public int? MD_Choice_seq { get; set; }
        public DateTime create_date { get; set; }
    }
}
