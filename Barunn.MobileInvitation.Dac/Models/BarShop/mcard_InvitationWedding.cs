using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_InvitationWedding
    {
        public int WeddingID { get; set; }
        public int InvitationID { get; set; }
        public string GroomName { get; set; }
        public string GroomMobile { get; set; }
        public string BrideName { get; set; }
        public string BrideMobile { get; set; }
        public string GroomHostRel1 { get; set; }
        public string GroomHostName1 { get; set; }
        public string GroomHostMobile1 { get; set; }
        public string GroomHostRel2 { get; set; }
        public string GroomHostName2 { get; set; }
        public string GroomHostMobile2 { get; set; }
        public string BrideHostRel1 { get; set; }
        public string BrideHostName1 { get; set; }
        public string BrideHostMobile1 { get; set; }
        public string BrideHostRel2 { get; set; }
        public string BrideHostName2 { get; set; }
        public string BrideHostMobile2 { get; set; }
        public string GroomAccountBank { get; set; }
        public string GroomAccountNumber { get; set; }
        public string BrideAccountBank { get; set; }
        public string BrideAccountNumber { get; set; }
        public string GroomAccountYN { get; set; }
        public string BrideAccountYN { get; set; }
        public string GroomAccountName { get; set; }
        public string GroomAccountRelation { get; set; }
        public string BrideAccountName { get; set; }
        public string BrideAccountRelation { get; set; }
        public string GroomAccountBank2 { get; set; }
        public string GroomAccountNumber2 { get; set; }
        public string BrideAccountBank2 { get; set; }
        public string BrideAccountNumber2 { get; set; }
        public string GroomAccountYN2 { get; set; }
        public string BrideAccountYN2 { get; set; }
        public string GroomAccountName2 { get; set; }
        public string GroomAccountRelation2 { get; set; }
        public string BrideAccountName2 { get; set; }
        public string BrideAccountRelation2 { get; set; }

        public virtual mcard_Invitation Invitation { get; set; }
    }
}
