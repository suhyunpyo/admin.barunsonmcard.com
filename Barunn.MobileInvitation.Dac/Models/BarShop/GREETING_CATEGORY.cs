using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class GREETING_CATEGORY
    {
        public GREETING_CATEGORY()
        {
            GREETINGs = new HashSet<GREETING>();
        }

        public int GREETING_CATEGORY_SEQ { get; set; }
        public string GREETING_CATEGORY_NAME { get; set; }
        public int CATEGORY_UPPER_CODE { get; set; }
        public string CATEGORY_DESCRIPTION { get; set; }
        public DateTime? REGIST_DATE { get; set; }
        public DateTime? LAST_UPDATE { get; set; }
        public string CATEGORY_USED { get; set; }
        public byte? DEPTH { get; set; }
        public short? display_seq { get; set; }

        public virtual ICollection<GREETING> GREETINGs { get; set; }
    }
}
