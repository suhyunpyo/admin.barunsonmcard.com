using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_PRIVATE_CHOICE
    {
        public string MEMBER_ID { get; set; }
        public DateTime? REGDATE { get; set; }
        public int CARD_SEQ { get; set; }
        public string CARD_TYPE { get; set; }
        public string Daum_uid { get; set; }
    }
}
