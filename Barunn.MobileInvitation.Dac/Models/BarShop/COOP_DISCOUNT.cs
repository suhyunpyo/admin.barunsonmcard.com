using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COOP_DISCOUNT
    {
        public string disrate_type { get; set; }
        public int card_seq { get; set; }
        public int company_Seq { get; set; }
        public double discount_rate { get; set; }
        public DateTime mod_date { get; set; }
        public string cardbrand { get; set; }
    }
}
