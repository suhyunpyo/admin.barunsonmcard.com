using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVENT_FACEBOOKSHARE
    {
        public int seq { get; set; }
        public string member_id { get; set; }
        public string site_div { get; set; }
        public int? event_year { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
