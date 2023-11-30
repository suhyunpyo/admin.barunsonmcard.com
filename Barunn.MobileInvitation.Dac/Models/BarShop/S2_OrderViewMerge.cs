using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_OrderViewMerge
    {
        public string sales_gubun { get; set; }
        public int order_seq { get; set; }
        public byte? procLevel { get; set; }
        public DateTime? src_confirm_date { get; set; }
        public string order_type { get; set; }
        public string order_name { get; set; }
        public string pay_type { get; set; }
        public byte? printW_status { get; set; }
        public int? order_count { get; set; }
        public string isColorPrint { get; set; }
        public string isColorInpaper { get; set; }
        public string isEmbo { get; set; }
        public string isCorel { get; set; }
        public int? card_seq { get; set; }
        public string card_div { get; set; }
        public int? unicef_price { get; set; }
        public string print_type { get; set; }
        public string CARD_CODE { get; set; }
        public string card_code_str { get; set; }
        public string print_group { get; set; }
        public int print_sizeH { get; set; }
        public string isDigital { get; set; }
        public string isinpaper { get; set; }
        public string isFPrint { get; set; }
    }
}
