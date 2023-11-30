using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardViewN
    {
        public string isS2 { get; set; }
        public int card_seq { get; set; }
        public string card_code { get; set; }
        public string old_code { get; set; }
        public string card_div { get; set; }
        public string card_image { get; set; }
        public int? card_price { get; set; }
        public string card_name { get; set; }
        public string card_code_str { get; set; }
        public string brand_code { get; set; }
        public string brand_name { get; set; }
        public int? inpaper_seq { get; set; }
        public int? env_seq { get; set; }
        public string erp_code { get; set; }
        public int? Master_2Color { get; set; }
        public int isLaser { get; set; }
        public string PrintMethod { get; set; }
        public int isMasterDigital { get; set; }
        public int IsEmbo { get; set; }
        public int isLetterPress { get; set; }
        public int isInternalDigital { get; set; }
        public string FPrint_YORN { get; set; }
    }
}
