using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_printW
    {
        public int order_seq { get; set; }
        public DateTime proc_date1 { get; set; }
        public DateTime? proc_date2 { get; set; }
        public DateTime? proc_date3 { get; set; }
        public DateTime? proc_date4 { get; set; }
        public string admin_id { get; set; }
    }
}
