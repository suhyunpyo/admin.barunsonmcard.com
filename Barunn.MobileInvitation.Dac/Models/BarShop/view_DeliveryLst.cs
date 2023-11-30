using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_DeliveryLst
    {
        public string otype { get; set; }
        public int order_seq { get; set; }
        public string sales_gubun { get; set; }
        public int? company_seq { get; set; }
        public string company_name { get; set; }
        public DateTime? delivery_Date { get; set; }
        public string recv_name { get; set; }
        public string recv_address { get; set; }
        public string delivery_com { get; set; }
        public string delivery_code { get; set; }
        public string ERP_PartCode { get; set; }
        public DateTime? packing_date { get; set; }
    }
}
