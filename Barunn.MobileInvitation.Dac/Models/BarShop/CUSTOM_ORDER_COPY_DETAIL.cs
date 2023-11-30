using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ORDER_COPY_DETAIL
    {
        public long id { get; set; }
        public int order_seq { get; set; }
        public byte delivery_seq { get; set; }
        public string item_type { get; set; }
        public short item_seq { get; set; }
        public string isPItem { get; set; }
        public string item_title { get; set; }
        public string item_code { get; set; }
        public short item_count { get; set; }
        public string etc1 { get; set; }
        public short? pack_count { get; set; }
        public int? pack_weight { get; set; }
        public DateTime reg_date { get; set; }
    }
}
