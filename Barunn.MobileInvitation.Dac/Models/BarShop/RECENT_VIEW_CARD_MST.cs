using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class RECENT_VIEW_CARD_MST
    {
        public int RECENT_VIEW_CARD_MST_SEQ { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string UID { get; set; }
        public string GUID { get; set; }
        public DateTime REG_DATE { get; set; }
    }
}
