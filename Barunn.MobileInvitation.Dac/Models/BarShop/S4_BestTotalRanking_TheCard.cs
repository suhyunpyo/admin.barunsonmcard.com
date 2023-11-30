using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_BestTotalRanking_TheCard
    {
        public string Gubun_date { get; set; }
        public string Gubun { get; set; }
        public string SubGubun { get; set; }
        public short RankNo { get; set; }
        public int Card_Seq { get; set; }
        public int Cnt { get; set; }
        public string RankChangeGubun { get; set; }
        public string RankChangeNo { get; set; }
        public DateTime RegDate { get; set; }
    }
}
