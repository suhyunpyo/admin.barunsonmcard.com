using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_ChoanBasic
    {
        public int id { get; set; }
        public string card_type { get; set; }
        public string card_code { get; set; }
        public string isOsiGaro { get; set; }
        public string isReverse { get; set; }
        public int BlankSpace { get; set; }
        public string printGroup { get; set; }
        public short print_size { get; set; }
        public string isBackColor { get; set; }
        public string isFBackColor { get; set; }
        public string isBackPoint { get; set; }
        public short? pagesizeW { get; set; }
        public short? pagesizeH { get; set; }
        public string guideLineH { get; set; }
        public string guideLineV { get; set; }
        public string osi { get; set; }
        public string isMapGaro { get; set; }
        public short? text_sx { get; set; }
        public short? text_sy { get; set; }
        public short? text_ex { get; set; }
        public short? text_ey { get; set; }
        public short? map_sx { get; set; }
        public short? map_sy { get; set; }
        public short? map_ex { get; set; }
        public short? map_ey { get; set; }
        public short? traffic_sx { get; set; }
        public short? traffic_sy { get; set; }
        public short? traffic_ex { get; set; }
        public short? traffic_ey { get; set; }
        public short print_sx { get; set; }
        public short print_sy { get; set; }
        public short print_ex { get; set; }
        public short print_ey { get; set; }
        public int? ex_A { get; set; }
        public int? ex_B { get; set; }
        public int? ex_D { get; set; }
        public string swf_url { get; set; }
        public string fswf_url { get; set; }
    }
}
