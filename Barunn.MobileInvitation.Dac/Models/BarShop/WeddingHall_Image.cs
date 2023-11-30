using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class WeddingHall_Image
    {
        public int seq { get; set; }
        public int WeddImg_IDX { get; set; }
        public int Wedd_IDX { get; set; }
        public string imgFolder { get; set; }
        public string ImgName { get; set; }
        public short? imgWidth { get; set; }
        public short? imgHeight { get; set; }
        public int? lsort { get; set; }
        public DateTime? reg_Date { get; set; }
        public DateTime? mod_date { get; set; }
        public string admin_id { get; set; }
        public string isCorel { get; set; }
        public string isR { get; set; }
        public int? sort { get; set; }
        public string isDP { get; set; }
        public string isColor { get; set; }
    }
}
