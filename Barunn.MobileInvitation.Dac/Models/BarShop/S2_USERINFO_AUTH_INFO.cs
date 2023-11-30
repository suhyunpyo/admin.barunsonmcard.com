using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_USERINFO_AUTH_INFO
    {
        public int SEQ { get; set; }
        public string ENCODE_DATA { get; set; }
        public string DUPINFO { get; set; }
        public string AUTH_MODULE_TYPE { get; set; }
        public string BIRTH_DATE { get; set; }
        public string GENDER { get; set; }
        public string NATIONAL_INFO { get; set; }
        public string AUTH_DESC { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
