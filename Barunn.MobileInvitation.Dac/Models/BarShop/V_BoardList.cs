using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class V_BoardList
    {
        public string boardid { get; set; }
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string writer { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int display_order { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int? viewcnt { get; set; }
        public DateTime reg_date { get; set; }
    }
}
