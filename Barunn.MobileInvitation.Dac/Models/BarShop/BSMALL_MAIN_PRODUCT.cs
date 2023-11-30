using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BSMALL_MAIN_PRODUCT
    {
        public int SEQ { get; set; }
        public int MD_SEQ { get; set; }
        public string GRADE { get; set; }
        public string GUBUN { get; set; }
        public string BRAND_KIND { get; set; }
        public int SORTING_NUM { get; set; }
        public int? CARD_SEQ { get; set; }
        public string IMGFILE_PATH { get; set; }
        public string CUSTOM_IMG { get; set; }
        public string LINK_URL { get; set; }
        public string LINK_TARGET { get; set; }
        public int CLICK_COUNT { get; set; }
        public string VIEW_DIV { get; set; }
        public string JEHU_VIEW_DIV { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_UID { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string UPDATED_UID { get; set; }
    }
}
