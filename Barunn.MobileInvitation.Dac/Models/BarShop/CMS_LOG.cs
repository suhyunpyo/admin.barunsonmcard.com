using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CMS_LOG
    {
        public long id { get; set; }
        public string InOut { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public string cms_num { get; set; }
        public string cname { get; set; }
        public string info1 { get; set; }
        public string site { get; set; }
        public string qtype { get; set; }
        public string msg { get; set; }
        public string admin_id { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
