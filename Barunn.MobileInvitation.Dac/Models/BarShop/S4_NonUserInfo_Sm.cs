using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_NonUserInfo_Sm
    {
        public int seq { get; set; }
        public string uid { get; set; }
        public int company_seq { get; set; }
        public string sms_chk { get; set; }
        public DateTime reg_date { get; set; }
        public string inflow_route { get; set; }
        public int order_seq { get; set; }
    }
}
