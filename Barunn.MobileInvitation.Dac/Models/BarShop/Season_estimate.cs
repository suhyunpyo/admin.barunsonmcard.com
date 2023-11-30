using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Season_estimate
    {
        public int seq { get; set; }
        public string osheet_div { get; set; }
        public string com_name { get; set; }
        public string com_place { get; set; }
        public string com_address { get; set; }
        public string charge_name { get; set; }
        public string charge_position { get; set; }
        public string charge_email { get; set; }
        public string phone { get; set; }
        public string phone_extension { get; set; }
        public string hphone { get; set; }
        public string card_div { get; set; }
        public string card_code { get; set; }
        public int order_count { get; set; }
        public string color_opt { get; set; }
        public string isinpaper { get; set; }
        public string ishandmade { get; set; }
        public string isenvinsert { get; set; }
        public string isembo { get; set; }
        public string isprintadd { get; set; }
        public string etc_message { get; set; }
        public string upload_file { get; set; }
        public string reply_yn { get; set; }
        public DateTime rdate { get; set; }
    }
}
