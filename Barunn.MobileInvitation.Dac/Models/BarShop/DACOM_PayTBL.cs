using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DACOM_PayTBL
    {
        public long id { get; set; }
        public string pg_shopid { get; set; }
        public int order_seq { get; set; }
        public string cal_date { get; set; }
        public string settle_date { get; set; }
        public byte? settle_method { get; set; }
        public string pay_date { get; set; }
        public string status { get; set; }
        public string pay_info { get; set; }
        public int? settle_price { get; set; }
    }
}
