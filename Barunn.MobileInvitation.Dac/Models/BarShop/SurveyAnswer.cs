using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SurveyAnswer
    {
        public int SurveyNo { get; set; }
        public int QuestionNo { get; set; }
        public int AnswerNo { get; set; }
        public string AnswerText { get; set; }
        public string UseYN { get; set; }
        public DateTime RegDT { get; set; }
    }
}
