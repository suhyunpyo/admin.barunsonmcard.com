using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class INTEGRATION_MEMBER_SIGN_UP_LOG
    {
        public int SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string USER_ID { get; set; }
        public string INTEGRATION_USER_ID { get; set; }
        public string USER_TABLE_NAME { get; set; }
        public string REFERER_SITE { get; set; }
        public string REFERER_SALES_GUBUN { get; set; }
        public string SELECT_SITE { get; set; }
        public string SELECT_ID { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
