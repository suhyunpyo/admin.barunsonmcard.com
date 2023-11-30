using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class STORE_BARUNSON_ORDER_MATCHING
    {
        public int Uid { get; set; }
        public int Order_Seq { get; set; }
        public DateTime Reg_Date { get; set; }
        public DateTime Last_Matching_Date { get; set; }
    }
}
