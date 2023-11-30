using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Event_ReviewBest
    {
        public int best_seq { get; set; }
        public string comment { get; set; }
        public string best_date { get; set; }
        public DateTime reg_date { get; set; }

        public virtual S4_Event_Review best_seqNavigation { get; set; }
    }
}
