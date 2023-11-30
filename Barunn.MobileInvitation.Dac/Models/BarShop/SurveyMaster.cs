using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SurveyMaster
    {
        public int SurveyNo { get; set; }
        public int CompanySeq { get; set; }
        public string SalesGubun { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string SendMessage { get; set; }
        public DateTime FromDT { get; set; }
        public DateTime ToDT { get; set; }
        public string UseYN { get; set; }
        public DateTime RegDT { get; set; }
    }
}
