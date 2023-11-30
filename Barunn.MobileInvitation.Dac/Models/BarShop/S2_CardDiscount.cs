using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardDiscount
    {
        public int CardDiscount_Seq { get; set; }
        public short MinCount { get; set; }
        public short MaxCount { get; set; }
        public decimal? Discount_Rate { get; set; }
        public decimal? Discount_Price { get; set; }
    }
}
