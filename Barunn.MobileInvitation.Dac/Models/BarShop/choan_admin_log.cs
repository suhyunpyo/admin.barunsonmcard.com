using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class choan_admin_log
    {
        public int seq { get; set; }
        public int? order_seq { get; set; }
        public string worker_id { get; set; }
        public string admin_id { get; set; }
        public DateTime? regdate { get; set; }
        public string screen_name { get; set; }
        public string function_name { get; set; }
    }
}
