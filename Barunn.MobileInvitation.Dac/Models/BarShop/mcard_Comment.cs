using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_Comment
    {
        public int CommentID { get; set; }
        public int InvitationID { get; set; }
        public string InvitationCode { get; set; }
        public string GuestName { get; set; }
        public string Password { get; set; }
        public string Commentary { get; set; }
        public DateTime RegisterTime { get; set; }
        public string RegisterIP { get; set; }
        public string View_YN { get; set; }

        public virtual mcard_Invitation Invitation { get; set; }
    }
}
