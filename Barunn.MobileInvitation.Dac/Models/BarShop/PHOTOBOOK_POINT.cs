using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_POINT
    {
        public int id { get; set; }
        public string member_id { get; set; }
        public int point { get; set; }
        public string comment { get; set; }
        public string admin_id { get; set; }
        public DateTime? reg_date { get; set; }
        public int? order_detail_id { get; set; }
    }
}
