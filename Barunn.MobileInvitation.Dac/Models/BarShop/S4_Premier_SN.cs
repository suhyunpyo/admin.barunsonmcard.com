using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Premier_SN
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public int? sample_order_seq { get; set; }
        public string card_code { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string comment { get; set; }
        public string sns_div { get; set; }
        public string contents_url { get; set; }
        public string best_div { get; set; }
        public string view_div { get; set; }
        public DateTime reg_date { get; set; }
    }
}
