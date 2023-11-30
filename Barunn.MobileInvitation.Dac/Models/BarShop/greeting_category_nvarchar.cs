using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class greeting_category_nvarchar
    {
        public int greeting_category_seq { get; set; }
        public string greeting_category_name { get; set; }
        public int category_upper_code { get; set; }
        public short? display_seq { get; set; }
        public byte? depth { get; set; }
    }
}
