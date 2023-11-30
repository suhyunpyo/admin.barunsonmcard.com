using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class WEDD_MAIL
    {
        public int id { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
        public string div { get; set; }
        public string div_s2 { get; set; }
        public string sms_phone { get; set; }
        public string email { get; set; }
        public string email_sender { get; set; }
        public string sms_msg { get; set; }
        public string email_title { get; set; }
        public string email_msg { get; set; }
        public string USE_YORN { get; set; }
    }
}
