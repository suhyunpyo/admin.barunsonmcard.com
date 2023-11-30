using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CONNECT_PATH
    {
        public int idx { get; set; }
        public string page { get; set; }
        public string path { get; set; }
        public string pass { get; set; }
        public string ip { get; set; }
        public string os { get; set; }
        public string browser { get; set; }
        public DateTime? wdate { get; set; }
    }
}
