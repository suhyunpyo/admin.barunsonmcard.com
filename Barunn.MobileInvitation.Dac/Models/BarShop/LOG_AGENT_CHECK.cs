using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class LOG_AGENT_CHECK
    {
        public int LOG_SEQ { get; set; }
        public string LOG_AGENT { get; set; }
        public string UID { get; set; }
        public string SALES_GUBUN { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
