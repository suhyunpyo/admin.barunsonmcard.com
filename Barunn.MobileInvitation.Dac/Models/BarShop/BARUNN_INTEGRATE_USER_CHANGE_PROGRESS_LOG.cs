using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BARUNN_INTEGRATE_USER_CHANGE_PROGRESS_LOG
    {
        public int SEQ { get; set; }
        public string DUPINFO { get; set; }
        public string ID { get; set; }
        public string STEP { get; set; }
        public string STEP_DESC { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
