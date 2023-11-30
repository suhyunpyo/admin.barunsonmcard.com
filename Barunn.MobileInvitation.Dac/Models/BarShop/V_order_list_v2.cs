using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class V_order_list_v2
    {
        public string order_case { get; set; }
        public int order_seq { get; set; }
        public DateTime? order_Date { get; set; }
        public int? status_seq { get; set; }
        public int? settle_price { get; set; }
        public string settle_method { get; set; }
        public string pg_tid { get; set; }
        public string pg_shopid { get; set; }
        public string pg_resultinfo { get; set; }
        public string pg_resultinfo2 { get; set; }
        public DateTime? settle_date { get; set; }
        public DateTime? delivery_date { get; set; }
        public string delivery_com { get; set; }
        public string member_id { get; set; }
        public int? company_seq { get; set; }
        public string MEMBER_NAME { get; set; }
        public string member_email { get; set; }
        public int? up_order_seq { get; set; }
    }
}
