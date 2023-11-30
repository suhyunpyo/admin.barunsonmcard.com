using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_BUY_CARD_CONTENT
    {
        public int buy_card_SEQ { get; set; }
        public string buy_GROOM { get; set; }
        public string buy_BRIDE { get; set; }
        public string buy_YEAR { get; set; }
        public string buy_MONTH { get; set; }
        public string buy_DAY { get; set; }
        public string buy_WEEK { get; set; }
        public string buy_AMPM { get; set; }
        public string buy_HOUR { get; set; }
        public string buy_MINUTE { get; set; }
        public string buy_LUNAR { get; set; }
        public string buy_PLACE { get; set; }
        public string buy_MENT { get; set; }
        public string buy_MENT2 { get; set; }
        public string place_file_insert { get; set; }
        public string place_file_path { get; set; }
        public string place_name { get; set; }
        public string place_addr { get; set; }
        public string place_phone { get; set; }
        public string we_file_path { get; set; }
        public string man_description { get; set; }
        public string woman_description { get; set; }
        public string we_meet { get; set; }
        public string resolution { get; set; }

        public virtual ewed_BUY_CARD buy_card_SEQNavigation { get; set; }
    }
}
