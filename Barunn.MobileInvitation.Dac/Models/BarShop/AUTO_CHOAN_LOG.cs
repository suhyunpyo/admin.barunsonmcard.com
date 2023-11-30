using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class AUTO_CHOAN_LOG
    {
        public int SEQ { get; set; }
        public int? ORDER_SEQ { get; set; }
        public string LOCATION { get; set; }
        public string SUB_LOCATION { get; set; }
        public string MESSAGE { get; set; }
        public string DESCRIPTION { get; set; }
        public string REG_ID { get; set; }
        public string REG_IP { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
