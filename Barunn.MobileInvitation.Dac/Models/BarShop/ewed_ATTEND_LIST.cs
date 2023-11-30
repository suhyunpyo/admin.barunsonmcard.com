using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_ATTEND_LIST
    {
        public int seq { get; set; }
        public int buy_card_SEQ { get; set; }
        public string buy_card_UID { get; set; }
        public string rname { get; set; }
        public string rmail { get; set; }
        public string result { get; set; }
        public DateTime mdate { get; set; }
        public DateTime? rdate { get; set; }
    }
}
