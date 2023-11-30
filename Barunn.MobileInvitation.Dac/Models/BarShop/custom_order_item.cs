using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_item
    {
        public int id { get; set; }
        public int order_seq { get; set; }
        public int card_seq { get; set; }
        public string item_type { get; set; }
        public int? item_count { get; set; }
        public int? item_price { get; set; }
        public double? item_sale_price { get; set; }
        public double? discount_rate { get; set; }
        public string memo1 { get; set; }
        public int? addnum_price { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
