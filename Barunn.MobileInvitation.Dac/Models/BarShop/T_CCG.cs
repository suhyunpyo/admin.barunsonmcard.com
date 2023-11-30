using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class T_CCG
    {
        public string member_id { get; set; }
        public DateTime reg_date { get; set; }
        public DateTime? cancel_date { get; set; }
    }
}
