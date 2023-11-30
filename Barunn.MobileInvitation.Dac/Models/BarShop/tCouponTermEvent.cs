using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tCouponTermEvent
    {
        public string CouponCD { get; set; }
        public int EventIdx { get; set; }
        public string ProcType { get; set; }
        public DateTime InsertDT { get; set; }
    }
}
