using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class LMS_SEND_TARGET_GROUP
    {
        public int GROUP_SEQ { get; set; }
        public string SUBJECT { get; set; }
        public string MSG { get; set; }
        public DateTime? RESERVATION_DATE { get; set; }
        public string RQEUST_ADMIN_NAME { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
