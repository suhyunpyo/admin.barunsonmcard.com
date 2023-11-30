using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class TU_Bestcard
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
        public string card_code { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int view_cnt { get; set; }
        public string best_view { get; set; }
        public string detail_view { get; set; }
        public DateTime rdate { get; set; }
    }
}
