using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_Event_Member
    {
        public int CEM_Idx { get; set; }
        public int? CEM_Item { get; set; }
        public string CEM_UID { get; set; }
        public int CEM_Status { get; set; }
        public DateTime CEM_Regdate { get; set; }
    }
}
