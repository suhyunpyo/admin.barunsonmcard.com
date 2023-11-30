using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVENT_talklove
    {
        public int seq { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string msg_txt { get; set; }
        public string talk_div { get; set; }
        public DateTime rdate { get; set; }
    }
}
