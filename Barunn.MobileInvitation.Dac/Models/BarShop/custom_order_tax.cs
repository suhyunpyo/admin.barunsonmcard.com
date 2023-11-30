using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_tax
    {
        public int id { get; set; }
        public int order_seq { get; set; }
        public string upfile { get; set; }
        public string msg { get; set; }
        public string admin_id { get; set; }
        public DateTime reg_date { get; set; }
    }
}
