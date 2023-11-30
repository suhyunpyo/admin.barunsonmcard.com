using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_miniCD_INFO
    {
        public int card_seq { get; set; }
        public string CATE_L_CODE { get; set; }
        public string CATE_S_CODE { get; set; }
        public int? CARD_NO { get; set; }
        public string CARD_NAME { get; set; }
        public string CARD_TITLE { get; set; }
        public int CARD_PRICE { get; set; }
        public int? CARD_SALE_PRICE { get; set; }
        public DateTime CARD_INDATE { get; set; }
        public int? USE_CNT { get; set; }
        public string isFull { get; set; }
        public string wd_width { get; set; }
        public string wd_height { get; set; }
        public string wd_size { get; set; }
        public string wd_bgcolor { get; set; }
        public string CARD_SWF { get; set; }
        public string CARD_IMG_S { get; set; }
        public string CARD_IMG_M { get; set; }
        public string CARD_SHAPE { get; set; }
        public string CARD_COMPOSITION { get; set; }
        public string CARD_FEATURE { get; set; }
        public string CARD_EtcMsg { get; set; }
        public string CARD_DESCRIPTION { get; set; }
        public string isBest { get; set; }
        public string isNew { get; set; }
        public string isDisplay { get; set; }
        public int? Display_order { get; set; }
        public string visit_use_yn { get; set; }
        public string attend_use_yn { get; set; }
        public string miniCD_use_yn { get; set; }
        public string auto_yn { get; set; }
        public string swf_type { get; set; }
    }
}
