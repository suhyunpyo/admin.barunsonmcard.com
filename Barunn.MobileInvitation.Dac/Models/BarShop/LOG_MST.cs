using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class LOG_MST
    {
        public int SEQ { get; set; }
        public string GUID { get; set; }
        public string SITE { get; set; }
        public string LOCATION { get; set; }
        public string SUB_LOCATION { get; set; }
        public string LOG_TYPE_NAME { get; set; }
        public string MSG { get; set; }
        public string USER_ID { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
