using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_BUY_CARD
    {
        public int buy_card_SEQ { get; set; }
        public string buy_card_UID { get; set; }
        public byte buy_STATUS { get; set; }
        public int CARD_SEQ { get; set; }
        public DateTime buy_card_DATE { get; set; }
        public DateTime buy_card_MAXDATE { get; set; }
        public int buy_card_price { get; set; }

        public virtual ewed_BUY_CARD_CONTENT ewed_BUY_CARD_CONTENT { get; set; }
        public virtual ewed_BUY_SETTLE_INFO ewed_BUY_SETTLE_INFO { get; set; }
    }
}
