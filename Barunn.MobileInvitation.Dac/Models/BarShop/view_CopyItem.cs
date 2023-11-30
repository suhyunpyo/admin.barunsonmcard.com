using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_CopyItem
    {
        public long id { get; set; }
        public string item_type { get; set; }
        public string item_title { get; set; }
        public string plc_code { get; set; }
        public short item_count { get; set; }
        public string item_code { get; set; }
        public short item_seq { get; set; }
        public int order_seq { get; set; }
        public byte delivery_seq { get; set; }
    }
}
