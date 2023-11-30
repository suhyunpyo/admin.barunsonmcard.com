using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BestRanking_new
    {
        public int id { get; set; }
        public string sales_Gubun { get; set; }
        public int? company_seq { get; set; }
        public short Rank { get; set; }
        public int Card_Seq { get; set; }
        public int Cnt { get; set; }
        public DateTime RegDate { get; set; }
        public string Gubun { get; set; }
        public string Gubun_data { get; set; }
        public string RankChangeGubun { get; set; }
        public string RankChangeNo { get; set; }
    }
}
