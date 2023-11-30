using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_UserComment_ETC_reply
    {
        public int uc_idx { get; set; }
        public int? uc_seq { get; set; }
        public string uc_uname { get; set; }
        public string uc_comment { get; set; }
        public DateTime? uc_regdate { get; set; }
    }
}
