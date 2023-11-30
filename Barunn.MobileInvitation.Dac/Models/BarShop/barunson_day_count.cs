using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class barunson_day_count
    {
        public DateTime? reg_date { get; set; }
        public string day { get; set; }
        public int? barunson_join_total { get; set; }
        public int? barunson_join_web { get; set; }
        public int? barunson_join_mobile { get; set; }
        public int? barunson_membership_total { get; set; }
        public int? barunson_membership_join { get; set; }
        public int? barunson_membership_modify { get; set; }
        public int? barunson_membership_apay { get; set; }
        public int? barunson_membership_X { get; set; }
        public int? barunson_membership_percent { get; set; }
    }
}
