using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tCouponOff
    {
        public string CouponCD { get; set; }
        public string OffCD { get; set; }
        public string PrintYN { get; set; }
        public string TakeYN { get; set; }
        public DateTime? TakeDT { get; set; }
        public string CouponNum { get; set; }
        public DateTime? InsertDT { get; set; }
    }
}
