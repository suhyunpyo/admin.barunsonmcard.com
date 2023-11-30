using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class MMS_LOG
    {
        public long MSGKEY { get; set; }
        public string SUBJECT { get; set; }
        public string PHONE { get; set; }
        public string CALLBACK { get; set; }
        public string STATUS { get; set; }
        public DateTime REQDATE { get; set; }
        public string MSG { get; set; }
        public int FILE_CNT { get; set; }
        public int? FILE_CNT_REAL { get; set; }
        public string FILE_PATH1 { get; set; }
        public int? FILE_PATH1_SIZ { get; set; }
        public string FILE_PATH2 { get; set; }
        public int? FILE_PATH2_SIZ { get; set; }
        public string FILE_PATH3 { get; set; }
        public int? FILE_PATH3_SIZ { get; set; }
        public string FILE_PATH4 { get; set; }
        public int? FILE_PATH4_SIZ { get; set; }
        public string FILE_PATH5 { get; set; }
        public int? FILE_PATH5_SIZ { get; set; }
        public string EXPIRETIME { get; set; }
        public DateTime? SENTDATE { get; set; }
        public DateTime? RSLTDATE { get; set; }
        public DateTime? REPORTDATE { get; set; }
        public DateTime? TERMINATEDDATE { get; set; }
        public string RSLT { get; set; }
        public string TYPE { get; set; }
        public string TELCOINFO { get; set; }
        public string ID { get; set; }
        public string POST { get; set; }
        public string ETC1 { get; set; }
        public string ETC2 { get; set; }
        public string ETC3 { get; set; }
        public int? ETC4 { get; set; }
    }
}
