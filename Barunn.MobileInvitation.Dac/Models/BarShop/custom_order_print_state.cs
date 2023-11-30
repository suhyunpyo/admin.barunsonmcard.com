using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_print_state
    {
        public long id { get; set; }
        public short printer_id { get; set; }
        public string pdate { get; set; }
        public int pseq { get; set; }
        public DateTime reg_date { get; set; }
    }
}
