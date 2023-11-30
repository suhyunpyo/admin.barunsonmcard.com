using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Poll_itemComment
    {
        public int seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string comment { get; set; }
        public int? poll_item_seq { get; set; }
        public DateTime reg_date { get; set; }
        public string isOpen { get; set; }
        public string uphone { get; set; }
        public int? poll_seq { get; set; }
    }
}
