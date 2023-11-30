using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class RESEARCH_QLIST
    {
        public int id { get; set; }
        public int? research_seq { get; set; }
        public string qtitle { get; set; }
        public string ans1 { get; set; }
        public string ans2 { get; set; }
        public string ans3 { get; set; }
        public string ans4 { get; set; }
        public string ans5 { get; set; }
        public string ans6 { get; set; }
        public string ans7 { get; set; }
        public int? ans_cnt { get; set; }
        public int? ans1_cnt { get; set; }
        public int? ans2_cnt { get; set; }
        public int? ans3_cnt { get; set; }
        public int? ans4_cnt { get; set; }
        public int? ans5_cnt { get; set; }
        public int? ans6_cnt { get; set; }
        public string ans7_cnt { get; set; }
        public DateTime? reg_Date { get; set; }
        public string status { get; set; }
    }
}
