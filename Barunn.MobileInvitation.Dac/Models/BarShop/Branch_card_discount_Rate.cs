using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Branch_card_discount_Rate
    {
        public int id { get; set; }
        public int company_seq { get; set; }
        public string disrate_type { get; set; }
        public int card_price { get; set; }
        public short min_count { get; set; }
        public short max_count { get; set; }
        public double discount_rate { get; set; }
        public byte? company { get; set; }
    }
}
