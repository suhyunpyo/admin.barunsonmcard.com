using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardDiscountInfo
    {
        public int CardDiscount_Seq { get; set; }
        public string CardDiscount_Code { get; set; }
        public string CardDiscount_Div { get; set; }
        public int? CardDiscount_Company_SEQ { get; set; }
        public int? CardDiscount_Status { get; set; }
        public string CardDiscount_Type { get; set; }
    }
}
