using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVENT_ETLAND
    {
        public int id { get; set; }
        public string c_uid { get; set; }
        public string c_email { get; set; }
        public string c_name { get; set; }
        public string c_phone { get; set; }
        public string c_hphone { get; set; }
        public string c_addr { get; set; }
        public string c_weddingday { get; set; }
        public string coupon_code { get; set; }
        public string admin_memo { get; set; }
        public string isUse { get; set; }
        public DateTime reg_date { get; set; }
    }
}
