using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ORDER_COPY
    {
        public int order_seq { get; set; }
        public byte delivery_seq { get; set; }
        public string delivery_method { get; set; }
        public string jebon_str { get; set; }
        public string embo_str { get; set; }
        public string print_str { get; set; }
        public string isRisk { get; set; }
        public string isQuick { get; set; }
    }
}
