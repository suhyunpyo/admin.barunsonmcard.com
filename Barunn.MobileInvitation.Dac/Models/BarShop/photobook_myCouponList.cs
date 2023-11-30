﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class photobook_myCouponList
    {
        public string uid { get; set; }
        public string sales_gubun { get; set; }
        public string site_code { get; set; }
        public string coupon_code { get; set; }
        public string disrate_type { get; set; }
        public int disrate { get; set; }
        public string coupon_msg { get; set; }
        public string Isthrowaway { get; set; }
        public string use_yn { get; set; }
        public string prod_cate2 { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
