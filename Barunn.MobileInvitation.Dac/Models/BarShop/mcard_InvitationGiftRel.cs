using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_InvitationGiftRel
    {
        public int InvitationID { get; set; }
        public int GiftID { get; set; }
        public string InvitationCode { get; set; }
        public byte ListOrder { get; set; }
        public string SelectYN { get; set; }

        public virtual mcard_Gift Gift { get; set; }
        public virtual mcard_Invitation Invitation { get; set; }
    }
}
