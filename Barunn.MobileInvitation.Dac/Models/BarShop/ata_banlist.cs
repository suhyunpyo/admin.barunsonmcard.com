using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ata_banlist
    {
        public string service_type { get; set; }
        public int ban_seq { get; set; }
        public string ban_type { get; set; }
        public string content { get; set; }
        public string send_yn { get; set; }
        public string ban_status_yn { get; set; }
        public DateTime reg_date { get; set; }
        public string reg_user { get; set; }
        public DateTime update_date { get; set; }
        public string update_user { get; set; }
    }
}
