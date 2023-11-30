using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class BENEFIT_BANNER
    {
        public int SEQ { get; set; }
        public int? COMPANY_SEQ { get; set; }
        public string B_TYPE { get; set; }
        public int B_TYPE_NO { get; set; }
        public string DISPLAY_YN { get; set; }
        public string EVENT_S_DT { get; set; }
        public string EVENT_E_DT { get; set; }
        public string MAIN_TITLE { get; set; }
        public string SUB_TITLE { get; set; }
        public string PAGE_URL { get; set; }
        public string B_IMG { get; set; }
        public string B_BACK_COLOR { get; set; }
        public string WING_IMG { get; set; }
        public string WING_YN { get; set; }
        public string BAND_YN { get; set; }
        public string NEW_BLANK_YN { get; set; }
        public string JEHU_YN { get; set; }
        public string DELETE_YN { get; set; }
        public string END_YN { get; set; }
        public string REPLACE_YN { get; set; }
        public string ALWAYS_YN { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_UID { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string UPDATED_UID { get; set; }
    }
}
