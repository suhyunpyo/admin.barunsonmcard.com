﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.Barunson
{
    public partial class TB_Gift
    {
        public int Gift_ID { get; set; }
        public int? Invitation_ID { get; set; }
        public string Gift_Type_Code { get; set; }
        public string Gift_Name { get; set; }
        public int? External_Gift_ID { get; set; }
        public string Regist_User_ID { get; set; }
        public DateTime? Regist_DateTime { get; set; }
        public string Regist_IP { get; set; }
        public string Update_User_ID { get; set; }
        public DateTime? Update_DateTime { get; set; }
        public string Update_IP { get; set; }

        public virtual TB_Invitation Invitation { get; set; }
    }
}
