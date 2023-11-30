using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ETC_ORDER_GIFT_ITEM
    {
        public int Order_Seq { get; set; }
        public string card_erp_code { get; set; }
        public int card_seq { get; set; }
        public int order_count { get; set; }
        public int card_sale_price { get; set; }
        public string Use_Yn { get; set; }
        public DateTime Reg_Date { get; set; }
        public DateTime? Mod_Date { get; set; }
    }
}
