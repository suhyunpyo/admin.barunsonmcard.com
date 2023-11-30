using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_USERINFO_SIGNUP_DEVICE
    {
        public string DUPINFO { get; set; }
        public string UID { get; set; }
        public string USER_AGENT { get; set; }
        public string DEVICE_TYPE { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
