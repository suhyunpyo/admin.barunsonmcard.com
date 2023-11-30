using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_SM
    {
        public int seq { get; set; }
        public int order_seq { get; set; }
        public string uid { get; set; }
        public string from_no { get; set; }
        public string to_no { get; set; }
        public string to_message { get; set; }
        public string ret_value { get; set; }
        public DateTime mdate { get; set; }
    }
}
