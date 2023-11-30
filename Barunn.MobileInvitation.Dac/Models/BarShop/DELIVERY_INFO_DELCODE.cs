using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DELIVERY_INFO_DELCODE
    {
        public int order_seq { get; set; }
        public int delivery_id { get; set; }
        public string delivery_code_num { get; set; }
        public string delivery_com { get; set; }
        public int id { get; set; }
        public string isHJ { get; set; }
        public DateTime? DELCODE_REG_DATE { get; set; }
    }
}
