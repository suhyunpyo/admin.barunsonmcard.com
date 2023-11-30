using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_McardClickCount
    {
        public int Seq { get; set; }
        public int? Company_Seq { get; set; }
        public int? OrderSeq { get; set; }
        public string Gubun { get; set; }
        public string Ip { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
