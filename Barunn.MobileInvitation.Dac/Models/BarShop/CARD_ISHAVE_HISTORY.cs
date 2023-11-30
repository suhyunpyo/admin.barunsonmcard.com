using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_ISHAVE_HISTORY
    {
        public int id { get; set; }
        public int card_seq { get; set; }
        public int ishave_change { get; set; }
        public string description { get; set; }
        public string admin_id { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
