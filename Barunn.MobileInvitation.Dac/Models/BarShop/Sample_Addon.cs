using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Sample_Addon
    {
        public int Sample_Addon_Seq { get; set; }
        public int? Company_Seq { get; set; }
        public string Sales_Gubun { get; set; }
        public string Promotion_Year { get; set; }
        public string Promotion_Month { get; set; }
        public int? Card_Seq { get; set; }
        public string Card_Code { get; set; }
        public string Use_YN { get; set; }
        public DateTime? Reg_Date { get; set; }
    }
}
