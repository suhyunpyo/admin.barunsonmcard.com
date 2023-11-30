using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class inipay_vbank_log
    {
        public int seq { get; set; }
        public string ID_MERCHANT { get; set; }
        public string NO_TID { get; set; }
        public string NO_OID { get; set; }
        public string NO_VACCT { get; set; }
        public string AMT_INPUT { get; set; }
        public string NM_INPUTBANK { get; set; }
        public string NM_INPUT { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
