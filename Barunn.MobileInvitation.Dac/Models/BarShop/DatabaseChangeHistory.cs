using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DatabaseChangeHistory
    {
        public int EventID { get; set; }
        public string ObjectName { get; set; }
        public string EventType { get; set; }
        public string CommandText { get; set; }
        public string HostName { get; set; }
        public string HostIP { get; set; }
        public DateTime? EventDT { get; set; }
    }
}
