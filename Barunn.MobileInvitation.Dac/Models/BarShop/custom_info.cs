using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_info
    {
        public string sales_gubun { get; set; }
        public string order_type { get; set; }
        public int order_seq { get; set; }
        public string cname { get; set; }
        public string cphone { get; set; }
        public string chphone { get; set; }
        public int status_seq { get; set; }
        public DateTime? order_date { get; set; }
    }
}
