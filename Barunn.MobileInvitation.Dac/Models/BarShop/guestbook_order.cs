using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class guestbook_order
    {
        public int order_seq { get; set; }
        public string item_name1 { get; set; }
        public int item_count1 { get; set; }
        public string order_email { get; set; }
        public string order_name { get; set; }
        public string up_img1 { get; set; }
        public string up_file1 { get; set; }
        public string order_status { get; set; }
        public string order_memo { get; set; }
        public string request_date { get; set; }
        public string print_date { get; set; }
        public string print_commit_date { get; set; }
        public string request_admin { get; set; }
        public string item_name2 { get; set; }
        public int? item_count2 { get; set; }
        public string item_name3 { get; set; }
        public int? item_count3 { get; set; }
    }
}
