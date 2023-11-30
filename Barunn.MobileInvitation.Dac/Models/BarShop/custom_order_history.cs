using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_history
    {
        public int id { get; set; }
        public string htype { get; set; }
        public int order_seq { get; set; }
        public string admin_id { get; set; }
        public string memo { get; set; }
        public string system_sql { get; set; }
        public string isVisible { get; set; }
        public string ipAddress { get; set; }
        public DateTime reg_date { get; set; }
    }
}
