using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVENT_5hang
    {
        public int seq { get; set; }
        public byte event_id { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string msg_txt1 { get; set; }
        public string msg_txt2 { get; set; }
        public string msg_txt3 { get; set; }
        public string msg_txt4 { get; set; }
        public string msg_txt5 { get; set; }
        public DateTime rdate { get; set; }
    }
}
