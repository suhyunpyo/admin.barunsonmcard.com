using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class greeting_nvarchar
    {
        public int greeting_seq { get; set; }
        public int greeting_category_seq { get; set; }
        public string greeting_name { get; set; }
        public string greeting_content { get; set; }
        public string isDP { get; set; }
    }
}
