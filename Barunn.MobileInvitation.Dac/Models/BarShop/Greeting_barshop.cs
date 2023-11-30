using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Greeting_barshop
    {
        public int GREETING_SEQ { get; set; }
        public string GREETING_NAME { get; set; }
        public string GREETING_CONTENT { get; set; }
        public string DISPLAY_YES_OR_NO { get; set; }
        public int? USED_COUNT { get; set; }
        public DateTime? REGIST_DATE { get; set; }
        public DateTime? LAST_UPDATE { get; set; }
        public string RECOMEND_YES_OR_NO { get; set; }
        public string USE_IMAGE { get; set; }
        public int GREETING_CATEGORY_SEQ { get; set; }
    }
}
