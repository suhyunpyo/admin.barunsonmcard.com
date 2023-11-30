using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_tmap
    {
        public int order_seq { get; set; }
        public string nick_name { get; set; }
        public string wname { get; set; }
        public string waddr { get; set; }
        public string wdate { get; set; }
        public DateTime reg_date { get; set; }
        public string rstatus { get; set; }
        public DateTime? cf_date { get; set; }
        public DateTime? req_date { get; set; }
    }
}
