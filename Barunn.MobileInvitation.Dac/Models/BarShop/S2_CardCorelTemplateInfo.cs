using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardCorelTemplateInfo
    {
        public int SEQ { get; set; }
        public string CARD_CODE { get; set; }
        public string GROUP_CODE { get; set; }
        public string ERP_CODE { get; set; }
        public string INSIDE_PRINT_YORN { get; set; }
        public string OUTSIDE_PRINT_YORN { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
