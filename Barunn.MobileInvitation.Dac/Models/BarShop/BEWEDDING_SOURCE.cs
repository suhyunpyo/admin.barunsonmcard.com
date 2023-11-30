using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BEWEDDING_SOURCE
    {
        public int SOURCE_SEQ { get; set; }
        public string SOURCE_CODE { get; set; }
        public string SOURCE_NAME { get; set; }
        public DateTime? REG_DATE { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_ID { get; set; }
        public string USE_YN { get; set; }
    }
}
