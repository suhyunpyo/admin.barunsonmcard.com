using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CART
    {
        public int CART_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string ORDER_TYPE { get; set; }
        public string CART_OWNER { get; set; }
        public string OWNER_SESSION_ID { get; set; }
        public int CARD_NUM { get; set; }
        public int? UNIT_PRICE { get; set; }
        public int? DISCOUNT_RATE { get; set; }
        public DateTime REG_DATE { get; set; }
        public int CARD_SEQ { get; set; }
        public string CARD_OPTION { get; set; }
    }
}
