using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SH_Notice
    {
        public int N_num { get; set; }
        public string N_id { get; set; }
        public string N_title { get; set; }
        public string N_text { get; set; }
        public DateTime N_insdate { get; set; }
        public string N_passwd { get; set; }
        public string N_name { get; set; }
        public DateTime? org_date { get; set; }
        public string view_div { get; set; }
    }
}
