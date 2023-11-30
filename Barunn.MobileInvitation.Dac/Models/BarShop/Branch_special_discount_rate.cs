using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Branch_special_discount_rate
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public int card_seq { get; set; }
        public double discount_rate { get; set; }
        public DateTime rdate { get; set; }
    }
}
