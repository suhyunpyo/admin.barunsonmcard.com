using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Custom_order_Group
    {
        public int order_g_seq { get; set; }
        public string site_gubun { get; set; }
        public string pay_Type { get; set; }
        public int? company_seq { get; set; }
        public int? status_seq { get; set; }
        public int? settle_status { get; set; }
        public DateTime order_date { get; set; }
        public string src_cancel_admin_id { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string order_phone { get; set; }
        public string order_hphone { get; set; }
        public string order_faxphone { get; set; }
        public string order_etc_comment { get; set; }
        public string isReceipt { get; set; }
        public string etc_price_ment { get; set; }
        public int? order_price { get; set; }
        public int? order_total_price { get; set; }
        public int? delivery_price { get; set; }
        public int? etc_price { get; set; }
        public DateTime? settle_date { get; set; }
        public DateTime? settle_cancel_date { get; set; }
        public string settle_method { get; set; }
        public int? settle_price { get; set; }
        public string pg_shopid { get; set; }
        public string pg_tid { get; set; }
        public string pg_receipt_tid { get; set; }
        public string pg_resultinfo { get; set; }
        public string pg_resultinfo2 { get; set; }
        public int? pg_status { get; set; }
        public double? pg_fee { get; set; }
        public DateTime? pg_paydate { get; set; }
        public DateTime? pg_repaydate { get; set; }
        public string pg_caldate { get; set; }
        public string pg_recaldate { get; set; }
        public int? point_price { get; set; }
        public string delivery_name { get; set; }
        public string temp_key { get; set; }
        public string dacom_tid { get; set; }
        public string isAscrow { get; set; }
        public DateTime? src_ap_date { get; set; }
    }
}
