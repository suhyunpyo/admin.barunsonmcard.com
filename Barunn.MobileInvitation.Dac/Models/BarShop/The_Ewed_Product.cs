using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class The_Ewed_Product
    {
        public int Product_ID { get; set; }
        public string Product_Div { get; set; }
        public string Title { get; set; }
        public int? Price { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string Designer { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string Samp_FileName { get; set; }
        public short? Product_Number { get; set; }
        public bool? State { get; set; }
        public string Sales_Gubun { get; set; }
    }
}
