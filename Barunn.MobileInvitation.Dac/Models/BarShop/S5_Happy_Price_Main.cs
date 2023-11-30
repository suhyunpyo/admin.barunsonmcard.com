using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_Happy_Price_Main
    {
        public int hp_idx { get; set; }
        public string hp_title { get; set; }
        public DateTime? hp_Sdate { get; set; }
        public DateTime? hp_Edate { get; set; }
        public DateTime hp_regdate { get; set; }
        public int hp_status { get; set; }
    }
}
