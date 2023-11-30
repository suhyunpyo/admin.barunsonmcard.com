using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Branch_Card_Plan
    {
        public int id { get; set; }
        public int card_seq { get; set; }
        public byte display_group { get; set; }
        public short company_seq { get; set; }
        public short display_order { get; set; }
        public string info_tmp { get; set; }
        public short dis_gubun { get; set; }
    }
}
