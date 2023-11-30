using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CARD_LIKE
    {
        public int S2_CARD_LIKE_SEQ { get; set; }
        public int? CARD_SEQ { get; set; }
        public int? COMPANY_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string GUID { get; set; }
        public string UID { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
