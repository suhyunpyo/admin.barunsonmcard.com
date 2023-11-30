using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_UserCardView
    {
        public int seq { get; set; }
        public string session_id { get; set; }
        public int card_seq { get; set; }
        public int company_seq { get; set; }
        public int? cardkind_seq { get; set; }
        public string site_div { get; set; }
        public DateTime reg_date { get; set; }
    }
}
