using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_COUPON_WEEKLY_SPECIAL
    {
        public int SEQ { get; set; }
        public int? COUPON_SEQ { get; set; }
        public DateTime? DOWNLOAD_START_DATE { get; set; }
        public DateTime? DOWNLOAD_END_DATE { get; set; }
        public int? LIMIT_DOWNLOAD_QTY { get; set; }
        public int? REAL_DOWNLOAD_QTY { get; set; }
        public int? VIRTUAL_DOWNLOAD_QTY { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
