using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_MEMBER
    {
        public string uid { get; set; }
        public string pw { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string chk_mailservice { get; set; }
        public string sex { get; set; }
        public string jumin { get; set; }
        public string birth { get; set; }
        public string isLunar { get; set; }
        public string chk_event { get; set; }
        public string zip1 { get; set; }
        public string zip2 { get; set; }
        public string address { get; set; }
        public string addr_detail { get; set; }
        public DateTime? rdate { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string phone3 { get; set; }
        public string hand_phone1 { get; set; }
        public string hand_phone2 { get; set; }
        public string hand_phone3 { get; set; }
        public string addressCHK { get; set; }
        public string phoneCHK { get; set; }
        public string chk_ad { get; set; }
        public DateTime? login_date { get; set; }
        public DateTime? login_date_latest { get; set; }
        public string job { get; set; }
        public string site_div { get; set; }
        public string coupon_code { get; set; }
        public string ustatus { get; set; }
        public string member_class { get; set; }
        public string isAutoClass { get; set; }
    }
}
