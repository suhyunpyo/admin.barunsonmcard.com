using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_UserComment
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public int card_seq { get; set; }
        public string card_code { get; set; }
        public int? order_seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public int? score { get; set; }
        public int sym_cnt { get; set; }
        public string isBest { get; set; }
        public string isDP { get; set; }
        public string best_year { get; set; }
        public string best_month { get; set; }
        public string best_week { get; set; }
        public DateTime reg_date { get; set; }
        public int? resch_color { get; set; }
        public int? resch_bright { get; set; }
        public string upimg { get; set; }
        public string comm_div { get; set; }
        public string b_url { get; set; }
        public DateTime? edit_date { get; set; }
        public string EVENT_STATUS_CODE { get; set; }
        public string device_type { get; set; }
        public int? resch_price { get; set; }
    }
}
