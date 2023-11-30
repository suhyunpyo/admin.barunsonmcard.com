using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Visit_Reservation
    {
        public int visit_seq { get; set; }
        public string visit_name { get; set; }
        public string visit_date { get; set; }
        public string visit_time { get; set; }
        public string tel_no1 { get; set; }
        public string tel_no2 { get; set; }
        public string tel_no3 { get; set; }
        public string visit_content { get; set; }
        public DateTime RegDate { get; set; }
        public string chk_call { get; set; }
        public string domain_info { get; set; }
        public string wedd_date { get; set; }
        public string wedd_time { get; set; }
        public string gubun { get; set; }
    }
}
