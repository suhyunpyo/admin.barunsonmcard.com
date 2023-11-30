using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_Greeting
    {
        public int GreetingID { get; set; }
        public string InvitationType { get; set; }
        public string GreetingType { get; set; }
        public string GreetingWord { get; set; }
        public int GreetingOrder { get; set; }
    }
}
