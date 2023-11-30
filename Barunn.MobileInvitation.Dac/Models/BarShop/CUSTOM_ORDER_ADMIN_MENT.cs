using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ORDER_ADMIN_MENT
    {
        public int ID { get; set; }
        public string ISWOrder { get; set; }
        public string MENT { get; set; }
        public int ORDER_SEQ { get; set; }
        public byte? PCHECK { get; set; }
        public byte? STATUS { get; set; }
        public string ADMIN_ID { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string isJumun { get; set; }
        public byte? intype { get; set; }
        public string sgubun { get; set; }
        public string stype { get; set; }
    }
}
