using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class oneclick_sample
    {
        public int oc_Sample_Seq { get; set; }
        public string evt_nm { get; set; }
        public int Card_Seq { get; set; }
        public string Card_Code { get; set; }
        public string Use_YN { get; set; }
        public DateTime Reg_Date { get; set; }
    }
}
