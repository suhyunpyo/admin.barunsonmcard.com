using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Discount_Paper
    {
        public int DiscountSeq { get; set; }
        public int COMPANY_SEQ { get; set; }
        public DateTime? RegDate { get; set; }
        public string DiscountNum { get; set; }
    }
}
