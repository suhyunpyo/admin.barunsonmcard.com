using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_Music_Datum
    {
        public int seq { get; set; }
        public string m_title { get; set; }
        public string m_file { get; set; }
        public int use_cnt { get; set; }
        public DateTime mdate { get; set; }
    }
}
