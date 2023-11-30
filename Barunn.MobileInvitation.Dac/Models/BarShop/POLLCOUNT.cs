using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class POLLCOUNT
    {
        public int idx { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public int? q_idx { get; set; }
        public int? a_idx { get; set; }
        public DateTime? wdate { get; set; }
        public string ip { get; set; }
    }
}
