using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class XBBS_ARTICLE
    {
        public int XA_SEQ { get; set; }
        public int? XI_SEQ { get; set; }
        public int XA_RSEQ { get; set; }
        public string XA_TITLE { get; set; }
        public string XA_WRITER { get; set; }
        public string XA_WRITER_IP { get; set; }
        public string XA_PASSWD { get; set; }
        public string XA_EMAIL { get; set; }
        public string XA_HOMEPAGE { get; set; }
        public string XA_CONTENT { get; set; }
        public int XA_VIEW_CNT { get; set; }
        public int XA_RECMD_CNT { get; set; }
        public int? XA_THREAD { get; set; }
        public int XA_DEPTH { get; set; }
        public int XA_POSITION { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string XA_FILE { get; set; }

        public virtual XBBS_INFO XI_SEQNavigation { get; set; }
    }
}
