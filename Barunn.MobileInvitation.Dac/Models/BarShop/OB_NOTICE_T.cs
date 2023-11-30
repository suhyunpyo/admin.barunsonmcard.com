using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class OB_NOTICE_T
    {
        public int Nid { get; set; }
        public int? BOARD_ID { get; set; }
        public string Uid { get; set; }
        public string Uname { get; set; }
        public string Title { get; set; }
        public short Hit { get; set; }
        public DateTime RegDate { get; set; }
        public string Content { get; set; }
        public string gubun { get; set; }
        public string Type { get; set; }
        public int bd_order { get; set; }
        public int bd_level { get; set; }
        public int bd_group { get; set; }
        public string site_div { get; set; }
        public string FILE_NAME { get; set; }
        public string FILE_SIZE { get; set; }
    }
}
