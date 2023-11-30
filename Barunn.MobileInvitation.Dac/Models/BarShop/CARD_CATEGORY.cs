using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_CATEGORY
    {
        public CARD_CATEGORY()
        {
            DISCOUNT_POLICies = new HashSet<DISCOUNT_POLICY>();
        }

        public int CARD_CATEGORY_SEQ { get; set; }
        public string CATEGORY_NAME { get; set; }
        public int CATEGORY_UPPER_CODE { get; set; }
        public string CATEGORY_DESCRIPTION { get; set; }
        public DateTime? REGIST_DATE { get; set; }
        public DateTime? LAST_UPDATE { get; set; }
        public string CATEGORY_USED { get; set; }
        public string SAMPLE_SUPPLY { get; set; }
        public int COUNT_IMG_DISPLAY { get; set; }
        public int MINIMUM_ORDER { get; set; }
        public int? DEPTH { get; set; }
        public string PRODUCE_YEAR { get; set; }
        public string CATEGORY_DIV { get; set; }

        public virtual ICollection<DISCOUNT_POLICY> DISCOUNT_POLICies { get; set; }
    }
}
