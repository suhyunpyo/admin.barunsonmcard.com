using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BEWEDDING_NAVIGATION
    {
        public int NAVIGATION_SEQ { get; set; }
        public string SITE_NAME { get; set; }
        public string MAIN_TITLE { get; set; }
        public string URL { get; set; }
        public string IMAGE_URL { get; set; }
        public string BACKGROUND_IMAGE_URL { get; set; }
        public string INTERNAL_LINK_YORN { get; set; }
        public string DISPLAY_YORN { get; set; }
        public string DELETE_YORN { get; set; }
        public int? SORT_NUM { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
