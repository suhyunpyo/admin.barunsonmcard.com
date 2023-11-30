﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Poll_item
    {
        public int seq { get; set; }
        public int poll_seq { get; set; }
        public string item_title { get; set; }
        public int item_count { get; set; }
        public string item_etc_div { get; set; }
        public int? sort_num { get; set; }
    }
}
