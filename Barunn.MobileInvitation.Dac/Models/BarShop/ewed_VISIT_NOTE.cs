using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_VISIT_NOTE
    {
        public int v_seq { get; set; }
        public int v_buy_card_seq { get; set; }
        public string v_pwd { get; set; }
        public string v_name { get; set; }
        public string v_title { get; set; }
        public string v_message { get; set; }
        public DateTime mdate { get; set; }
    }
}
