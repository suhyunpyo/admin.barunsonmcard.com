using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardViewChasu
    {
        public string isS2 { get; set; }
        public int card_seq { get; set; }
        public string card_code { get; set; }
        public string old_code { get; set; }
        public string erp_code { get; set; }
        public string card_div { get; set; }
        public string card_image { get; set; }
        public int? card_price { get; set; }
        public string card_name { get; set; }
        public string card_code_str { get; set; }
        public string IsInPaper { get; set; }
        public string print_group { get; set; }
        public int print_sizeH { get; set; }
        public string isDigital { get; set; }
    }
}
