using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_InvitationBaby
    {
        public int BabyID { get; set; }
        public int InvitationID { get; set; }
        public string BabyName { get; set; }
        public string BabyBirth { get; set; }
        public string DadName { get; set; }
        public string DadMobile { get; set; }
        public string MomName { get; set; }
        public string MomMobile { get; set; }

        public virtual mcard_Invitation Invitation { get; set; }
    }
}
