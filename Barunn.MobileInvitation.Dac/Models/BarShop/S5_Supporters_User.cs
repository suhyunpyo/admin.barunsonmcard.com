using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_Supporters_User
    {
        public int SP_Idx { get; set; }
        public int SP_Company_seq { get; set; }
        public string SP_UserID { get; set; }
        public string SP_URL { get; set; }
        public int SP_Level { get; set; }
        public string SP_Best { get; set; }
        public DateTime SP_Regdate { get; set; }
        public int SP_Status { get; set; }
        public string SP_Comment { get; set; }
        public string SP_Title { get; set; }
        public string SP_Contents { get; set; }
        public DateTime? SP_Auth_Date { get; set; }
        public int? SP_SeasonNo { get; set; }
    }
}
