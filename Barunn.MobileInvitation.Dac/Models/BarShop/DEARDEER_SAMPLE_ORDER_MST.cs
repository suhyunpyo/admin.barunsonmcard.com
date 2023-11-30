using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DEARDEER_SAMPLE_ORDER_MST
    {
        public int DEARDEER_SAMPLE_ORDER_MST_SEQ { get; set; }
        public string SAMPLE_ORDER_NO { get; set; }
        public int STATUS_SEQ { get; set; }
        public string USER_ID { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_NAME { get; set; }
        public string HOME_PHONE_NUMBER { get; set; }
        public string MOBILE_PHONE_NUMBER { get; set; }
        public string ZIP_CODE { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS_DETAIL { get; set; }
        public string DELIVERY_COMPANY_CODE { get; set; }
        public string TRACKING_NUMBER { get; set; }
        public string INVOICE_PRINT_YORN { get; set; }
        public string JOB_ORDER_PRINT_YORN { get; set; }
        public string DSP_PRINT_YORN { get; set; }
        public string CLSFADDR { get; set; }
        public string CLLDLVBRANNM { get; set; }
        public string CLLDLCBRANSHORTNM { get; set; }
        public string CLLDLVEMPNM { get; set; }
        public string CLLDLVEMPNICKNM { get; set; }
        public string CLSFCD { get; set; }
        public string CLSFNM { get; set; }
        public string SUBCLSFCD { get; set; }
        public DateTime? PREPARE_DATE { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
        public DateTime REG_DATE { get; set; }
    }
}
