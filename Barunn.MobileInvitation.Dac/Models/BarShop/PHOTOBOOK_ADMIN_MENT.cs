using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_ADMIN_MENT
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public byte status { get; set; }
        public string ment { get; set; }
        public string admin_id { get; set; }
        public DateTime reg_date { get; set; }
    }
}
