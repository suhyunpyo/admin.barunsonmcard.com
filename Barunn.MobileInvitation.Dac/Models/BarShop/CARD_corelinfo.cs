using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_corelinfo
    {
        public int id { get; set; }
        public string card_code { get; set; }
        public short point_type { get; set; }
        public string isFPrint { get; set; }
        public double point_x { get; set; }
        public double point_y { get; set; }
        public byte? point_align { get; set; }
        public string font_name { get; set; }
        public double? font_size { get; set; }
        public string point_str { get; set; }
        public DateTime? mod_date { get; set; }
        public string admin_id { get; set; }
    }
}
