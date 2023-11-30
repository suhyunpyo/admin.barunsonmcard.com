using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class DELIVERY_INFO
    {
        public int ID { get; set; }
        public int ORDER_SEQ { get; set; }
        public int DELIVERY_SEQ { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string HPHONE { get; set; }
        public string ZIP { get; set; }
        public string ADDR { get; set; }
        public string ADDR_DETAIL { get; set; }
        public DateTime? PACKING_DATE { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
        public string DELIVERY_CODE_NUM { get; set; }
        public string DELIVERY_COM { get; set; }
        public string PACKING_ADMIN_ID { get; set; }
        public int? DELIVERY_PRICE { get; set; }
        public int? DELIVERY_METHOD { get; set; }
        public string DELIVERY_PAY { get; set; }
        public string DELIVERY_INFO1 { get; set; }
        public string receivecode { get; set; }
        public string receiveShopname { get; set; }
        public string DELIVERY_MEMO { get; set; }
        public DateTime? savepack_date { get; set; }
        public string savepack_admin_id { get; set; }
        public string isNewCopy { get; set; }
        public string nt_code { get; set; }
        public string Delivery_Type { get; set; }
        public string QuickGubun { get; set; }
    }
}
