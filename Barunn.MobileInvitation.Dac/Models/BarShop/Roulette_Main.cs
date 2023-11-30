using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Roulette_Main
    {
        public int rot_idx { get; set; }
        public int? rot_company_seq { get; set; }
        public string rot_title { get; set; }
        public DateTime? rot_sDate { get; set; }
        public DateTime? rot_Edate { get; set; }
        public DateTime? rot_regdate { get; set; }
        public int? rot_Status { get; set; }
        public int? rot_ST { get; set; }
        public int? last_ST { get; set; }
        public string ing_ST { get; set; }
        public int? comment_code { get; set; }
        public int? rot_limit_price { get; set; }
    }
}
