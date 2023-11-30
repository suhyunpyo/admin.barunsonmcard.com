using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_nmCardBoard
    {
        public int Board_Seq { get; set; }
        public int? Order_Seq { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public string IP { get; set; }
        public string HTTP_USER_AGENT { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
