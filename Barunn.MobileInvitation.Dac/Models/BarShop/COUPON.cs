using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COUPON
    {
        public int id { get; set; }
        public string code { get; set; }
        public int? order_seq { get; set; }
        public int discount_Value { get; set; }
        public string use_type { get; set; }
        public string isJaehu { get; set; }
        public string isUse { get; set; }
        public int? company_seq { get; set; }
        public string discount_type { get; set; }
        public DateTime? reg_date { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string isUsable { get; set; }
        public int? seq_num { get; set; }
        public string description { get; set; }
        public string tmp1 { get; set; }
        public string tmp2 { get; set; }
    }
}
