using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class VSTAT_CARD
    {
        public int card_seq { get; set; }
        public string card_type { get; set; }
        public string vdate { get; set; }
        public int vcnt { get; set; }
        public short company_seq { get; set; }
    }
}
