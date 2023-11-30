using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_cms_msg
    {
        public long id { get; set; }
        public string order_type { get; set; }
        public int? order_seq { get; set; }
        public string cms_type { get; set; }
        public string cphone { get; set; }
        public string msg { get; set; }
        public DateTime reg_date { get; set; }
        public string admin_id { get; set; }
    }
}
