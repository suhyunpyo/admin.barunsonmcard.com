using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_ShopHelper
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public int card_seq_1 { get; set; }
        public int card_seq_2 { get; set; }
        public int card_count_1 { get; set; }
        public int card_count_2 { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string comment { get; set; }
        public string view_div { get; set; }
        public DateTime reg_date { get; set; }
    }
}
