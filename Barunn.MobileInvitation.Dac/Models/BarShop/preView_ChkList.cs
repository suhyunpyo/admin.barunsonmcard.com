using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class preView_ChkList
    {
        public short lid { get; set; }
        public byte? sid { get; set; }
        public byte? checkNum { get; set; }
        public string checkVar { get; set; }
        public string order_type { get; set; }
        public string isCard { get; set; }
    }
}
