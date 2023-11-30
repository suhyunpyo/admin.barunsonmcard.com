using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Branch_QA_Tbl
    {
        public int QA_IID { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string MEMBER_ID { get; set; }
        public string MEMBER_NAME { get; set; }
        public string E_MAIL { get; set; }
        public string TEL_NO { get; set; }
        public string Q_KIND { get; set; }
        public string Q_TITLE { get; set; }
        public string Q_CONTENT { get; set; }
        public string A_ID { get; set; }
        public DateTime? A_DT { get; set; }
        public string A_CONTENT { get; set; }
        public string A_STAT { get; set; }
        public DateTime REG_DT { get; set; }
    }
}
