using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ADMIN_CONNECT
    {
        public string admin_id { get; set; }
        public string login_date { get; set; }
        public DateTime last_update { get; set; }
        public string login_ip { get; set; }
    }
}
