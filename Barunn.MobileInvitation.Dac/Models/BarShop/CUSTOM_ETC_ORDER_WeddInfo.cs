using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ETC_ORDER_WeddInfo
    {
        public int order_seq { get; set; }
        public string groom_name { get; set; }
        public string bride_name { get; set; }
        public string wedd_year { get; set; }
        public string wedd_month { get; set; }
        public string wedd_day { get; set; }
        public string wedd_hour { get; set; }
        public string wedd_minuite { get; set; }
        public string wedd_ampm { get; set; }
        public byte? wedd_week { get; set; }
        public string wedd_name { get; set; }
        public string wedd_place { get; set; }
        public string photo1 { get; set; }
        public string photo2 { get; set; }
        public string photo3 { get; set; }
        public string tmp { get; set; }
        public string etc_comment { get; set; }
    }
}
