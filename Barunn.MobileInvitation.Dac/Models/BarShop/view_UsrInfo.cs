using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_UsrInfo
    {
        public string site_name { get; set; }
        public string tbl_name { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string pwd { get; set; }
        public string jumin { get; set; }
        public string umail { get; set; }
        public string phone { get; set; }
        public string hand_phone { get; set; }
        public DateTime? reg_date { get; set; }
        public int? company_seq { get; set; }
    }
}
