using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_Order_Settle
    {
        public int order_seq { get; set; }
        public string order_div { get; set; }
        public int? settle_price { get; set; }
        public string transaction_no { get; set; }
        public byte? result_code { get; set; }
        public string result_msg { get; set; }
        public byte? payment_way { get; set; }
        public string auth_no { get; set; }
        public string company_code { get; set; }
        public string auth_date { get; set; }
        public string auth_time { get; set; }
        public string order_no { get; set; }
        public string isdacom { get; set; }
        public string PGcancel_date { get; set; }
        public string virReturn_date { get; set; }
    }
}
