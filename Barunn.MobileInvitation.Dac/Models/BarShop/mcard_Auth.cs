using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_Auth
    {
        public int AuthID { get; set; }
        public string InvitationCode { get; set; }
        public string SessionCode { get; set; }
        public DateTime AuthTime { get; set; }
        public string OrdererName { get; set; }
        public string OrdererMobile { get; set; }
        public string OrdererEmail { get; set; }
        public string MemberYN { get; set; }
        public string AuthIP { get; set; }
    }
}
