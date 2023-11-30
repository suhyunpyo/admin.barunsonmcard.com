using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Poll
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string contents { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string view_div { get; set; }
        public DateTime reg_date { get; set; }
        public string poll_type { get; set; }
    }
}
