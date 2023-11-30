using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class MCARD_INVITATION_FLOW
    {
        public string PCM { get; set; }
        public string UID { get; set; }
        public int? STEP0 { get; set; }
        public int? STEP1 { get; set; }
        public int? STEP2 { get; set; }
        public int? STEP3 { get; set; }
        public int? STEP4 { get; set; }
        public string MEM_GB { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
    }
}
