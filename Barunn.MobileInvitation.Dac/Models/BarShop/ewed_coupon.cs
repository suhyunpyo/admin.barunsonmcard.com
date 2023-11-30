using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_coupon
    {
        public string coupon_code { get; set; }
        public int? order_seq { get; set; }
        public short coupon_price { get; set; }
        public string use_type { get; set; }
        public DateTime? reg_date { get; set; }
        public string isUsable { get; set; }
        public int seq_num { get; set; }
    }
}
