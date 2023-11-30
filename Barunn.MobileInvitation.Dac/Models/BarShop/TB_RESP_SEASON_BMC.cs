﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class TB_RESP_SEASON_BMC
    {
        public string MERT_ID { get; set; }
        public string order_seq { get; set; }
        public string amount { get; set; }
        public string result_msg { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
