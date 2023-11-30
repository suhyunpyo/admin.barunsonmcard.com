using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class wed_Notice
    {
        public int seq { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int viewcnt { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
        public string div { get; set; }
        public DateTime mdate { get; set; }
        public byte vlevel { get; set; }
        public string auth { get; set; }
    }
}
