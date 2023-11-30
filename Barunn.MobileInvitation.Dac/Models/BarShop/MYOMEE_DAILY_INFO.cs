using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class MYOMEE_DAILY_INFO
    {
        public int seq { get; set; }
        public string ConnInfo { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string Birth_date { get; set; }
        public string phone { get; set; }
        public string hand_phone { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public string addr_detail { get; set; }
        public string umail { get; set; }
        public string wedding_day { get; set; }
        public DateTime? barun_reg_Date { get; set; }
        public string wedd_pgubun { get; set; }
        public DateTime? create_date { get; set; }
    }
}
