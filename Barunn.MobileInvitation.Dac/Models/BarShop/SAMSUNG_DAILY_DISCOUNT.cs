using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class SAMSUNG_DAILY_DISCOUNT
    {
        public int seq { get; set; }
        public string conninfo { get; set; }
        public string chk_smembership { get; set; }
        public DateTime? smembership_reg_date { get; set; }
        public DateTime? smembership_leave_date { get; set; }
        public string chk_smembership_leave { get; set; }
        public int? order_seq { get; set; }
        public int? up_order_seq { get; set; }
        public string order_type { get; set; }
        public string sales_Gubun { get; set; }
        public string site_gubun { get; set; }
        public int? company_seq { get; set; }
        public int? status_seq { get; set; }
        public DateTime? order_date { get; set; }
        public byte? settle_status { get; set; }
        public DateTime? settle_date { get; set; }
        public int? settle_price { get; set; }
        public string pg_resultinfo { get; set; }
        public string pg_shopid { get; set; }
        public string pg_tid { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string discount_in_advance { get; set; }
        public DateTime? discount_in_advance_reg_date { get; set; }
        public DateTime? discount_in_advance_cancel_date { get; set; }
        public DateTime? reg_date_s { get; set; }
        public string dacom_tid { get; set; }
    }
}
