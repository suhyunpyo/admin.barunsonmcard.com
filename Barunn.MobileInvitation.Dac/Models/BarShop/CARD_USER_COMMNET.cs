using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_USER_COMMNET
    {
        public int CMT_SEQ { get; set; }
        public string sales_Gubun { get; set; }
        public int? company_seq { get; set; }
        public string MEMBER_UID { get; set; }
        public string MEMBER_NAME { get; set; }
        public string COMMENT { get; set; }
        public string SERVICE_MENT { get; set; }
        public DateTime? REGDATE { get; set; }
        public int CARD_SEQ { get; set; }
        public byte? score { get; set; }
        public string DIV { get; set; }
        public DateTime? WEDD_DT { get; set; }
        public string WEDD_PLACE { get; set; }
        public string TRAVEL_PLACE { get; set; }
        public string TITLE { get; set; }
        public string tmp_data { get; set; }
        public string IsBest { get; set; }
        public string IsDP { get; set; }
    }
}
