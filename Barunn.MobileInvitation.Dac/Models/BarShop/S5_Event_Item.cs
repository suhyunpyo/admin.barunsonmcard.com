using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_Event_Item
    {
        public int CE_Idx { get; set; }
        public int? CE_Event_NUM { get; set; }
        public int? CE_Item_NUM { get; set; }
        public string CE_Item_NM { get; set; }
        public int? CE_Item_CNT { get; set; }
        public DateTime CE_Regdate { get; set; }
        public int CE_Status { get; set; }
        public string CE_IMG { get; set; }
    }
}
