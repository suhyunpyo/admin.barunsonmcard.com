using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Branch_Member
    {
        public string uid { get; set; }
        public string pw { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string chk_mailservice { get; set; }
        public string sex { get; set; }
        public string jumin { get; set; }
        public DateTime? birth { get; set; }
        public string chk_event { get; set; }
        public string zip1 { get; set; }
        public string zip2 { get; set; }
        public string address { get; set; }
        public string addr_detail { get; set; }
        public string chk_penpal { get; set; }
        public DateTime? rdate { get; set; }
        public string phone { get; set; }
        public string hand_phone { get; set; }
        public string addressCHK { get; set; }
        public string phoneCHK { get; set; }
        public string nickname { get; set; }
        public string inpath { get; set; }
        public string chk_ad { get; set; }
        public DateTime? login_date { get; set; }
        public DateTime? login_date_latest { get; set; }
        public string job { get; set; }
        public string site_div { get; set; }
        public short? company_seq { get; set; }
        public string sample_chk { get; set; }
    }
}
