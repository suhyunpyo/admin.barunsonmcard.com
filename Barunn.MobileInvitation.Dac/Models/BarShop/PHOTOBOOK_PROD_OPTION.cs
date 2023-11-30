using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_PROD_OPTION
    {
        public int prod_id { get; set; }
        public short prod_option { get; set; }
        public int add_price { get; set; }
        public string tmp1 { get; set; }
        public string mc_prod_name { get; set; }
        public string mc_prod_img { get; set; }
        public string pn { get; set; }
        public short? p { get; set; }
    }
}
