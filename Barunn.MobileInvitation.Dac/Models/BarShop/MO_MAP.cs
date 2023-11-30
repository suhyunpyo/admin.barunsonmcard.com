using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class MO_MAP
    {
        public string MO_NUMBER { get; set; }
        public string ALARM_YN { get; set; }
        public string ALARM_DAY_TIME { get; set; }
        public string ALARM_DAY_RECEIVERS { get; set; }
        public string ALARM_DAY_SENDER { get; set; }
        public string ALARM_NIGHT_TIME { get; set; }
        public string ALARM_NIGHT_RECEIVERS { get; set; }
        public string ALARM_NIGHT_SENDER { get; set; }
        public string ALARM_MSG { get; set; }
        public string AUTO_REPLY_YN { get; set; }
        public string AUTO_REPLY_MSG { get; set; }
    }
}
