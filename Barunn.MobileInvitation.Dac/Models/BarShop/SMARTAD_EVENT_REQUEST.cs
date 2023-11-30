using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SMARTAD_EVENT_REQUEST
    {
        public int REQUEST_SEQ { get; set; }
        public int AD_EVENT_SEQ { get; set; }
        public DateTime? REQUEST_DATE { get; set; }
        public string REQUEST_MSG { get; set; }
    }
}
