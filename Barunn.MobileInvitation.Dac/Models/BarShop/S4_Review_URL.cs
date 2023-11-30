using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Review_URL
    {
        public int Review_Id { get; set; }
        public string Review_Group { get; set; }
        public string Company_Seq { get; set; }
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public string CellPhone { get; set; }
        public string Review_Url { get; set; }
        public string Review_Url2 { get; set; }
        public DateTime? Review_Date { get; set; }
        public string Evaluator { get; set; }
        public short? Evaluate_Tag { get; set; }
        public string Evaluate_Content { get; set; }
        public string Evaluate_Comment { get; set; }
        public DateTime? Evaluate_Date { get; set; }
        public string Send_Date { get; set; }
        public string View_Flag { get; set; }
    }
}
