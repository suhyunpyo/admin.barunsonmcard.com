using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class The_Ewed_BGM
    {
        public int BGM_ID { get; set; }
        public string Album { get; set; }
        public string Singer { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Free { get; set; }
        public string File_Path { get; set; }
        public bool? State { get; set; }
    }
}
