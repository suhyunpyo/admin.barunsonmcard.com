using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_BHANDS_EVENT_BANNER
    {
        public int SEQ { get; set; }
        public int MD_SEQ { get; set; }
        public int SORTING_NUM { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string MD_TITLE { get; set; }
        public string SUB_TITLE { get; set; }
        public string LINK_URL { get; set; }
        public string IMGFILE_PATH { get; set; }
        public string BACKGROUND_COLOR { get; set; }
        public string EVENT_OPEN_YORN { get; set; }
        public string LINK_TARGET { get; set; }
        public string VIEW_DIV { get; set; }
        public string JEHU_VIEW_DIV { get; set; }
        public string WING_BANNER_YN { get; set; }
        public string BAND_BANNER_YN { get; set; }
        public string CREATE_ID { get; set; }
        public DateTime CREATE_DATE { get; set; }
    }
}
