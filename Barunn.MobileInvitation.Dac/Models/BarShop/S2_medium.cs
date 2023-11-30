using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_medium
    {
        public int seq { get; set; }
        public string writer { get; set; }
        public int? company_seq { get; set; }
        public string Thumbnail_url { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string contents { get; set; }
        public string isdp { get; set; }
        public DateTime reg_date { get; set; }
        public string temp1 { get; set; }
        public string temp2 { get; set; }
    }
}
