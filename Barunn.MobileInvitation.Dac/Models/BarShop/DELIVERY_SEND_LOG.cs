using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DELIVERY_SEND_LOG
    {
        public int SEQ { get; set; }
        public string ORDER_SEQ { get; set; }
        public string ORDER_TABLE_NAME { get; set; }
        public string DELIVERY_CODE { get; set; }
        public string RESULT_CODE { get; set; }
        public string RESULT_MSG { get; set; }
        public string ERROR_MSG { get; set; }
        public string ERROR_DESC { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
