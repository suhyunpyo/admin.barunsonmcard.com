using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_SampleBasket
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public int card_seq { get; set; }
        public DateTime reg_date { get; set; }
        public string MD_recommend { get; set; }
        public string GUID { get; set; }
    }
}
