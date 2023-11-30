using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_Admin
    {
        public int AdminID { get; set; }
        public string AdminUserID { get; set; }
        public string AdminPassword { get; set; }
        public string AdminRole { get; set; }
        public string AdminName { get; set; }
        public DateTime? RegisterTime { get; set; }
    }
}
