using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class LMS_SEND_TARGET_USER
    {
        public int SEQ { get; set; }
        public int? GROUP_SEQ { get; set; }
        public string SITE_DIV { get; set; }
        public string SITE_NAME { get; set; }
        public DateTime? SIGN_IN_DATE { get; set; }
        public string UID { get; set; }
        public string UNAME { get; set; }
        public string BIRTH_DATE_VALUE { get; set; }
        public string HPHONE { get; set; }
        public string WEDDING_DAY_VALUE { get; set; }
        public string MK_EVENT_YORN { get; set; }
        public string SMS_YORN { get; set; }
    }
}
