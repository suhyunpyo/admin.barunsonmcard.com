using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PREVIEW
    {
        public int preview_seq { get; set; }
        public int? card_seq { get; set; }
        public int order_Seq { get; set; }
        public string preview_type { get; set; }
        public string CE { get; set; }
        public int? sid { get; set; }
        public byte? tbl_seq { get; set; }
        public byte? corel_pnum { get; set; }
        public string imgFolder { get; set; }
        public string imgName { get; set; }
        public short? imgWidth { get; set; }
        public short? imgHeight { get; set; }
        public float? imgRatio { get; set; }
        public byte? step { get; set; }
        public byte checkCnt { get; set; }
        public byte Pstatus { get; set; }
        public string subject { get; set; }
        public DateTime Rdate { get; set; }
        public DateTime? Mdate { get; set; }
        public DateTime? confirm_date { get; set; }
        public DateTime? print_date { get; set; }
        public string content { get; set; }
    }
}
