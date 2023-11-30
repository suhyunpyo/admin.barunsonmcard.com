using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardSamplePrice
    {
        public int id { get; set; }
        public string site_gubun { get; set; }
        public string price_type { get; set; }
        public int price { get; set; }
        public string updater { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
