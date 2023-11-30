using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_Event_flow
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public string q_comment { get; set; }
        public string a_comment { get; set; }
        public DateTime reg_date { get; set; }
    }
}
