using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardSalesBest
    {
        public int Company_Seq { get; set; }
        public int card_seq { get; set; }
        public short? Ranking_w { get; set; }
        public short? Ranking_m { get; set; }
        public short? NewProduct { get; set; }
        public short? SpecialPrice { get; set; }
        public short? BestWeek { get; set; }
        public short? BestMonth { get; set; }
    }
}
