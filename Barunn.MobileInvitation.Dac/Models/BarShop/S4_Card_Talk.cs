using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Card_Talk
    {
        public int seq { get; set; }
        public int tk_seq { get; set; }
        public byte tk_ansnum { get; set; }
        public int company_seq { get; set; }
        public int card_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string hand_phone1 { get; set; }
        public string hand_phone2 { get; set; }
        public string hand_phone3 { get; set; }
        public string chk_send_sms { get; set; }
        public string chk_send_mail { get; set; }
        public string contents { get; set; }
        public DateTime reg_date { get; set; }
        public string card_code { get; set; }
        public int? order_seq { get; set; }
    }
}
