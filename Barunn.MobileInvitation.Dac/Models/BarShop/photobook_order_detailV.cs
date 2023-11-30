using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class photobook_order_detailV
    {
        public int order_id { get; set; }
        public string product_order_id { get; set; }
        public string prod_code { get; set; }
        public short prod_page { get; set; }
        public int item_count { get; set; }
        public int item_sale_price { get; set; }
        public string item_option { get; set; }
        public string thumbnail_url { get; set; }
        public string prod_name { get; set; }
        public string cover_name { get; set; }
        public string coating_yn { get; set; }
        public string prod_cate { get; set; }
        public string prod_cate2 { get; set; }
        public string cover_style { get; set; }
        public string makecom_code { get; set; }
        public string prod_size { get; set; }
        public string delivery_code { get; set; }
        public string p_delivery_date { get; set; }
    }
}
