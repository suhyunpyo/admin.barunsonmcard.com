using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_UserInfo_Deardeer_Marketing
    {
        public int seq { get; set; }
        public string uid { get; set; }
        public string agreement_type { get; set; }
        public string chk_agreement { get; set; }
        public DateTime? agree_date { get; set; }
        public DateTime? cancel_date { get; set; }
    }
}
