using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BC_Career
    {
        public int Career_Seq { get; set; }
        public string Subject { get; set; }
        public string SubTitle { get; set; }
        public string Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Hidden { get; set; }
        public string AnyTime { get; set; }
        public DateTime RegDate { get; set; }
    }
}
