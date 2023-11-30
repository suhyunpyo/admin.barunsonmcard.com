using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Yoosami
    {
        public int Seq { get; set; }
        public DateTime? RegDate { get; set; }
        public int? Visit { get; set; }
        public string Site { get; set; }
        public string Source { get; set; }
    }
}
