using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CONTRACT_TBL
    {
        public int CON_ID { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string CONTRACT_NM { get; set; }
        public string CONTRACT_KIND { get; set; }
        public int CONTRACT_VAL { get; set; }
        public DateTime CONTRACT_SDT { get; set; }
        public DateTime CONTRACT_EDT { get; set; }
        public string REG_ID { get; set; }
        public DateTime REG_DT { get; set; }
        public string CHG_ID { get; set; }
        public DateTime CHG_DT { get; set; }
        public string STAT { get; set; }
    }
}
