using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_Gift
    {
        public mcard_Gift()
        {
            mcard_InvitationGiftRels = new HashSet<mcard_InvitationGiftRel>();
            mcard_TmpInvitationGiftRels = new HashSet<mcard_TmpInvitationGiftRel>();
        }

        public int GiftID { get; set; }
        public string GiftName { get; set; }
        public int GiftPrice { get; set; }
        public string GiftURL { get; set; }
        public string GiftImageURL { get; set; }
        public byte GiftOrder { get; set; }

        public virtual ICollection<mcard_InvitationGiftRel> mcard_InvitationGiftRels { get; set; }
        public virtual ICollection<mcard_TmpInvitationGiftRel> mcard_TmpInvitationGiftRels { get; set; }
    }
}
