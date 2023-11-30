using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardRank
    {
        public int? Card_Seq { get; set; }
        public int? Company_Seq { get; set; }
        public int? Rank { get; set; }
        public string Rank_UpDown { get; set; }
        public byte? Rank_Change { get; set; }
        public string Rank_Div { get; set; }
        public DateTime RegDate { get; set; }
    }
}
