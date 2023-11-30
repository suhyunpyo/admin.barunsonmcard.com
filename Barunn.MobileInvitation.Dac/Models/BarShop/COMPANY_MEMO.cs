using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COMPANY_MEMO
    {
        public int id { get; set; }
        public int company_seq { get; set; }
        public string memo { get; set; }
        public string admin_id { get; set; }
        public DateTime reg_date { get; set; }
    }
}
