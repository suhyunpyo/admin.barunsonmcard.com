using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_OPEN
    {
        public int id { get; set; }
        public string site_code { get; set; }
        public int order_id { get; set; }
        public string product_order_id { get; set; }
        public string prod_code { get; set; }
        public string member_id { get; set; }
        public string member_name { get; set; }
        public string isBest { get; set; }
        public string isOpen { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public DateTime reg_date { get; set; }
        public DateTime? best_reg_date { get; set; }
    }
}
