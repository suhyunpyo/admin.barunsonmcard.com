using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_Skin
    {
        public mcard_Skin()
        {
            mcard_Invitations = new HashSet<mcard_Invitation>();
            mcard_TmpInvitations = new HashSet<mcard_TmpInvitation>();
        }

        public int SkinID { get; set; }
        public string SkinCode { get; set; }
        public string MainImageYN { get; set; }
        public string SkinPath { get; set; }
        public string MainRatioW { get; set; }
        public string MainRatioH { get; set; }
        public string DateENKR { get; set; }
        public string DateSeparater { get; set; }
        public string SkinNameEN { get; set; }
        public string SkinNameKR { get; set; }
        public string InvitationType { get; set; }

        public virtual ICollection<mcard_Invitation> mcard_Invitations { get; set; }
        public virtual ICollection<mcard_TmpInvitation> mcard_TmpInvitations { get; set; }
    }
}
