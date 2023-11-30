using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_MediaContent
    {
        public int seq { get; set; }
        public int mv_seq { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public DateTime reg_date { get; set; }
    }
}
