using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class STORE_ORDER_DATE_SEND_LOG
    {
        public int Seq { get; set; }
        public string Type { get; set; }
        public int Uid { get; set; }
        public string Member_ID { get; set; }
        public byte Status_Seq { get; set; }
        public string Memo { get; set; }
        public string RefererURL { get; set; }
        public string IP { get; set; }
        public string User_Agent { get; set; }
        public string Auto_Yn { get; set; }
        public DateTime Reg_Date { get; set; }
    }
}
