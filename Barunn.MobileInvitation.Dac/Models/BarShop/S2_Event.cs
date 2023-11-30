using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_Event
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string uid { get; set; }
        public string charge_use { get; set; }
        public int charge_use_seq { get; set; }
        public int charge_use_num { get; set; }
        public DateTime reg_date { get; set; }
    }
}
