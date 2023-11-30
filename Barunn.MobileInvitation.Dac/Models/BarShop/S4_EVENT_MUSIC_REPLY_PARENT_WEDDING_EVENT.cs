using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_EVENT_MUSIC_REPLY_PARENT_WEDDING_EVENT
    {
        public int REPLY_PARENT_WEDDING_EVENT_SEQ { get; set; }
        public int? EVENT_MUSIC_REPLY_SEQ { get; set; }
        public int? STATUS_CODE { get; set; }
        public string FATHER_NAME { get; set; }
        public string MOTHER_NAME { get; set; }
        public string PARENT_WEDDING_DAY { get; set; }
        public string WEDDING_DAY { get; set; }
        public string FRONT_IMG { get; set; }
        public string WEDDING_IMG { get; set; }
        public string BACK_IMG { get; set; }
        public DateTime? REG_DATE { get; set; }
    }
}
