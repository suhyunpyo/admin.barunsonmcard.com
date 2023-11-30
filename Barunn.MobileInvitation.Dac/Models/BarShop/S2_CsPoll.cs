using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CsPoll
    {
        public int company_seq { get; set; }
        public string status { get; set; }
        public int id { get; set; }
        public short seq { get; set; }
        public string title { get; set; }
        public string qtype { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public string answer5 { get; set; }
        public string answer6 { get; set; }
        public string answer7 { get; set; }
        public string answer8 { get; set; }
        public DateTime? reg_date { get; set; }
        public short? answer_cnt { get; set; }
    }
}
