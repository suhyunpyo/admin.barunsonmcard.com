using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class AUTO_CHOAN_APP_STATUS_LOG
    {
        public int SEQ { get; set; }
        public string MACHINE_IP { get; set; }
        public string APP_NAME { get; set; }
        public string APP_STATUS { get; set; }
        public DateTime REG_DATE { get; set; }
    }
}
