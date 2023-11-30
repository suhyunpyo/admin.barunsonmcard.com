using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_Comment
    {
        public int CMT_SEQ { get; set; }
        public string MEMBER_NAME { get; set; }
        public string comment { get; set; }
        public DateTime? REGDATE { get; set; }
        public int CARD_SEQ { get; set; }
        public byte? score { get; set; }
        public string TITLE { get; set; }
        public string card_cate { get; set; }
        public string card_code { get; set; }
        public string card_img_xs { get; set; }
        public string comm_div { get; set; }
    }
}
