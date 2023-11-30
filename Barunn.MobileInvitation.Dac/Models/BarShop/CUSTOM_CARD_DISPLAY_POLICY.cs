using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_CARD_DISPLAY_POLICY
    {
        public int CARD_SEQ { get; set; }
        public int? DISPLAY_ORDER { get; set; }
        public int CATEGORY_SEQ { get; set; }

        public virtual DISPLAY_CATEGORY CATEGORY_SEQNavigation { get; set; }
    }
}
