using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CustomizedCard
    {
        public int seq { get; set; }
        public string custom_div { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public string image_small { get; set; }
        public string image_big { get; set; }
        public DateTime reg_date { get; set; }
    }
}
