using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_DISCOUNT_RATE_WED
    {
        public int id { get; set; }
        public string card_group { get; set; }
        public byte company { get; set; }
        public string disrate_type { get; set; }
        public string card_kind { get; set; }
        public int card_price { get; set; }
        public short min_count { get; set; }
        public short max_count { get; set; }
        public double discount_rate { get; set; }
    }
}
