using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_BASKET
    {
        public int id { get; set; }
        public string site_code { get; set; }
        public string member_id { get; set; }
        public string prod_code { get; set; }
        public string user_title { get; set; }
        public short prod_page { get; set; }
        public string prod_order_id { get; set; }
        public int? prod_seq { get; set; }
        public int? prod_price { get; set; }
        public short? prod_disrate { get; set; }
        public int? prod_sale_price { get; set; }
        public string disrate_type { get; set; }
        public short item_count { get; set; }
        public DateTime? reg_date { get; set; }
        public string status { get; set; }
        public short? seq { get; set; }
        public DateTime? mod_date { get; set; }
    }
}
