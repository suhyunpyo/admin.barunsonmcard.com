using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ADMIN_LST
    {
        public int id { get; set; }
        public string ADMIN_ID { get; set; }
        public string ADMIN_PASSWD { get; set; }
        public string ADMIN_NAME { get; set; }
        public string CMS_ID { get; set; }
        public string CMS_NUM { get; set; }
        public string ADMIN_PHONE { get; set; }
        public string ADMIN_EMAIL { get; set; }
        public string COMPANY_TYPE { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string COMPANY_GUBUN { get; set; }
        public byte? ADMIN_LEVEL { get; set; }
        public string isDesigner { get; set; }
        public string isDown { get; set; }
        public string isAlba { get; set; }
        public string isCS { get; set; }
        public string isDeveloper { get; set; }
        public string NState { get; set; }
        public byte? adLevel { get; set; }
        public string isPackingSMS { get; set; }
        public string COMPANY_TYPE_CODE { get; set; }
        public string isViewNoticeYN { get; set; }

        public virtual COMPANY COMPANY_SEQNavigation { get; set; }
    }
}
