using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_FAQ
    {
        public int seq { get; set; }
        public string site_code { get; set; }
        public short display_order { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public byte div { get; set; }
        public DateTime mdate { get; set; }
        public string isBest { get; set; }
    }
}
