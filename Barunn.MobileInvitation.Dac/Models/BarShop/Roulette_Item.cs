using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Roulette_Item
    {
        public int roti_Idx { get; set; }
        public string roti_title { get; set; }
        public int roti_price { get; set; }
        public string roti_company_name { get; set; }
        public string roti_memo { get; set; }
        public DateTime roti_regdate { get; set; }
        public int roti_status { get; set; }
        public string roti_type { get; set; }
        public string roti_couponCD { get; set; }
        public DateTime? roti_sdate { get; set; }
        public DateTime? roti_edate { get; set; }
    }
}
