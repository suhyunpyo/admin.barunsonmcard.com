using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventRelay
    {
        public int order_seq { get; set; }
        public string relay_uid { get; set; }
        public DateTime reg_date { get; set; }
        public string uid { get; set; }
        public int? relay_order_seq { get; set; }
        public string give_status { get; set; }
    }
}
