using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_WEIGHT
    {
        public string card_code { get; set; }
        public double card_weight1 { get; set; }
        public DateTime reg_date { get; set; }
        public DateTime? mod_date { get; set; }
    }
}
