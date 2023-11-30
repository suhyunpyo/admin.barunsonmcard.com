using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_plist_svg
    {
        public int pid { get; set; }
        public int? order_seq { get; set; }
        public DateTime? reg_date { get; set; }
        public string err_msg { get; set; }
    }
}
