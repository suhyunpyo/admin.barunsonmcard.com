using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SurveyQuestion
    {
        public int SurveyNo { get; set; }
        public int QuestionNo { get; set; }
        public string QuestionText { get; set; }
        public string UseYN { get; set; }
        public DateTime RegDT { get; set; }
    }
}
