using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class WeddingHall_Log
    {
        public int id { get; set; }
        public string gubun { get; set; }
        public int key_idx { get; set; }
        public string admin_id { get; set; }
        public string note { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
