using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class The_Ewed_MyBGM
    {
        public string member_id { get; set; }
        public int bgm_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string idx { get; set; }
        public byte? settle_status { get; set; }
        public int? settle_price { get; set; }
        public DateTime? settle_date { get; set; }
        public string settle_method { get; set; }
        public string pg_resultinfo { get; set; }
        public string my_state { get; set; }
        public DateTime? src_erp_date { get; set; }
        public string Sales_Gubun { get; set; }
        public string pg_tid { get; set; }
    }
}
