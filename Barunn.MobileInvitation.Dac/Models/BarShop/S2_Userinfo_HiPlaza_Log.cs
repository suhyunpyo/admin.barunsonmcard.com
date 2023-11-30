using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_Userinfo_HiPlaza_Log
    {
        public decimal id { get; set; }
        public string uid { get; set; }
        public string P_ORG_CD { get; set; }
        public string P_CUST_NM { get; set; }
        public string P_SSN_CI { get; set; }
        public string P_MOBILE_DDD { get; set; }
        public string P_MOBILE_NO1 { get; set; }
        public string P_MOBILE_NO2 { get; set; }
        public string P_TEL_DDD { get; set; }
        public string P_TEL_NO1 { get; set; }
        public string P_TEL_NO2 { get; set; }
        public string P_PI_MOTION_YN { get; set; }
        public string P_SMS_RCV_YN { get; set; }
        public string P_DM_RCV_YN { get; set; }
        public string P_TM_RCV_YN { get; set; }
        public string P_EMAIL_RCV_YN { get; set; }
        public string P_EMAIL_ID { get; set; }
        public string P_SEX { get; set; }
        public string P_BIRTHDAY_DATE { get; set; }
        public string P_SOLAR_CALENDAR_YN { get; set; }
        public string P_POST_CD { get; set; }
        public string P_ADDR1 { get; set; }
        public string P_ADDR2 { get; set; }
        public string P_WEDDING_DATE { get; set; }
        public string RTN_CD { get; set; }
        public string RTN_MSG { get; set; }
        public string MLG_CUST_ID { get; set; }
        public string MBS_CARD_NO { get; set; }
        public string JSON_STR { get; set; }
        public string ENCODE_STR { get; set; }
        public string DECODE_STR { get; set; }
        public string RESULT_JSON_STR { get; set; }
        public DateTime reg_date { get; set; }
        public DateTime? result_date { get; set; }
        public DateTime? cancel_date { get; set; }
    }
}
