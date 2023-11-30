using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class bhands_day_count
    {
        public DateTime? reg_date { get; set; }
        public string day { get; set; }
        public int? bhands_join_total { get; set; }
        public int? bhands_join_web { get; set; }
        public int? bhands_join_mobile { get; set; }
        public int? bhands_membership_total { get; set; }
        public int? bhands_membership_join { get; set; }
        public int? bhands_membership_modify { get; set; }
        public int? bhands_membership_apay { get; set; }
        public int? bhands_membership_X { get; set; }
        public int? bhands_membership_percent { get; set; }
    }
}
