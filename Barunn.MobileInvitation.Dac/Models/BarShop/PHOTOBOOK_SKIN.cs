using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_SKIN
    {
        public int id { get; set; }
        public string skin_code { get; set; }
        public string skin_fn { get; set; }
        public string prod_cate { get; set; }
        public short? skin_page { get; set; }
        public string prod_size { get; set; }
        public string description { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
    }
}
