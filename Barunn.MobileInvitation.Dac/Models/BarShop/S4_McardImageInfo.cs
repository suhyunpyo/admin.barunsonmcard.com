using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_McardImageInfo
    {
        public int? Order_Seq { get; set; }
        public short? FileIndex { get; set; }
        public string FileName { get; set; }
        public DateTime? RegDate { get; set; }
        public short? imageRotation { get; set; }
        public double? imagePositionX { get; set; }
        public double? imagePositionY { get; set; }
    }
}
