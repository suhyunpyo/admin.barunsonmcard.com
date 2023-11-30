using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_UserBye
    {
        public int seq { get; set; }
        public string uid { get; set; }
        public string reason_1 { get; set; }
        public string reason_2 { get; set; }
        public string reason_3 { get; set; }
        public string reason_4 { get; set; }
        public string reason_5 { get; set; }
        public string reason_6 { get; set; }
        public string reason_7 { get; set; }
        public string reason_txt { get; set; }
        public DateTime reg_date { get; set; }
        public int? company_seq { get; set; }
        public string RequestNumber { get; set; }
        public string AuthType { get; set; }
        public string DupInfo { get; set; }
        public string ConnInfo { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string NationalInfo { get; set; }
    }
}
