using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Custom_etc_basket
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public string order_type { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public int card_seq { get; set; }
        public int card_num { get; set; }
        public int UNIT_PRICE { get; set; }
        public DateTime reg_date { get; set; }
    }
}
