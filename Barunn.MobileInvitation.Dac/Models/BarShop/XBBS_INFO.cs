using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class XBBS_INFO
    {
        public XBBS_INFO()
        {
            XBBS_ARTICLEs = new HashSet<XBBS_ARTICLE>();
        }

        public int XI_SEQ { get; set; }
        public string XI_TITLE { get; set; }
        public string XI_TITLE_IMG { get; set; }
        public string XI_DESCRIPTION { get; set; }
        public string XI_PUBLIC { get; set; }
        public int XI_ROWS { get; set; }
        public int XI_PAGES { get; set; }
        public string XI_TMP_FILE { get; set; }
        public DateTime? REG_DATE { get; set; }

        public virtual ICollection<XBBS_ARTICLE> XBBS_ARTICLEs { get; set; }
    }
}
