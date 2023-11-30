using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_LoginIpInfo
    {
        public int SEQ { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string UID { get; set; }
        public string UNAME { get; set; }
        public string UMAIL { get; set; }
        public string IP { get; set; }
        public string REFERER_URL { get; set; }
        public DateTime? REGDATE { get; set; }
        public string DEVICE_TYPE_NAME { get; set; }
        public string AGENT_TYPE { get; set; }
    }
}
