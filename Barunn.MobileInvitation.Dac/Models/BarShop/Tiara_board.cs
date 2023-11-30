using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Tiara_board
    {
        public int id { get; set; }
        public string member_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string hphone { get; set; }
        public byte category { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public string isSecret { get; set; }
        public string pw { get; set; }
        public string isCoupon { get; set; }
        public string zipcode { get; set; }
        public string addr { get; set; }
        public string addr_detail { get; set; }
        public int? qid { get; set; }
        public byte? depth { get; set; }
        public int? vcnt { get; set; }
        public DateTime? reg_date { get; set; }
    }
}
