using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_RESULT_LIST
    {
        public int seq_idx { get; set; }
        public string uid { get; set; }
        public string mailto { get; set; }
        public string r_mail { get; set; }
        public byte return_value { get; set; }
        public DateTime send_date { get; set; }
        public DateTime? return_date { get; set; }
    }
}
