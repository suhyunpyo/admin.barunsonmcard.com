using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventOnliving_Count
    {
        public int seq { get; set; }
        public int? company_seq { get; set; }
        public string gubun { get; set; }
        public int item_seq { get; set; }
        public string item_name { get; set; }
        public int item_count { get; set; }
    }
}
