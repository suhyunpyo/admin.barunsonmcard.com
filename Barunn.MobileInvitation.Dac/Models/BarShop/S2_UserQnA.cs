using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_UserQnA
    {
        public int qa_iid { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string card_code { get; set; }
        public int? order_seq { get; set; }
        public string member_id { get; set; }
        public string member_name { get; set; }
        public string e_mail { get; set; }
        public string tel_no { get; set; }
        public string chk_mail_input { get; set; }
        public string q_kind { get; set; }
        public string q_title { get; set; }
        public string q_content { get; set; }
        public string isMail { get; set; }
        public string isSMS { get; set; }
        public string isSecret { get; set; }
        public string user_upfile1 { get; set; }
        public string user_upfile2 { get; set; }
        public string admin_upfile1 { get; set; }
        public string a_id { get; set; }
        public DateTime? a_dt { get; set; }
        public string a_content { get; set; }
        public string a_stat { get; set; }
        public DateTime reg_dt { get; set; }
        public string a_research1 { get; set; }
        public string a_research2 { get; set; }
        public string a_research_date { get; set; }
        public string inflow { get; set; }
    }
}
