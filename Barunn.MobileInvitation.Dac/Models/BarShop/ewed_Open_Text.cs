using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_Open_Text
    {
        public int seq { get; set; }
        public int order_seq { get; set; }
        public string uid { get; set; }
        public string pwd { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public DateTime idate { get; set; }
    }
}
