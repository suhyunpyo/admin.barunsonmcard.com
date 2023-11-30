using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BEAUTYBOX_ITEM_MST
    {
        public int SEQ { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public int? GROUP_CODE { get; set; }
        public string USE_YORN { get; set; }
        public string STOCK_YORN { get; set; }
        public int? STOCK_QTY { get; set; }
        public int? SALE_QTY { get; set; }
        public DateTime? IDATE { get; set; }
    }
}
