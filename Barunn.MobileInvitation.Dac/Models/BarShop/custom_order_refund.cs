using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_refund
    {
        public int id { get; set; }
        public string order_tbl { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
        public int order_seq { get; set; }
        public string order_name { get; set; }
        public string settle_date { get; set; }
        public string settle_method { get; set; }
        public int? settle_price { get; set; }
        public string bank_name { get; set; }
        public string bank_account { get; set; }
        public string bank_user { get; set; }
        public int refund_price { get; set; }
        public string refund_date { get; set; }
        public string refund_msg { get; set; }
        public string refund_gubun { get; set; }
        public string admin_id { get; set; }
        public string REFUND_TYPE_CODE { get; set; }
        public DateTime reg_date { get; set; }
    }
}
