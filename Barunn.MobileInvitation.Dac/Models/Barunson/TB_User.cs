﻿using Barunn.MobileInvitation.Dac.Models.Barunson;
using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.Barunson
{
    public partial class TB_User
    {
        public TB_User()
        {
            TB_Accounts = new HashSet<TB_Account>();
        }

        public string User_ID { get; set; }

        public virtual ICollection<TB_Account> TB_Accounts { get; set; }
    }
}
