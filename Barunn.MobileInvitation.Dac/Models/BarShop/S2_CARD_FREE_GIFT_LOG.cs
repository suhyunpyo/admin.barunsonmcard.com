using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CARD_FREE_GIFT_LOG
    {
        public int LOG_SEQ { get; set; }
        public int? FREE_GIFT_SEQ { get; set; }
        public int? CARD_SEQ { get; set; }
        public int? ORDER_SEQ { get; set; }
        public string UID { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
