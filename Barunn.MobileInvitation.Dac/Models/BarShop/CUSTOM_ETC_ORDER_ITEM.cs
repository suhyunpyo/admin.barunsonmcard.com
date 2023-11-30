using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ETC_ORDER_ITEM
    {
        public int card_seq { get; set; }
        public int order_seq { get; set; }
        public byte seq { get; set; }
        public int order_count { get; set; }
        public int card_price { get; set; }
        public double card_sale_price { get; set; }
        public string order_tbl { get; set; }
        public string isChange { get; set; }
        public string card_opt { get; set; }
        public string SampleBook_ID { get; set; }
        public byte? SampleBook_Status { get; set; }
    }
}
