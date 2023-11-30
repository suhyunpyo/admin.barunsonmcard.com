using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_TmpInvitationOption
    {
        public int OptionID { get; set; }
        public int InvitationID { get; set; }
        public string OptionItem { get; set; }
        public string OptionValue { get; set; }
        public DateTime RegisterTime { get; set; }

        public virtual mcard_TmpInvitation Invitation { get; set; }
    }
}
