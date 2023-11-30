using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CopperPlateInfo
    {
        public int card_seq { get; set; }
        public string isFPrint { get; set; }
        public string card_code { get; set; }
        public int? pCount { get; set; }
        public DateTime? regdate { get; set; }
        public int? bCount { get; set; }
    }
}
