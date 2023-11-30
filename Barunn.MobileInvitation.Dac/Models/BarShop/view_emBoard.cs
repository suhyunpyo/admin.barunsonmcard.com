using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_emBoard
    {
        public string Uid { get; set; }
        public string order_email { get; set; }
        public int Board_Seq { get; set; }
        public int Order_Seq { get; set; }
        public string Name { get; set; }
        public string content { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
