using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DELIVERY_INFO_DETAIL
    {
        public int ID { get; set; }
        public int order_seq { get; set; }
        public int delivery_id { get; set; }
        public string item_type { get; set; }
        public string item_title { get; set; }
        public int item_id { get; set; }
        public int item_count { get; set; }
    }
}
