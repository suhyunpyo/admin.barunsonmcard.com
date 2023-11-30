using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_Plus_Friend
    {
        public int PF_Idx { get; set; }
        public string PF_UID { get; set; }
        public string PF_FUID { get; set; }
        public DateTime? PF_Regdate { get; set; }
        public int? PF_Status { get; set; }
        public string PF_Coupon_Status { get; set; }
        public string PF_Coupon_Status_F { get; set; }
        public string sales_gubun { get; set; }
    }
}
