using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BANNER_MST
    {
        public int BANNER_SEQ { get; set; }
        public int? MD_SEQ { get; set; }
        public int? COMPANY_SEQ { get; set; }
        public string BANNER_TYPE { get; set; }
        public string BANNER_TITLE { get; set; }
        public string BANNER_IMAGE_URL { get; set; }
        public string PAGE_LINK_URL { get; set; }
        public string DISPLAY_YN { get; set; }
        public int? SORT_NUM { get; set; }
        public string REG_ID { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string UPD_ID { get; set; }
        public DateTime? UPD_DATE { get; set; }
    }
}
