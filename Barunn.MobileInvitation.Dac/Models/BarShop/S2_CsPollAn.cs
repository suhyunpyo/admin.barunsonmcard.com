using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CsPollAn
    {
        public int aid { get; set; }
        public int qid { get; set; }
        public string answer { get; set; }
        public string answer_desc { get; set; }
        public int? order_seq { get; set; }
    }
}
