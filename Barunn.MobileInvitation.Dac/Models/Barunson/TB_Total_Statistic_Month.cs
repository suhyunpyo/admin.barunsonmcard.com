using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.Barunson
{
    public partial class TB_Total_Statistic_Month
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public int? Free_Order_Count { get; set; }
        public int? Charge_Order_Count { get; set; }
        public int? Cancel_Count { get; set; }
        public int? Payment_Price { get; set; }
        public int? Cancel_Refund_Price { get; set; }
        public int? Profit_Price { get; set; }
        public int? Memberjoin_Count { get; set; }
    }
}
