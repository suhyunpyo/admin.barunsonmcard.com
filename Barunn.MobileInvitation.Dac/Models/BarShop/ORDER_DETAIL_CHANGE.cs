using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ORDER_DETAIL_CHANGE
    {
        public int ORDER_SEQ { get; set; }
        public int CARD_SEQ { get; set; }
        public int PRE_CARD_SEQ { get; set; }
        public string CHANGE_NOTE { get; set; }
        public string CHANGE_DATE { get; set; }
    }
}
