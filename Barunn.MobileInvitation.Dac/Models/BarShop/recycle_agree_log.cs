using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class recycle_agree_log
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int order_seq { get; set; }
        public DateTime created_tmstmp { get; set; }
    }
}
