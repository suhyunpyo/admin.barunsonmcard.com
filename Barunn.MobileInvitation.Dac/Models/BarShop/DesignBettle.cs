using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DesignBettle
    {
        public int SEQ { get; set; }
        public short Num { get; set; }
        public string Gubun { get; set; }
        public int Card_Seq { get; set; }
        public string Content { get; set; }
        public DateTime RegDate { get; set; }
        public string Member_id { get; set; }
        public string Order_Name { get; set; }
        public string Order_Email { get; set; }
    }
}
