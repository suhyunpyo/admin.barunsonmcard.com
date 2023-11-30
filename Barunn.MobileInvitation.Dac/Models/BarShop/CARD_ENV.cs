﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_ENV
    {
        public int card_seq { get; set; }
        public string env_code { get; set; }
        public string erp_code { get; set; }
        public int print_size { get; set; }
        public string card_size { get; set; }
        public int carD_price_customer { get; set; }
        public double? card_src_price { get; set; }
        public double? card_branch_price { get; set; }
    }
}
