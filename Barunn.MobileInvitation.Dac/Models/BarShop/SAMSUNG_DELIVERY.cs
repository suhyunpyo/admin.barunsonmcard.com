using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SAMSUNG_DELIVERY
    {
        public int id { get; set; }
        public string branch_code { get; set; }
        public string send_name { get; set; }
        public string send_zip { get; set; }
        public string send_Addr1 { get; set; }
        public string send_addr2 { get; set; }
        public string recv_name { get; set; }
        public string recv_phone { get; set; }
        public string recv_zip { get; set; }
        public string recv_addr1 { get; set; }
        public string recv_addr2 { get; set; }
        public byte? greeting_type { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
