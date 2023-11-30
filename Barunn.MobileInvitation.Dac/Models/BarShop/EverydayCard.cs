using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EverydayCard
    {
        public int SEQ { get; set; }
        public string Prd_Code { get; set; }
        public string Prd_Name { get; set; }
        public string Prd_Size { get; set; }
        public int? Prd_Price { get; set; }
        public string Prd_Img { get; set; }
        public string Prd_Text { get; set; }
        public string Prd_View { get; set; }
        public DateTime? Prd_Date { get; set; }
    }
}
