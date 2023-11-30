using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class board_basic_info
    {
        public int Idx { get; set; }
        public string siteid { get; set; }
        public int? type { get; set; }
        public int? theme { get; set; }
        public string title { get; set; }
        public string helper { get; set; }
        public int? write { get; set; }
        public int? view { get; set; }
        public string writer_view { get; set; }
        public string status { get; set; }
        public string admin_name { get; set; }
        public string reply { get; set; }
        public int? reply_level { get; set; }
        public string answer { get; set; }
        public int? answer_level { get; set; }
        public string pwd { get; set; }
        public string editor { get; set; }
        public DateTime regdate { get; set; }
        public DateTime? editdate { get; set; }
    }
}
