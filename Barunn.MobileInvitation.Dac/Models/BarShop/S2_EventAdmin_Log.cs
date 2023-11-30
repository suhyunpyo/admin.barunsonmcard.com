using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_EventAdmin_Log
    {
        public int seq { get; set; }
        public string admin_id { get; set; }
        public string admin_memo { get; set; }
        public string uid { get; set; }
        public string charge_use { get; set; }
        public int? charge_use_seq { get; set; }
        public int? charge_use_num { get; set; }
        public DateTime reg_date { get; set; }
    }
}
