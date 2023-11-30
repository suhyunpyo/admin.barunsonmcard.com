using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EventManager
    {
        public int seq { get; set; }
        public int company_seq { get; set; }
        public string EventName { get; set; }
        public string EventUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte EventKind { get; set; }
        public string ManagerComment { get; set; }
        public byte isDeleted { get; set; }
        public string EventListImage { get; set; }
        public string EventDescription { get; set; }
        public byte isJehu { get; set; }
    }
}
