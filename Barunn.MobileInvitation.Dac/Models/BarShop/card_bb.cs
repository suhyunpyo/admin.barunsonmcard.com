using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class card_bb
    {
        public int pk_id { get; set; }
        public string forum_id { get; set; }
        public int? group_id { get; set; }
        public int? group_p { get; set; }
        public int? group_order { get; set; }
        public int? msglevel { get; set; }
        public string subject { get; set; }
        public string uid { get; set; }
        public string writer { get; set; }
        public string email { get; set; }
        public int? readno { get; set; }
        public string passwd { get; set; }
        public DateTime? writedate { get; set; }
        public string homepage { get; set; }
        public string ip { get; set; }
        public string tag { get; set; }
        public string content { get; set; }
        public string hide { get; set; }
    }
}
