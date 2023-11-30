using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_Notice_daum
    {
        public int seq { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int? viewcnt { get; set; }
        public string div { get; set; }
        public DateTime? mdate { get; set; }
    }
}
