﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.Barunson
{
    public partial class TB_Wish_List
    {
        public int Wish_ID { get; set; }
        public string User_ID { get; set; }
        public int? Product_ID { get; set; }
        public DateTime? Regist_DateTime { get; set; }
    }
}
