using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class card_design
    {
        public string card_code { get; set; }
        public int pagesizeW { get; set; }
        public int pagesizeH { get; set; }
        public string design_type { get; set; }
        public string isFPrint { get; set; }
        public string isMapFPrint { get; set; }
        public string isBackground { get; set; }
    }
}
