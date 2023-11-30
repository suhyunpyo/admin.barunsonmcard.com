using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CARD_FREE_FOOD_TICKET_MST
    {
        public int SEQ { get; set; }
        public int CARD_SEQ { get; set; }
        public int COMPANY_SEQ { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public string USE_JEHU_YORN { get; set; }
        public string USE_YORN { get; set; }
        public DateTime REG_DATE { get; set; }
    }
}
