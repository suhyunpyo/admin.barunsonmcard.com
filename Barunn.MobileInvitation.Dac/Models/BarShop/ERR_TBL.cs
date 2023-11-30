using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ERR_TBL
    {
        public int id { get; set; }
        public string login_id { get; set; }
        public string err_desc { get; set; }
        public DateTime? err_time { get; set; }
    }
}
