using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Tiara_Event
    {
        public int id { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string hphone { get; set; }
        public string email { get; set; }
        public string etc_info_s { get; set; }
        public int? etc_info_l { get; set; }
        public DateTime reg_date { get; set; }
    }
}
