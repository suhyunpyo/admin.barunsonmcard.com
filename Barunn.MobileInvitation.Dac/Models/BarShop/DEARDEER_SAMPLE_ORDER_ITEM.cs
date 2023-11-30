using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DEARDEER_SAMPLE_ORDER_ITEM
    {
        public int DEARDEER_SAMPLE_ORDER_ITEM_SEQ { get; set; }
        public int DEARDEER_SAMPLE_ORDER_MST_SEQ { get; set; }
        public string SAMPLE_ORDER_ITEM_NO { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_IMAGE_URL { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
