using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventBlog
    {
        public int Seq { get; set; }
        public int Company_Seq { get; set; }
        public string Uid { get; set; }
        public string Uname { get; set; }
        public int? Order_Seq { get; set; }
        public string Blog_URL { get; set; }
        public string Cafe_URL { get; set; }
        public string isSelection { get; set; }
        public DateTime Reg_Date { get; set; }
        public string comment { get; set; }
        public string isOpen { get; set; }
    }
}
