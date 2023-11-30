using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardOption_UsrImg
    {
        public int uimg_seq { get; set; }
        public string uimg_card_code { get; set; }
        public int? uimg_number { get; set; }
        public string uimg_title { get; set; }
        public string uimg_subtitle { get; set; }
        public string uimg_info_img { get; set; }
        public string uimg_info_text { get; set; }
        public int uimg_status { get; set; }
        public DateTime uimg_regdate { get; set; }
        public int? uimg_wh { get; set; }
    }
}
