using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_Order_MemoDay
    {
        public int m_seq { get; set; }
        public int order_seq { get; set; }
        public byte seq { get; set; }
        public string m_year { get; set; }
        public string m_month { get; set; }
        public string m_day { get; set; }
        public string m_title { get; set; }
        public string m_contents { get; set; }
    }
}
