using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class weddinghall_tmp
    {
        public int id { get; set; }
        public int order_seq { get; set; }
        public string admin_id { get; set; }
        public string folder { get; set; }
        public DateTime reg_date { get; set; }
        public DateTime? approve_date { get; set; }
        public string status { get; set; }
        public int? wedd_idx { get; set; }
        public int? weddimg_idx { get; set; }
        public byte? wdiv { get; set; }
        public byte? location { get; set; }
        public string wname { get; set; }
        public string wphone { get; set; }
        public string waddress { get; set; }
    }
}
