using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SmartADNotice
    {
        public int seq { get; set; }
        public string writer { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int? viewcnt { get; set; }
        public string notice_div { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public DateTime reg_date { get; set; }
        public string display_YN { get; set; }
    }
}
