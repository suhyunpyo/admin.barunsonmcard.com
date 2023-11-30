using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_printjob
    {
        public string pdate { get; set; }
        public string cdate { get; set; }
        public int cseq { get; set; }
        public long pid { get; set; }
        public int pcount { get; set; }
        public string ptype { get; set; }
        public string printer_id { get; set; }
        public int id { get; set; }
        public DateTime? printer_date { get; set; }
    }
}
