using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SurveyResponse
    {
        public int SurveyNo { get; set; }
        public int QuestionNo { get; set; }
        public int ResponseNo { get; set; }
        public int? ReplyAnswerNo { get; set; }
        public string MemberID { get; set; }
        public string Comment { get; set; }
        public DateTime RegDT { get; set; }
    }
}
