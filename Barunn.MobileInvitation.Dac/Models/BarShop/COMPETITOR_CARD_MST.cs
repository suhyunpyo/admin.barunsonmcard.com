using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COMPETITOR_CARD_MST
    {
        public int SEQ { get; set; }
        public string SITE_NAME { get; set; }
        public int? CARD_SEQ { get; set; }
        public string CARD_CODE { get; set; }
        public string CARD_NAME { get; set; }
        public decimal? CARD_PRICE { get; set; }
        public decimal? DISCOUNT_RATE { get; set; }
        public string CARD_IMAGE { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
