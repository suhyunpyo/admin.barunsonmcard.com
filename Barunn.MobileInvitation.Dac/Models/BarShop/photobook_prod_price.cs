using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class photobook_prod_price
    {
        public string prod_code { get; set; }
        public string makecom_code { get; set; }
        public string prod_name { get; set; }
        public string prod_cate2 { get; set; }
        public string prod_size { get; set; }
        public DateTime? reg_date { get; set; }
        public int prod_price { get; set; }
        public string disrate_type { get; set; }
        public short? fix_disrate { get; set; }
        public int src_price { get; set; }
        public short prod_option { get; set; }
        public int add_price { get; set; }
        public string mc_prod_name { get; set; }
        public short? p { get; set; }
    }
}
