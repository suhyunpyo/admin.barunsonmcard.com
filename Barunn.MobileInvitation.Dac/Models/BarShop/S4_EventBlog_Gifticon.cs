using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventBlog_Gifticon
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string blog_url { get; set; }
        public string cafe_url { get; set; }
        public string gifticon_type { get; set; }
        public string hphone1 { get; set; }
        public string hphone2 { get; set; }
        public string hphone3 { get; set; }
        public string msg { get; set; }
        public string isSms { get; set; }
        public DateTime? sms_date { get; set; }
        public DateTime reg_date { get; set; }
    }
}
