using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BHANDS_WEEKLY_HOTDEAL
    {
        public int Seq { get; set; }
        public string START_DT { get; set; }
        public string END_DT { get; set; }
        public int? Card_seq { get; set; }
        public int? W_NUM { get; set; }
    }
}
