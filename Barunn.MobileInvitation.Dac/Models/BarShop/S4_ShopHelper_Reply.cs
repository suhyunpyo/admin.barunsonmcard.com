using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_ShopHelper_Reply
    {
        public int seq { get; set; }
        public int sh_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string comment { get; set; }
        public DateTime reg_date { get; set; }
    }
}
