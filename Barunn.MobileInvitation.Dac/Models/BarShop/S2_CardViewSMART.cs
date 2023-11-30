using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardViewSMART
    {
        public int Card_Seq { get; set; }
        public string Card_Code { get; set; }
        public int? company_seq { get; set; }
        public string isDisplay { get; set; }
        public string isJumun { get; set; }
        public int? card_price_customer { get; set; }
    }
}
