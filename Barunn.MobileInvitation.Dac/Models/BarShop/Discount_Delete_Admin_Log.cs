using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Discount_Delete_Admin_Log
    {
        public int SEQ { get; set; }
        public string ADMIN_ID { get; set; }
        public DateTime? DELETE_DATE { get; set; }
        public int DISCOUNT_SEQ { get; set; }
    }
}
