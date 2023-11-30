using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Branch_card_display
    {
        public int company_seq { get; set; }
        public int card_seq { get; set; }
        public int? sales_ranking { get; set; }
        public string best_yes_or_no { get; set; }
        public string disrate_type { get; set; }
    }
}
