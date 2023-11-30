using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class s4_poll_user_reply
    {
        public int seq { get; set; }
        public int poll_seq { get; set; }
        public string uid { get; set; }
        public int poll_item_seq { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
