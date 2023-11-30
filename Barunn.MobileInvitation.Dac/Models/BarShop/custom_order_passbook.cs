using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_passbook
    {
        public int id { get; set; }
        public string order_tbl { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
        public int order_seq { get; set; }
        public string sender_name { get; set; }
        public int send_price { get; set; }
        public string send_msg { get; set; }
        public string passbook_type_code { get; set; }
        public string admin_id { get; set; }
        public DateTime reg_date { get; set; }
    }
}
