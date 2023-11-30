using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Admin_LoginIpInfo
    {
        [Key]
        public int seq { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string ip { get; set; }
        public string referer_url { get; set; }
        public string user_agent { get; set; }
        public DateTime regdate { get; set; }
    }
}
