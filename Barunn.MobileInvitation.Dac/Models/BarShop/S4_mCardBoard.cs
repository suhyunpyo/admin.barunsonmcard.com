using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_mCardBoard
    {
        public int Board_Seq { get; set; }
        public int? Order_Seq { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
