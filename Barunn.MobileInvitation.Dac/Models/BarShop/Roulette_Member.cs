using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Roulette_Member
    {
        public int rotm_Idx { get; set; }
        public string rotm_UID { get; set; }
        public int rotm_rot_idx { get; set; }
        public int rotm_ST { get; set; }
        public int rotm_rots_Idx { get; set; }
        public DateTime rotm_regdate { get; set; }
        public string rotm_coupon_code { get; set; }
        public int rotm_status { get; set; }
        public int? rotm_order_seq { get; set; }
        public string rotm_phone { get; set; }
    }
}
