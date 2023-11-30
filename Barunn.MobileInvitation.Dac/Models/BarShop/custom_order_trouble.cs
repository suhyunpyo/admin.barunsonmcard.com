using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_trouble
    {
        public int order_seq { get; set; }
        public string trouble_type { get; set; }
        public string upfile { get; set; }
        public string admin_id { get; set; }
        public string QcNumber { get; set; }
        public string RequestOutDate { get; set; }
        public string trouble_comment { get; set; }
        public DateTime reg_date { get; set; }
    }
}
