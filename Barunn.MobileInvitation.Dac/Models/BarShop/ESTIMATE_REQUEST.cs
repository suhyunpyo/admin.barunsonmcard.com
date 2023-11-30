using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ESTIMATE_REQUEST
    {
        public int seq { get; set; }
        public string sales_gubun { get; set; }
        public int company_seq { get; set; }
        public string company_name { get; set; }
        public string company_location { get; set; }
        public string person_name { get; set; }
        public string person_email { get; set; }
        public string person_phone1 { get; set; }
        public string person_phone2 { get; set; }
        public string person_phone3 { get; set; }
        public string product_type { get; set; }
        public string product_content { get; set; }
        public string product_count { get; set; }
        public DateTime? product_duedate { get; set; }
        public string etc_upfile { get; set; }
        public string etc_content { get; set; }
        public string chk_admin { get; set; }
        public DateTime? chk_date { get; set; }
        public string delete_ind { get; set; }
        public DateTime? CREATED_TMSTMP { get; set; }
        public string CREATED_USERID { get; set; }
        public string admin_content { get; set; }
    }
}
