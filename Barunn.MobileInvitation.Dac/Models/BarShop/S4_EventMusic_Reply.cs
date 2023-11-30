using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventMusic_Reply
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public int reg_num { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string comment { get; set; }
        public DateTime reg_date { get; set; }
        public string var1 { get; set; }
        public int? card_seq { get; set; }
        public string inflow_route { get; set; }
    }
}
