using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tCouponTarget
    {
        public string CouponCD { get; set; }
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public int? UserChkNM { get; set; }
        public DateTime? InsertDT { get; set; }
    }
}
