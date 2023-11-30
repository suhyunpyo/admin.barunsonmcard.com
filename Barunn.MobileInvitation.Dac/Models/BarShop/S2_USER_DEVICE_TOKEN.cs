using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_USER_DEVICE_TOKEN
    {
        public int S2_USER_DEVICE_TOKEN_SEQ { get; set; }
        public string UID { get; set; }
        public string COMPANY_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string TOKEN { get; set; }
        public string MOBILE_DEVICE_OS_TYPE_CODE { get; set; }
        public string DEVICE_TYPE_NAME { get; set; }
        public string HTTP_USER_AGENT { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
