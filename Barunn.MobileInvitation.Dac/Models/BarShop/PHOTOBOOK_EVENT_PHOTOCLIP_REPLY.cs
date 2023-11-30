using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_EVENT_PHOTOCLIP_REPLY
    {
        public int seq { get; set; }
        public int clip_seq { get; set; }
        public string member_id { get; set; }
        public string reply { get; set; }
        public DateTime reg_date { get; set; }
    }
}
