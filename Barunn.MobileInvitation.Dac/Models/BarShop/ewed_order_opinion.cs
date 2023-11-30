using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_order_opinion
    {
        public int BBS_SEQ { get; set; }
        public string TITLE { get; set; }
        public string WRITER { get; set; }
        public string WRITER_IP { get; set; }
        public string CONTENT { get; set; }
        public string REPLY { get; set; }
        public int VIEW_CNT { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string FILE_PATH { get; set; }
        public string STATUS { get; set; }
        public int ORDER_SEQ { get; set; }
        public string EMAIL { get; set; }
        public string NOTIFY_EMAIL_YESORNO { get; set; }
        public string upfile { get; set; }
    }
}
