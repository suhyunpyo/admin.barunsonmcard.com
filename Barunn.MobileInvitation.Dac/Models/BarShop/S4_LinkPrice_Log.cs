using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_LinkPrice_Log
    {
        public string LPINFO { get; set; }
        public string YYYYMMDD { get; set; }
        public string HHMISS { get; set; }
        public int order_seq { get; set; }
        public string card_code { get; set; }
        public int order_count { get; set; }
        public int settle_price { get; set; }
        public string card_name { get; set; }
        public string category_name { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string user_ip { get; set; }
        public DateTime reg_date { get; set; }
    }
}
