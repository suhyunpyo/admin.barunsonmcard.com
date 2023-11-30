using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Admin_Menu
    {
        public int AM_Idx { get; set; }
        public int? AM_SiteID { get; set; }
        public string AM_Code1 { get; set; }
        public string AM_Code2 { get; set; }
        public string AM_Code3 { get; set; }
        public string AM_Code_Merge { get; set; }
        public string AM_Name { get; set; }
        public string AM_URL { get; set; }
        public int AM_Status { get; set; }
        public int? AM_Sort { get; set; }
        public DateTime AM_Regdate { get; set; }
        public string AM_Image1 { get; set; }
        public string AM_Image2 { get; set; }
        public string AM_Image3 { get; set; }
    }
}
