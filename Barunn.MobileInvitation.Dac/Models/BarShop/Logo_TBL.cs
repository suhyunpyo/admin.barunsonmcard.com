using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Logo_TBL
    {
        public int id { get; set; }
        public string LogoName { get; set; }
        public string lcode { get; set; }
        public string lcategory { get; set; }
        public string filename { get; set; }
        public string src_filename { get; set; }
        public string admin_id { get; set; }
        public string flag { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
