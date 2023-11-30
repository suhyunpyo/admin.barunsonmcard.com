using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVT_BHANDS_COUPLE
    {
        public int seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string gubun { get; set; }
        public string title { get; set; }
        public string snsUrl { get; set; }
        public string upMV_name { get; set; }
        public string is_display { get; set; }
        public DateTime created_tmstmp { get; set; }
    }
}
