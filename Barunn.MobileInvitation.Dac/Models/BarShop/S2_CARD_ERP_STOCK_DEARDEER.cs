using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CARD_ERP_STOCK_DEARDEER
    {
        public int S2_CARD_ERP_STOCK_SEQ { get; set; }
        public string CARD_CODE { get; set; }
        public string CARD_CODE_ERP { get; set; }
        public string CARD_TYPE_NAME { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRAND_NAME_OLD { get; set; }
        public string ORIGIN_NAME { get; set; }
        public string PRODUCTION_STATUS_NAME { get; set; }
        public decimal? CLOSING_COST { get; set; }
        public int? CONSUMER_PRICE { get; set; }
        public int? CARD_SET_PRICE { get; set; }
        public int? INVENTORY_CURRENT_QTY { get; set; }
        public int? INVENTORY_REQUEST_QTY { get; set; }
        public int? INVENTORY_AVAILABLE_QTY { get; set; }
        public int? INVENTORY_NOT_MAKE_QTY { get; set; }
        public int? INVENTORY_CHINA_QTY { get; set; }
        public int? INVENTORY_MOVING_QTY { get; set; }
        public int? TOTAL_SALE_PRICE_30_DAY { get; set; }
        public int? TOTAL_SALE_PRICE_90_DAY { get; set; }
        public int? TOTAL_SALE_PRICE_180_DAY { get; set; }
        public int? TOTAL_SALE_PRICE_365_DAY { get; set; }
        public DateTime? ERP_FIRST_REG_DATE { get; set; }
        public DateTime? RELEASE_DATE { get; set; }
        public DateTime? MOD_DATE { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
