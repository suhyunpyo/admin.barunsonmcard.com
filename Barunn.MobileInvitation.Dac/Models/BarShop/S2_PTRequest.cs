using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_PTRequest
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string com_name { get; set; }
        public string com_url { get; set; }
        public string person_name { get; set; }
        public string person_email { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string phone3 { get; set; }
        public string hand_phone1 { get; set; }
        public string hand_phone2 { get; set; }
        public string hand_phone3 { get; set; }
        public string zip1 { get; set; }
        public string zip2 { get; set; }
        public string address { get; set; }
        public string addr_detail { get; set; }
        public string com_contents { get; set; }
        public string com_message { get; set; }
        public string user_upfile { get; set; }
        public DateTime reg_date { get; set; }
        public string zip1_R { get; set; }
        public string zip2_R { get; set; }
        public string address_R { get; set; }
        public string addr_detail_R { get; set; }
    }
}
