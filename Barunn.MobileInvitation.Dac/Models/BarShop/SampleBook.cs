using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SampleBook
    {
        public int Seq { get; set; }
        public string SampleBook_ID { get; set; }
        public string Delivery_YN { get; set; }
        public byte Delivery_Status { get; set; }
        public int Delivery_Count { get; set; }
        public string SampleBook_Condition { get; set; }
        public string Admin_Memo { get; set; }
        public string Admin_ID { get; set; }
        public DateTime? Regist_Date { get; set; }
    }
}
