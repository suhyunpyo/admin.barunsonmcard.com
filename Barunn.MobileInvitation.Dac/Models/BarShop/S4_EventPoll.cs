using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventPoll
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string contents { get; set; }
        public DateTime reg_date { get; set; }
    }
}
