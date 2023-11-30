using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_Wedding_Attend
    {
        public int seq { get; set; }
        public int order_seq { get; set; }
        public string uid { get; set; }
        public string rname { get; set; }
        public string rmail { get; set; }
        public byte result { get; set; }
        public DateTime mdate { get; set; }
        public DateTime? rdate { get; set; }
        public string attend_yn { get; set; }
        public string note { get; set; }
        public string senderDiv { get; set; }
    }
}
