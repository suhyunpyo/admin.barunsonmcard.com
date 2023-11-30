using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CALLCENTER_SIGNAL_LAMP_MST
    {
        public int SEQ { get; set; }
        public int? CALL_BUSY { get; set; }
        public int? CALL_GOOD { get; set; }
        public int? QNA_BUSY { get; set; }
        public int? QNA_GOOD { get; set; }
        public int? SMS_BUSY { get; set; }
        public int? SMS_GOOD { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
