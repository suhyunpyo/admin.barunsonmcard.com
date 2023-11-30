using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ADMIN_PRICE_LOGINFO
    {
        public int Seq { get; set; }
        public int? CardSeq { get; set; }
        public string AdminId { get; set; }
        public string AdminName { get; set; }
        public string Ip { get; set; }
        public int? PrePrice { get; set; }
        public int? AfterPrice { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
