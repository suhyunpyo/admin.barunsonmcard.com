using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVT_BHANDS_COUPLES_VOTE
    {
        public int Idx { get; set; }
        public int CouplesIdx { get; set; }
        public string UID { get; set; }
        public DateTime RegDate { get; set; }
    }
}
