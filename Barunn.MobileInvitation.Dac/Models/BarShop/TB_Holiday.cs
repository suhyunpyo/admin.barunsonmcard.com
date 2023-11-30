using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class TB_Holiday
    {
        public int id { get; set; }
        public string hyear { get; set; }
        public string hmonth { get; set; }
        public string hday { get; set; }
        public byte? add_d { get; set; }
        public string msg { get; set; }
    }
}
