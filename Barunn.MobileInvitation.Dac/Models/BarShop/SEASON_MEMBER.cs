using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SEASON_MEMBER
    {
        public string uid { get; set; }
        public string pwd { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string jumin { get; set; }
        public string zip1 { get; set; }
        public string zip2 { get; set; }
        public string address { get; set; }
        public string addr_detail { get; set; }
        public string phone { get; set; }
        public string hand_phone { get; set; }
        public string chk_sms { get; set; }
        public string chk_mailservice { get; set; }
        public string site_div { get; set; }
        public int? company_seq { get; set; }
        public DateTime reg_date { get; set; }
        public DateTime? mod_date { get; set; }
        public string out_yorn { get; set; }
        public DateTime? out_date { get; set; }
    }
}
