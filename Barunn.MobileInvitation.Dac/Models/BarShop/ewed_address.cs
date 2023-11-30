using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_address
    {
        public int no { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string addr { get; set; }
        public string email { get; set; }
        public string hp { get; set; }
        public byte gcode { get; set; }
        public DateTime rdate { get; set; }
        public string birth_y { get; set; }
        public string birth_m { get; set; }
        public string birth_d { get; set; }
        public string h_zip1 { get; set; }
        public string h_zip2 { get; set; }
        public string h_addr { get; set; }
        public string h_addr_detail { get; set; }
        public string h_phone { get; set; }
        public string company { get; set; }
        public string c_zip1 { get; set; }
        public string c_zip2 { get; set; }
        public string c_addr { get; set; }
        public string c_addr_detail { get; set; }
        public string c_phone { get; set; }
        public string memo { get; set; }
    }
}
