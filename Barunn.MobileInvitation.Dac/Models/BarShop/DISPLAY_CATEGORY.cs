using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DISPLAY_CATEGORY
    {
        public DISPLAY_CATEGORY()
        {
            CARD_DISPLAY_POLICies = new HashSet<CARD_DISPLAY_POLICY>();
            CUSTOM_CARD_DISPLAY_POLICies = new HashSet<CUSTOM_CARD_DISPLAY_POLICY>();
        }

        public int CATEGORY_SEQ { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string CATEGORY_DESC { get; set; }
        public int? DISPLAY_COUNT { get; set; }
        public int? CATEGORY_DIV { get; set; }

        public virtual ICollection<CARD_DISPLAY_POLICY> CARD_DISPLAY_POLICies { get; set; }
        public virtual ICollection<CUSTOM_CARD_DISPLAY_POLICY> CUSTOM_CARD_DISPLAY_POLICies { get; set; }
    }
}
