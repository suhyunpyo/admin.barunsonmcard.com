using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CARD_FREE_GIFT
    {
        public int FREE_GIFT_SEQ { get; set; }
        public int? CARD_SEQ { get; set; }
        public string CARD_CODE { get; set; }
        public int? QTY { get; set; }
        public string ITEM_TYPE { get; set; }
        public string FREE_GIFT_DESC { get; set; }
        public string USE_YORN { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public int? LIMIT_ORDER_PRICE { get; set; }
        public int? LIMIT_ORDER_COUNT { get; set; }
        public string SALES_GUBUN { get; set; }
        public string LIMIT_ORDER_TYPE_STR { get; set; }
        public string LIMIT_DELIVERY_REGION_STR { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string LIMIT_DELIVERY_GU_STR { get; set; }
        public string LIMIT_CARD_BRAND { get; set; }
        public int item_count { get; set; }
    }
}
