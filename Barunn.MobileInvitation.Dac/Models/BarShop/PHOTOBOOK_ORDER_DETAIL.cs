using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_ORDER_DETAIL
    {
        public int id { get; set; }
        public string product_order_id { get; set; }
        public int order_id { get; set; }
        public short seq { get; set; }
        public string prod_code { get; set; }
        public string user_title { get; set; }
        public short prod_page { get; set; }
        public int item_count { get; set; }
        public int? item_price { get; set; }
        public int item_sale_price { get; set; }
        public string item_option { get; set; }
        public string product_state { get; set; }
        public string process_state { get; set; }
        public string express_id { get; set; }
        public string delivery_code { get; set; }
        public string print_date { get; set; }
        public string p_packing_date { get; set; }
        public string p_delivery_date { get; set; }
        public string thumbnail_url { get; set; }
        public string isput { get; set; }
        public string iscomment { get; set; }
        public string isopen { get; set; }
    }
}
