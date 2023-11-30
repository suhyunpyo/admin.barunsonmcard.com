using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class APP_VERSION_MST
    {
        public int SEQ { get; set; }
        public string DEVICE_TYPE_CODE { get; set; }
        public int? VERSION { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
