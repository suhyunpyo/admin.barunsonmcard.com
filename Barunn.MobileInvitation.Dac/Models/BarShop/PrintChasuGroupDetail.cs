using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PrintChasuGroupDetail
    {
        public int Seq { get; set; }
        public string GroupCode { get; set; }
        public short GroupCodeSeq { get; set; }
        public string CardCode { get; set; }
        public string PrintType { get; set; }
    }
}
