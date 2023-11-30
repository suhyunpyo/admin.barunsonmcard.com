﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_EVENT_REMIND
    {
        public int seq { get; set; }
        public int order_id { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string writer { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public string prod_image { get; set; }
        public string isview { get; set; }
        public DateTime reg_date { get; set; }
    }
}
