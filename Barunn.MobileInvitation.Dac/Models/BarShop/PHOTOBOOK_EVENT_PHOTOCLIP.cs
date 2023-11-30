using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_EVENT_PHOTOCLIP
    {
        public int clip_seq { get; set; }
        public string member_id { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public string up_image { get; set; }
        public int view_cnt { get; set; }
        public DateTime reg_date { get; set; }
        public int? is_clip { get; set; }
    }
}
