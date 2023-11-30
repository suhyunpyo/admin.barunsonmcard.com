using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tEventBotTemplete
    {
        public int BotIdx { get; set; }
        public int EventIdx { get; set; }
        public int BotOrderNo { get; set; }
        public string BotCopy { get; set; }
        public string BotBanner { get; set; }
        public string BotUrl { get; set; }
        public string BotType { get; set; }
        public string addBtnYN { get; set; }
        public string addBtnUrl { get; set; }
        public string BotNavi { get; set; }
        public int? BotReview { get; set; }
        public int? BotStock { get; set; }
        public int? BotSaleCnt { get; set; }
    }
}
