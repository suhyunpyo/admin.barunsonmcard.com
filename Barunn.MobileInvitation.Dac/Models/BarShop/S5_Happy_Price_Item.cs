using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_Happy_Price_Item
    {
        public int hpi_idx { get; set; }
        public int hpi_hp_idx { get; set; }
        public int hpi_Card_seq { get; set; }
        public string hpi_title { get; set; }
        public int hpi_limit_cnt { get; set; }
        public int hpi_sale_cnt { get; set; }
        public DateTime hpi_regdate { get; set; }
        public int hpi_status { get; set; }
    }
}
