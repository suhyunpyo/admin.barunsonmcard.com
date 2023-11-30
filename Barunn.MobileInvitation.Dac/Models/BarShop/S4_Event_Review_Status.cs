using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Event_Review_Status
    {
        public int ERA_Idx { get; set; }
        public int ERA_ER_idx { get; set; }
        public string ERA_adminID { get; set; }
        public int? ERA_Status { get; set; }
        public int? ERA_Coupon_Status { get; set; }
        public string ERA_Coupon_Code { get; set; }
        public string ERA_Comment { get; set; }
        public DateTime? ERA_Regdate { get; set; }
        public string ERA_Comment_Cancel { get; set; }
    }
}
