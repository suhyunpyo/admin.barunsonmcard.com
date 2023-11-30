using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class bbarunson_log
    {
        public int id { get; set; }
        public string gubun { get; set; }
        public string admin_id { get; set; }
        public string key_idx { get; set; }
        public string act_sql { get; set; }
        public DateTime reg_date { get; set; }
    }
}
