using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_Card_Detail_Sub
    {
        public int Seq { get; set; }
        public int Card_Seq { get; set; }
        public string Step_1 { get; set; }
        public string Step_2 { get; set; }
        public string Step_3 { get; set; }
        public string Step_4 { get; set; }
        public string Step_5 { get; set; }
        public string Step_6 { get; set; }
        public string Step_Title { get; set; }
        public string Step_Text { get; set; }
        public string Step_Img { get; set; }
        public DateTime RegDate { get; set; }
    }
}
