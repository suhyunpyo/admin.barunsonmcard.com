using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_WishGreeting
    {
        public int seq { get; set; }
        public string uid { get; set; }
        public int greeting_seq { get; set; }
        public string site_div { get; set; }
        public DateTime reg_date { get; set; }
    }
}
