using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class MAIN_POPUP_MST
    {
        public int SEQ { get; set; }
        public string TITLE { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
}
