using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SmartAD_Partner
    {
        public int PARTNER_SEQ { get; set; }
        public string PARTNER_ID { get; set; }
        public string PARTNER_NAME { get; set; }
        public string PARTNER_TELEPHONE { get; set; }
        public string PARTNER_CELLPHONE { get; set; }
        public string PARTNER_ADMIN_NAME { get; set; }
        public string PARTNER_ADMIN_POSITION { get; set; }
        public string PARTNER_ADMIN_DEPARTMENT { get; set; }
        public string PARTNER_EMAIL { get; set; }
        public string PARTNER_PASSWORD { get; set; }
        public string PARTNER_HOMEPAGE { get; set; }
        public string PARTNER_CONTENT { get; set; }
        public DateTime? REG_DATE { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_ID { get; set; }
        public string USE_YN { get; set; }
        public string USE_ADMIN_ID { get; set; }
    }
}
