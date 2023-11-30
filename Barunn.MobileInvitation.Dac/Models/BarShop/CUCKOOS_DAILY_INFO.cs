using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUCKOOS_DAILY_INFO
    {
        public int seq { get; set; }
        public string file_dt { get; set; }
        public string ConnInfo { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string phone { get; set; }
        public string hand_phone { get; set; }
        public string umail { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public string wedding_day { get; set; }
        public string barun_reg_site { get; set; }
        public DateTime? barun_reg_Date { get; set; }
        public DateTime? cuckos_reg_date { get; set; }
    }
}
