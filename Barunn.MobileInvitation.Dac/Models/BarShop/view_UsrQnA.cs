using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_UsrQnA
    {
        public string isS2 { get; set; }
        public int qa_iid { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string company_name { get; set; }
        public int? order_seq { get; set; }
        public string CARD_CODE { get; set; }
        public string member_id { get; set; }
        public string member_name { get; set; }
        public string e_mail { get; set; }
        public string tel_no { get; set; }
        public string Q_kind { get; set; }
        public string Q_title { get; set; }
        public string Q_content { get; set; }
        public string A_stat { get; set; }
        public DateTime? a_dt { get; set; }
        public DateTime reg_dt { get; set; }
        public string a_research1 { get; set; }
        public string a_research2 { get; set; }
        public string a_research_date { get; set; }
        public string a_id { get; set; }
    }
}
