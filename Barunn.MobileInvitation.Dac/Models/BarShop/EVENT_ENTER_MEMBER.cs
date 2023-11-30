using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EVENT_ENTER_MEMBER
    {
        public int SEQ { get; set; }
        public string EVENT_GUBUN { get; set; }
        public string MEMBER_ID { get; set; }
        public string SALES_GUBUN { get; set; }
        public DateTime REG_DATE { get; set; }
    }
}
