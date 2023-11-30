using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_cprice
    {
        public int id { get; set; }
        public int order_seq { get; set; }
        public int cprice { get; set; }
        public DateTime reg_date { get; set; }
    }
}
