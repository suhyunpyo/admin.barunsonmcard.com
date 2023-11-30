using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Premier_Movie
    {
        public int seq { get; set; }
        public int order_seq { get; set; }
        public int company_seq { get; set; }
        public string qrcode_image { get; set; }
        public string movie_url { get; set; }
        public string etc_comment { get; set; }
        public string isComplete { get; set; }
        public DateTime? regdate { get; set; }
    }
}
