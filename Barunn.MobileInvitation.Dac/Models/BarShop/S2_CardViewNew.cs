using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardViewNew
    {
        public string isS2 { get; set; }
        public int card_seq { get; set; }
        public string card_div { get; set; }
        public string card_code { get; set; }
        public string card_code_str { get; set; }
        public string card_image { get; set; }
        public int? card_price { get; set; }
        public string card_name { get; set; }
        public string cardbrand { get; set; }
        public int? inpaper_seq { get; set; }
        public int? env_seq { get; set; }
        public string erp_code { get; set; }
    }
}
