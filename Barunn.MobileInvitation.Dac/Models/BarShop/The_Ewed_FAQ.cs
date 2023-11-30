using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class The_Ewed_FAQ
    {
        public int seq { get; set; }
        public short display_order { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public byte? div { get; set; }
        public string sales_gubun { get; set; }
        public DateTime mdate { get; set; }
        public int? COMPANY_SEQ { get; set; }
    }
}
