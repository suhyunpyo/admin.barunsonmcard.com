using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class VW_DELIVERY_MST
    {
        public int ORDER_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public int? COMPANY_SEQ { get; set; }
        public string COMPANY_NAME { get; set; }
        public string ORDER_TYPE { get; set; }
        public string ORDER_TABLE_NAME { get; set; }
        public string ISHJ { get; set; }
        public int? STATUS_SEQ { get; set; }
        public string ERP_PARTCODE { get; set; }
        public string DELIVERY_CODE { get; set; }
        public string RECV_ZIP { get; set; }
        public string RECV_ADDR { get; set; }
        public string RECV_ADDR_DETAIL { get; set; }
        public string RECV_NAME { get; set; }
        public string RECV_PHONE { get; set; }
        public string RECV_HPHONE { get; set; }
        public string RECV_MSG { get; set; }
        public DateTime? SEND_DATE { get; set; }
        public string DELIVERY_MSG { get; set; }
        public int DELIVERY_SEQ { get; set; }
    }
}
