using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_TROUBLE
    {
        public int order_id { get; set; }
        public int? up_id { get; set; }
        public string trouble_type { get; set; }
        public string trouble_msg { get; set; }
        public string upfile { get; set; }
        public string admin_id { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
