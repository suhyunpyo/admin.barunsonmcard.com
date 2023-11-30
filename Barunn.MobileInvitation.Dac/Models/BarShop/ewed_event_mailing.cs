using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_event_mailing
    {
        public int seq { get; set; }
        public int order_seq { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string send { get; set; }
        public DateTime? mdate { get; set; }
    }
}
