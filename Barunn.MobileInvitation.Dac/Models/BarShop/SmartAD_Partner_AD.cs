using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SmartAD_Partner_AD
    {
        public int AD_SEQ { get; set; }
        public int PARTNER_SEQ { get; set; }
        public string AD_TYPE { get; set; }
        public string CONTENT1 { get; set; }
        public string CONTENT2 { get; set; }
        public string CONTENT3 { get; set; }
        public string PROMOTION_TYPE { get; set; }
        public string COUPON_CODE { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string DIRECTION_MSG { get; set; }
        public string CAUTION_MSG { get; set; }
        public DateTime? REG_DATE { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_ID { get; set; }
        public string DISPLAY_YN { get; set; }
        public int? PRIORITY { get; set; }
        public int VIRTUAL_CNT { get; set; }
    }
}
