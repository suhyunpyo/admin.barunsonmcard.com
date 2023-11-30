using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SC_LOG
    {
        public int TR_NUM { get; set; }
        public DateTime TR_SENDDATE { get; set; }
        public string TR_ID { get; set; }
        public string TR_SENDSTAT { get; set; }
        public string TR_RSLTSTAT { get; set; }
        public string TR_MSGTYPE { get; set; }
        public string TR_PHONE { get; set; }
        public string TR_CALLBACK { get; set; }
        public DateTime? TR_RSLTDATE { get; set; }
        public DateTime? TR_MODIFIED { get; set; }
        public string TR_MSG { get; set; }
        public string TR_ETC1 { get; set; }
        public string TR_ETC2 { get; set; }
        public string TR_ETC3 { get; set; }
        public string TR_ETC4 { get; set; }
        public string TR_ETC5 { get; set; }
        public string TR_ETC6 { get; set; }
        public string TR_NET { get; set; }
        public int? TR_SERIALNUM { get; set; }
        public DateTime? TR_REALSENDDATE { get; set; }
    }
}
