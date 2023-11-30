using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class AliveCheck_Info
    {
        public string Site_Gubun { get; set; }
        public string Sample_Check { get; set; }
        public short? Sample_Time { get; set; }
        public string Order_Check { get; set; }
        public short? Order_Time { get; set; }
        public string Settle_Check { get; set; }
        public short? Settle_Time { get; set; }
    }
}
