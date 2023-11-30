using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_card_faq
    {
        public int pkid { get; set; }
        public int? group_order { get; set; }
        public string subject { get; set; }
        public DateTime? writedate { get; set; }
        public string content { get; set; }
        public string hide { get; set; }
        public string forum_id { get; set; }
    }
}
