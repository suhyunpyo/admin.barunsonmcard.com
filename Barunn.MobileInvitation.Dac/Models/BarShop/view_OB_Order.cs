using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_OB_Order
    {
        public int order_seq { get; set; }
        public string order_name { get; set; }
        public DateTime? order_date { get; set; }
        public int? order_price { get; set; }
        public int? settle_price { get; set; }
        public byte? settle_status { get; set; }
        public string settle_method { get; set; }
        public int? last_total_price { get; set; }
        public string posflag { get; set; }
        public string order_hphone { get; set; }
        public DateTime? settle_date { get; set; }
        public string isReceipt { get; set; }
        public string pg_resultinfo { get; set; }
        public double? pg_fee { get; set; }
        public int? company_Seq { get; set; }
        public int? delivery_price { get; set; }
        public int? jebon_price { get; set; }
        public string sticker_price { get; set; }
        public int? mini_price { get; set; }
        public int? etc_price { get; set; }
        public int? env_price { get; set; }
        public int? cont_price { get; set; }
        public int? option_price { get; set; }
        public int? reduce_price { get; set; }
        public string fticket_price { get; set; }
        public int? print_price { get; set; }
        public int? sasik_price { get; set; }
        public int card_seq { get; set; }
        public string item_type { get; set; }
        public int? item_count { get; set; }
        public int? item_price { get; set; }
        public double? item_sale_price { get; set; }
    }
}
