using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Event_install
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string favorite_install { get; set; }
        public string desktop_install { get; set; }
        public int? favorite_cnt { get; set; }
        public int? desktop_cnt { get; set; }
        public DateTime reg_date { get; set; }
        public string isSelection { get; set; }
        public string isUsed { get; set; }
    }
}
