using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SQM_QA_TBL
    {
        public int QA_IID { get; set; }
        public string sales_gubun { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string order_tbl { get; set; }
        public int? order_seq { get; set; }
        public string MEMBER_ID { get; set; }
        public string MEMBER_NAME { get; set; }
        public string E_MAIL { get; set; }
        public string TEL_NO { get; set; }
        public string isEmail { get; set; }
        public string isSMS { get; set; }
        public string isSecret { get; set; }
        public string Q_KIND { get; set; }
        public string Q_TITLE { get; set; }
        public string Q_CONTENT { get; set; }
        public string A_ID { get; set; }
        public DateTime? A_DT { get; set; }
        public string A_CONTENT { get; set; }
        public string A_STAT { get; set; }
        public string a_research1 { get; set; }
        public string a_research2 { get; set; }
        public string a_research_date { get; set; }
        public string user_upfile1 { get; set; }
        public string user_upfile2 { get; set; }
        public DateTime REG_DT { get; set; }
    }
}
