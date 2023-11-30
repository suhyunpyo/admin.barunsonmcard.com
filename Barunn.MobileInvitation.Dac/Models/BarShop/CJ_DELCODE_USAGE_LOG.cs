using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CJ_DELCODE_USAGE_LOG
    {
        public int SEQ { get; set; }
        public int? DELCODE_SEQ { get; set; }
        public long? CODESEQ { get; set; }
        public string DELIVERY_CODE_NUM { get; set; }
        public string SP_NAME { get; set; }
        public int? ORDER_SEQ { get; set; }
        public int? DELIVERY_ID { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
