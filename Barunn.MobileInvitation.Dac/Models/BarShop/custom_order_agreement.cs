using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_agreement
    {
        public int seq { get; set; }
        public int? order_seq { get; set; }
        public string agreement_type { get; set; }
        public int? is_agreemented { get; set; }
        public int? is_print { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
