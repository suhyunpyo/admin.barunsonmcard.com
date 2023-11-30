using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_BUY_SETTLE_INFO
    {
        public int buy_card_SEQ { get; set; }
        public string TID { get; set; }
        public string ResultCode { get; set; }
        public string ResultMsg { get; set; }
        public string PayMethod { get; set; }
        public string AuthCode { get; set; }
        public string CardQuota { get; set; }
        public string QuotaInterest { get; set; }
        public string CardCode { get; set; }
        public string PGAuthDate { get; set; }
        public string PGAuthTime { get; set; }
        public string OrderNO { get; set; }

        public virtual ewed_BUY_CARD buy_card_SEQNavigation { get; set; }
    }
}
