using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_Bestseller
    {
        public string sales_gubun { get; set; }
        public string gubun { get; set; }
        public int? card_seq { get; set; }
        public string card_code { get; set; }
        public short? company { get; set; }
        public int card_price_customer { get; set; }
        public string disrate_type { get; set; }
        public byte? card_kind { get; set; }
        public string card_img_ms { get; set; }
        public DateTime? regdate { get; set; }
    }
}
