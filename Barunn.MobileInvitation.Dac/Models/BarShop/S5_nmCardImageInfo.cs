using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_nmCardImageInfo
    {
        public int Order_Seq { get; set; }
        public int FileType { get; set; }
        public short FileIndex { get; set; }
        public string FileName { get; set; }
        public string OrgFileName { get; set; }
        public DateTime? RegDate { get; set; }
        public double? imageSizeW { get; set; }
        public double? imageSizeH { get; set; }
        public short? imageRotation { get; set; }
        public double? imagePositionX { get; set; }
        public double? imagePositionY { get; set; }
    }
}
