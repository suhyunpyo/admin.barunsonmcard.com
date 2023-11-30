using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Category_Manage
    {
        public int CM_Idx { get; set; }
        public int CM_SiteID { get; set; }
        public string CM_Code1 { get; set; }
        public string CM_Code2 { get; set; }
        public string CM_Code3 { get; set; }
        public string CM_Code_Merge { get; set; }
        public string CM_Name { get; set; }
        public int CM_Status { get; set; }
        public DateTime CM_Regdate { get; set; }
        public string CM_Banner { get; set; }
    }
}
