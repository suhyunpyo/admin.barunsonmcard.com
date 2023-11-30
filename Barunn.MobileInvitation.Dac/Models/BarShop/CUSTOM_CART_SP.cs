using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_CART_SP
    {
        public long id { get; set; }
        public string MEMBER_ID { get; set; }
        public DateTime? REG_DATE { get; set; }
        public int CARD_SEQ { get; set; }
        public string card_option { get; set; }
        public string ishave { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
    }
}
