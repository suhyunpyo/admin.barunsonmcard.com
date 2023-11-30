using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_COMMENT
    {
        public int id { get; set; }
        public string site_code { get; set; }
        public string member_id { get; set; }
        public string writer_name { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public string prod_code { get; set; }
        public int? order_id { get; set; }
        public string product_order_id { get; set; }
        public DateTime reg_date { get; set; }
        public string isBest { get; set; }
        public DateTime? best_reg_date { get; set; }
        public string comm_div { get; set; }
        public string up_file { get; set; }
        public string score { get; set; }
    }
}
