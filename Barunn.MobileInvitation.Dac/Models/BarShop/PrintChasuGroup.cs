using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PrintChasuGroup
    {
        public int Seq { get; set; }
        public string GroupCode { get; set; }
        public short GroupCodeSeq { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public string GroupRemark { get; set; }
        public string GroupType { get; set; }
        public string UseYN { get; set; }
        public DateTime RegDT { get; set; }
        public string RegUser { get; set; }
    }
}
