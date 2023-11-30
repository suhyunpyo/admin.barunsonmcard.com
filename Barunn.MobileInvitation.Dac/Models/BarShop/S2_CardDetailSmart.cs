using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardDetailSmart
    {
        public int card_seq { get; set; }
        public string card_category { get; set; }
        public string isNew { get; set; }
        public string isBest { get; set; }
        public string isPlan { get; set; }
        public string composition { get; set; }
        public string summary { get; set; }
        public string origin { get; set; }
        public string etc1 { get; set; }
        public string etc2 { get; set; }
        public int? min_onum { get; set; }
        public string option_str1 { get; set; }
        public string option_str2 { get; set; }
        public string option_str3 { get; set; }
        public string option_str4 { get; set; }
        public string option_str5 { get; set; }
        public int? card_sale_price { get; set; }
    }
}
