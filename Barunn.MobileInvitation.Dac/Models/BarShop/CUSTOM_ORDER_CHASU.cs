using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ORDER_CHASU
    {
        public long id { get; set; }
        public string pdate { get; set; }
        public int? pseq { get; set; }
        public int order_seq { get; set; }
        public int? oseq { get; set; }
        public DateTime? proc_date1 { get; set; }
        public DateTime? proc_date2 { get; set; }
        public DateTime? proc_date3 { get; set; }
        public DateTime? proc_date4 { get; set; }
        public string pdate_Real { get; set; }
    }
}
