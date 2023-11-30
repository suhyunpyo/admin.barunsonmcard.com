using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_MD_Choice
    {
        public int SEQ { get; set; }
        public int MD_SEQ { get; set; }
        public int SORTING_NUM { get; set; }
        public int? CARD_SEQ { get; set; }
        public string CARD_TEXT { get; set; }
        public string MD_TITLE { get; set; }
        public string MD_CONTENT { get; set; }
        public string MD_DESC { get; set; }
        public string IMGFILE_PATH { get; set; }
        public string CUSTOM_IMG { get; set; }
        public string LINK_URL { get; set; }
        public string LINK_TARGET { get; set; }
        public int CLICK_COUNT { get; set; }
        public string VIEW_DIV { get; set; }
        public string JEHU_VIEW_DIV { get; set; }
        public string EVENT_OPEN_YORN { get; set; }
        public string SNS_SHARE_YORN { get; set; }
        public string SNS_SHARE_IMAGE_URL { get; set; }
        public string ADMIN_ID { get; set; }
        public int? RECOM_NUM { get; set; }
        public string GRADE { get; set; }
        public string GUBUN { get; set; }
        public string BRAND_KIND { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public DateTime REG_DATE { get; set; }
        public string SNS_TYPE { get; set; }
    }
}
