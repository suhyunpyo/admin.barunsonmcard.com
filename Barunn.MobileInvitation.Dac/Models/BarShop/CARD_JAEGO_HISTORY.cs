using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_JAEGO_HISTORY
    {
        public string card_code { get; set; }
        public int chnum { get; set; }
        public string chcomment { get; set; }
        public DateTime reg_date { get; set; }
        public int? order_seq { get; set; }
        public int seq { get; set; }
        public int? now_jaego { get; set; }
        public string admin_id { get; set; }
    }
}
