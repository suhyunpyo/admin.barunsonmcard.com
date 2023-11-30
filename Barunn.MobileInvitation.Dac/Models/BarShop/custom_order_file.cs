using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_file
    {
        public int order_seq { get; set; }
        public string cont_file1 { get; set; }
        public string cont_file2 { get; set; }
        public string cont_file3 { get; set; }
        public string cont_file4 { get; set; }
        public string env_file1 { get; set; }
        public string env_file2 { get; set; }
        public string env_file3 { get; set; }
        public string env_file4 { get; set; }
        public string env_file5 { get; set; }
        public DateTime regdate { get; set; }
    }
}
