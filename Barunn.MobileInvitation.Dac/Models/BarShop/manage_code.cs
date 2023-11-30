using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class manage_code
    {
        public string code_type { get; set; }
        public string code { get; set; }
        public string code_value { get; set; }
        public string etc1 { get; set; }
        public string etc2 { get; set; }
        public int? seq { get; set; }
        public string etc3 { get; set; }
        public string use_yorn { get; set; }
        public int code_id { get; set; }
        public int? parent_id { get; set; }
    }
}
