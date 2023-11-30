using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SampleBook_History
    {
        public int Seq { get; set; }
        public string SampleBook_ID { get; set; }
        public string SampleBook_Condition { get; set; }
        public string Admin_Memo { get; set; }
        public string Admin_ID { get; set; }
        public DateTime? Regist_Date { get; set; }
    }
}
