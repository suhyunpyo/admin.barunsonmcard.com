﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Branch_user_comment
    {
        public int CMT_SEQ { get; set; }
        public short COMPANY_SEQ { get; set; }
        public string MEMBER_UID { get; set; }
        public string MEMBER_NAME { get; set; }
        public string COMMENT { get; set; }
        public string SERVICE_MENT { get; set; }
        public DateTime? REGDATE { get; set; }
        public int CARD_SEQ { get; set; }
        public byte? score { get; set; }
        public string TITLE { get; set; }
        public string IsBest { get; set; }
    }
}
