using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class MCARD_INVITATION_FOR_OTHER_SITE
    {
        public int SEQ { get; set; }
        public string SITE_CODE { get; set; }
        public string ORIGINAL_SITE_ORDER_CODE { get; set; }
        public string InvitationCode { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string HPHONE { get; set; }
        public string AVAILABLE_YORN { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
