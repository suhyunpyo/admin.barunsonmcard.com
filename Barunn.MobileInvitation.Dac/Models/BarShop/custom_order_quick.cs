using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_quick
    {
        public int id { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
        public int order_seq { get; set; }
        public int quick_price { get; set; }
        public string sender_code { get; set; }
        public string receiver_address { get; set; }
        public string quick_reason { get; set; }
        public string quick_type { get; set; }
        public string quick_dept { get; set; }
        public string quick_driver_no { get; set; }
        public string admin_id { get; set; }
        public DateTime reg_date { get; set; }
    }
}
