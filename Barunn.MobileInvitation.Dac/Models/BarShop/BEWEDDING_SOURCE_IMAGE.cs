using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BEWEDDING_SOURCE_IMAGE
    {
        public int SOURCE_IMG_SEQ { get; set; }
        public int SOURCE_SEQ { get; set; }
        public string LOGO_TYPE { get; set; }
        public string LOGO_IMG_URL { get; set; }
        public DateTime? REG_DATE { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_ID { get; set; }
    }
}
