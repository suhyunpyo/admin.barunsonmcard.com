using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class photobook_order_detail_erp
    {
        public int order_id { get; set; }
        public string product_order_id { get; set; }
        public string prod_code { get; set; }
        public int item_count { get; set; }
        public int? item_price { get; set; }
        public int item_sale_price { get; set; }
        public string erp_code { get; set; }
    }
}
