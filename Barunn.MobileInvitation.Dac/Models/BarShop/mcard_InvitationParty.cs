using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_InvitationParty
    {
        public int PartyID { get; set; }
        public int InvitationID { get; set; }
        public string GroupName { get; set; }
        public string GroupMobile { get; set; }
        public string GuideName { get; set; }
        public string GuideNote { get; set; }

        public virtual mcard_Invitation Invitation { get; set; }
    }
}
