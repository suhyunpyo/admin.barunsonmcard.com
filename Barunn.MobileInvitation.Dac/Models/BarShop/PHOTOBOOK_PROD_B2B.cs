using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_PROD_B2B
    {
        public string site_code { get; set; }
        public string prod_code { get; set; }
        public string prod_option { get; set; }
        public int prod_price { get; set; }
        public DateTime reg_date { get; set; }
    }
}
