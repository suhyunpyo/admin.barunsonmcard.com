using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVENT_SAMSUNG
    {
        public int id { get; set; }
        public int company_seq { get; set; }
        public string branch_code { get; set; }
        public string c_name { get; set; }
        public string c_phone { get; set; }
        public string c_email { get; set; }
        public string coupon_code { get; set; }
        public string isAns { get; set; }
        public string c_sex { get; set; }
        public byte? c_age { get; set; }
        public string wedding_Date { get; set; }
        public byte? ans1 { get; set; }
        public byte? ans2 { get; set; }
        public byte? ans3 { get; set; }
        public byte? ans4 { get; set; }
        public byte? ans5 { get; set; }
        public string ans5_detail { get; set; }
        public string sreg_Date { get; set; }
        public DateTime? reg_date { get; set; }
        public DateTime? ans_date { get; set; }
        public int? l_info1 { get; set; }
        public int? l_info2 { get; set; }
        public string admin_memo { get; set; }
        public string isBarunson { get; set; }
    }
}
