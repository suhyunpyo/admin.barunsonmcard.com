﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.Barunson
{
    public partial class TB_Payment_Status_Day
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public int? Card_Payment_Price { get; set; }
        public int? Account_Transfer_Price { get; set; }
        public int? Virtual_Account_Price { get; set; }
        public int? Etc_Price { get; set; }
        public int? Total_Price { get; set; }
        public int? Cancel_Refund_Price { get; set; }
        public int? Profit_Price { get; set; }
    }
}
