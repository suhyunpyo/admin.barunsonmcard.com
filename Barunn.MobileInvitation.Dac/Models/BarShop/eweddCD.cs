using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class eweddCD
    {
        public int ewedd_id { get; set; }
        public string order_seq { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public string useremail { get; set; }
        public string settle_date { get; set; }
        public string settle_cancle_date { get; set; }
        public string settle_price { get; set; }
        public string order_date { get; set; }
        public string productID { get; set; }
        public double? writenum { get; set; }
        public double? visitwrite { get; set; }
        public double? visitnum { get; set; }
        public double? ewedd_sum { get; set; }
        public string result { get; set; }
        public string order_result { get; set; }
        public string settle_status { get; set; }
        public string status_seq { get; set; }
        public bool send_ok { get; set; }
    }
}
