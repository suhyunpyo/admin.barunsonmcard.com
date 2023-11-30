using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardImage
    {
        public int CardImage_Seq { get; set; }
        public int Card_Seq { get; set; }
        public string CardImage_WSize { get; set; }
        public string CardImage_HSize { get; set; }
        public string CardImage_FileName { get; set; }
        public string CardImage_Div { get; set; }
        public int? Company_Seq { get; set; }
    }
}
