using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Authorization_SM
    {
        public int SEQ { get; set; }
        public string GUID { get; set; }
        public string HPHONE { get; set; }
        public int? SMS_NUM { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
