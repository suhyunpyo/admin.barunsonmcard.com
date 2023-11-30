using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PREVIEW_NOTICE
    {
        public int id { get; set; }
        public int order_Seq { get; set; }
        public string contents { get; set; }
        public DateTime reg_date { get; set; }
        public string admin_id { get; set; }
        public byte status { get; set; }
    }
}
