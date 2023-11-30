using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CS_HappyCall
    {
        public int idx { get; set; }
        public int? sample_order_seq { get; set; }
        public byte? connect_yn { get; set; }
        public string product { get; set; }
        public byte? good_yn { get; set; }
        public string good_yn_etc { get; set; }
        public string good_yn_etc2 { get; set; }
        public byte? good_cont { get; set; }
        public string good_cont_etc { get; set; }
        public byte? inflow { get; set; }
        public string inflow_etc { get; set; }
        public byte? card_type { get; set; }
        public string card_type_etc { get; set; }
        public byte? card_money { get; set; }
        public byte? gift { get; set; }
        public string gift_etc { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
