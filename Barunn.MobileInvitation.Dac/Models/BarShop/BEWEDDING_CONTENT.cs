using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BEWEDDING_CONTENT
    {
        public int CONTENT_SEQ { get; set; }
        public int SOURCE_SEQ { get; set; }
        public string CONTENT_TYPE { get; set; }
        public string LOGO_DISPLAY_YN { get; set; }
        public string LOGO_TYPE { get; set; }
        public string IMAGE_TAG { get; set; }
        public string CONTENT_1 { get; set; }
        public string CONTENT_2 { get; set; }
        public string CONTENT_3 { get; set; }
        public string USE_YN { get; set; }
        public string DEL_YN { get; set; }
        public string CONTENT_LINK_URL { get; set; }
        public int? CONTENT_CLICK_CNT { get; set; }
        public string INTERNAL_LINK_YORN { get; set; }
        public int? SORTING_NUM { get; set; }
        public int? PREVIEW_SORTING_NUM { get; set; }
        public DateTime? REG_DATE { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_ID { get; set; }
    }
}
