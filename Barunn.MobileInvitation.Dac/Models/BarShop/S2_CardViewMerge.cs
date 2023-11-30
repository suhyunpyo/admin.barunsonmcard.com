using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardViewMerge
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
        public short? brand { get; set; }
        public string card_code_str { get; set; }
        public string env_code { get; set; }
        public string inpaper_code { get; set; }
        public string isHanji { get; set; }
        public int? master_2color { get; set; }
        public int isLaser { get; set; }
    }
}
