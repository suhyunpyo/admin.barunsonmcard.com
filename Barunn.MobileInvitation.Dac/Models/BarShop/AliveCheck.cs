using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class AliveCheck
    {
        public DateTime? RegDate { get; set; }
        public string Site_Gubun { get; set; }
        public short Cnt { get; set; }
        public string Gubun { get; set; }
    }
}
