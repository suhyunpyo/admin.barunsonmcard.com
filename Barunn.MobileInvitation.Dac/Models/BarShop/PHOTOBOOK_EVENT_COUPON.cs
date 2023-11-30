using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_EVENT_COUPON
    {
        public int seq { get; set; }
        public int order_seq { get; set; }
        public string sales_gubun { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string coupon_code { get; set; }
        public string send_yn { get; set; }
        public DateTime reg_date { get; set; }
    }
}
