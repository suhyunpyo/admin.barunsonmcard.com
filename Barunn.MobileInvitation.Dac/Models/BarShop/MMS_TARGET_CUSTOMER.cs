using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class MMS_TARGET_CUSTOMER
    {
        public int SEQ { get; set; }
        public int? COMPANY_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string HPHONE { get; set; }
        public string SUBJECT { get; set; }
        public string MSG { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
