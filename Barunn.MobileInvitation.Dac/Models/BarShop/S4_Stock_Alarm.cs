using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Stock_Alarm
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public int card_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string hand_phone1 { get; set; }
        public string hand_phone2 { get; set; }
        public string hand_phone3 { get; set; }
        public string isAlarm_send { get; set; }
        public DateTime? send_date { get; set; }
        public DateTime reg_date { get; set; }
    }
}
