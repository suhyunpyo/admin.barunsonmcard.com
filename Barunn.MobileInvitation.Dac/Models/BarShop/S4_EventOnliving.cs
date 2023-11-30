﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventOnliving
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string gubun { get; set; }
        public int? item_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public DateTime reg_date { get; set; }
    }
}