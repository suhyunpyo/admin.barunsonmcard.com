using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tEventBotItem
    {
        public int BotItemIdx { get; set; }
        public int BotIdx { get; set; }
        public int EventIdx { get; set; }
        public string BotItemCD { get; set; }
        public int BotOrderNo { get; set; }
        public string BotSize { get; set; }
    }
}
