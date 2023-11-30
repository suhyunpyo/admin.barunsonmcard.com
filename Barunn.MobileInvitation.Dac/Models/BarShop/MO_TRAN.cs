using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class MO_TRAN
    {
        public long MO_NUM { get; set; }
        public string MO_ACCEPTTIME { get; set; }
        public string MO_MODIFIED { get; set; }
        public string MO_NUMBER { get; set; }
        public string MO_SENDER { get; set; }
        public string MO_MSG { get; set; }
        public long? MO_SN { get; set; }
        public string MO_NET { get; set; }
        public string MO_STATUS { get; set; }
        public string MO_REPLY_DATE { get; set; }
        public string MO_REPLY_MSG { get; set; }
        public string admin_id { get; set; }
        public DateTime? act_date { get; set; }
        public DateTime? reg_date { get; set; }
        public string site_gubun { get; set; }
        public string IMAGE1 { get; set; }
        public string IMAGE2 { get; set; }
        public string IMAGE3 { get; set; }
    }
}
