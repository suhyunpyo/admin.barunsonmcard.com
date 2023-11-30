using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_SAMPLE_ORDER_ITEM
    {
        public int CARD_SEQ { get; set; }
        public int SAMPLE_ORDER_SEQ { get; set; }
        public int CARD_PRICE { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string isChu { get; set; }
        public string md_recommend { get; set; }
        public int? sort { get; set; }
        public string price_info { get; set; }

        public virtual CUSTOM_SAMPLE_ORDER SAMPLE_ORDER_SEQNavigation { get; set; }
    }
}
