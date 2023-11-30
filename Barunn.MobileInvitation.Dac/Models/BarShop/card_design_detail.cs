using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class card_design_detail
    {
        public string card_code { get; set; }
        public short rtype { get; set; }
        public string isFPrint { get; set; }
        public short sx { get; set; }
        public short sy { get; set; }
        public short ex { get; set; }
        public short ey { get; set; }
        public byte alignment { get; set; }
    }
}
