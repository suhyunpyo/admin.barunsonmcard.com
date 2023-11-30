using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class evt_mem_regist_gift
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public int gift_card_seq { get; set; }
        public DateTime regist_Date { get; set; }
        public DateTime? give_Date { get; set; }
        public int? order_seq { get; set; }
        public DateTime? end_date { get; set; }
    }
}
