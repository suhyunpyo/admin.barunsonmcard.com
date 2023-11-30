using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tCouponSub1
    {
        public string CouponCD { get; set; }
        public string CouponNum { get; set; }
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public string TakeYN { get; set; }
        public DateTime? TakeDT { get; set; }
        public string UseYN { get; set; }
        public DateTime? UseDT { get; set; }
        public string userDelYN { get; set; }
        public string sendMailYN { get; set; }
        public DateTime? InsertDT { get; set; }
        public string TimeCD { get; set; }
    }
}
