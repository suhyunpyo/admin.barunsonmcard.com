using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Tiara_basket
    {
        public int id { get; set; }
        public string member_id { get; set; }
        public string sessionID { get; set; }
        public int card_seq { get; set; }
        public int order_count { get; set; }
        public int card_sale_price { get; set; }
        public DateTime reg_date { get; set; }
    }
}
