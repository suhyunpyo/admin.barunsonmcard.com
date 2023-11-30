using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SmartAD_Partner_AD_IMAGE
    {
        public int IMAGE_SEQ { get; set; }
        public int AD_SEQ { get; set; }
        public string IMAGE_TYPE { get; set; }
        public string IMG_URL { get; set; }
        public DateTime? REG_DATE { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_ID { get; set; }
    }
}
