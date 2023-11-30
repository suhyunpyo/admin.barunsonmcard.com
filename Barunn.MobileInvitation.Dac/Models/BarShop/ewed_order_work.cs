using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_order_work
    {
        public int Order_Seq { get; set; }
        public string Wid { get; set; }
        public string Cid { get; set; }
        public bool? Confirm_Flag { get; set; }
        public string Content { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ConDate { get; set; }
        public DateTime? ModiDate { get; set; }
        public string ModiContent { get; set; }
    }
}
