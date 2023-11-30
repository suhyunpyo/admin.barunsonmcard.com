using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class view_SampleList
    {
        public int COMPANY_SEQ { get; set; }
        public int sample_order_seq { get; set; }
        public string MEMBER_ID { get; set; }
        public string MEMBER_NAME { get; set; }
        public string MEMBER_EMAIL { get; set; }
        public string MEMBER_FAX { get; set; }
        public string MEMBER_PHONE { get; set; }
        public string MEMBER_ZIP { get; set; }
        public string MEMBER_ADDRESS { get; set; }
        public string member_address_detail { get; set; }
        public string MEMO { get; set; }
        public string MEMBER_HPHONE { get; set; }
        public string DELIVERY_CODE_NUM { get; set; }
        public string card_code { get; set; }
        public string old_code { get; set; }
    }
}
