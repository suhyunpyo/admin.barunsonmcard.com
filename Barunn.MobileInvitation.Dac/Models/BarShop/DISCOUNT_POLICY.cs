using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DISCOUNT_POLICY
    {
        public int DISCOUNT_SEQ { get; set; }
        public int? MIN_PRICE { get; set; }
        public int? MAX_PRICE { get; set; }
        public int? MIN_COUNT { get; set; }
        public int? MAX_COUNT { get; set; }
        public double? DISCOUNT_RATE { get; set; }
        public int CARD_CATEGORY_SEQ { get; set; }

        public virtual CARD_CATEGORY CARD_CATEGORY_SEQNavigation { get; set; }
    }
}
