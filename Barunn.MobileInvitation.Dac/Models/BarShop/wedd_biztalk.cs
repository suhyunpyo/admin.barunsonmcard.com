using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class wedd_biztalk
    {
        public int ID { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
        public string div { get; set; }
        public string content { get; set; }
        public int? msg_type { get; set; }
        public string sender_key { get; set; }
        public string template_code { get; set; }
        public string kko_btn_type { get; set; }
        public string kko_btn_info { get; set; }
        public string lms_subject { get; set; }
        public string template_name { get; set; }
        public string USE_YORN { get; set; }
        public string callback { get; set; }
    }
}
