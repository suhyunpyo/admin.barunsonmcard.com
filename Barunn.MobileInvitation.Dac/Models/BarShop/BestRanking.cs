using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BestRanking
    {
        public string sales_Gubun { get; set; }
        public short? Rank { get; set; }
        public int? Card_Seq { get; set; }
        public string Card_Code { get; set; }
        public string Card_img_ms { get; set; }
        public short? Cnt { get; set; }
        public string Ranking { get; set; }
        public DateTime? RegDate { get; set; }
        public string Gubun { get; set; }
    }
}
