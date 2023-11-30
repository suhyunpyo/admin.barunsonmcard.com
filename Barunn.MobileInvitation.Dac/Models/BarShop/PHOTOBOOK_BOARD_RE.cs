using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_BOARD_RE
    {
        public int id { get; set; }
        public short board_id { get; set; }
        public int up_id { get; set; }
        public string member_id { get; set; }
        public string member_name { get; set; }
        public string contents { get; set; }
        public DateTime reg_date { get; set; }
    }
}
