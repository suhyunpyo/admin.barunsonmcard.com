using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_eCardBoard
    {
        public int Board_Seq { get; set; }
        public int? Order_Seq { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime? RegDate { get; set; }
        public string Pwd { get; set; }
    }
}
