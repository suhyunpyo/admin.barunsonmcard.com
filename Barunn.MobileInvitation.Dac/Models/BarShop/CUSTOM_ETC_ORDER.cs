using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_ETC_ORDER
    {
        public int order_seq { get; set; }
        public string order_type { get; set; }
        public string sales_gubun { get; set; }
        public string site_gubun { get; set; }
        public short company_Seq { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string order_phone { get; set; }
        public string order_hphone { get; set; }
        public string recv_name { get; set; }
        public string recv_phone { get; set; }
        public string recv_hphone { get; set; }
        public string recv_address { get; set; }
        public string recv_address_detail { get; set; }
        public string recv_zip { get; set; }
        public string recv_msg { get; set; }
        public byte? status_seq { get; set; }
        public DateTime? order_date { get; set; }
        public DateTime? settle_date { get; set; }
        public int? delivery_price { get; set; }
        public int? option_price { get; set; }
        public string couponseq { get; set; }
        public int? coupon_price { get; set; }
        public int? settle_price { get; set; }
        public DateTime? settle_Cancel_Date { get; set; }
        public string settle_method { get; set; }
        public string pg_shopid { get; set; }
        public string pg_tid { get; set; }
        public string dacom_tid { get; set; }
        public string pg_receipt_tid { get; set; }
        public string pg_resultinfo { get; set; }
        public string pg_resultinfo2 { get; set; }
        public string card_installmonth { get; set; }
        public string card_nointyn { get; set; }
        public string isReceipt { get; set; }
        public DateTime? compose_date { get; set; }
        public DateTime? mod_request_date { get; set; }
        public DateTime? mod_compose_date { get; set; }
        public string compose_admin_id { get; set; }
        public DateTime? confirm_date { get; set; }
        public DateTime? prepare_date { get; set; }
        public DateTime? print_date { get; set; }
        public DateTime? delivery_date { get; set; }
        public string delivery_code { get; set; }
        public string delivery_method { get; set; }
        public string delivery_com { get; set; }
        public double? pg_Fee { get; set; }
        public double? pg_refee { get; set; }
        public string pg_paydate { get; set; }
        public string pg_caldate { get; set; }
        public string pg_repaydate { get; set; }
        public string pg_recaldate { get; set; }
        public string delivery_admin_id { get; set; }
        public string admin_id { get; set; }
        public int? etc_info_l { get; set; }
        public string etc_info_s { get; set; }
        public string result_info { get; set; }
        public string src_erp_date { get; set; }
        public string admin_memo { get; set; }
        public string isAscrow { get; set; }
        public string isHJ { get; set; }
        public string coupon_no { get; set; }
        public int? order_g_seq { get; set; }
        public string SampleBook_ID { get; set; }
        public DateTime? Return_Limit_Date { get; set; }
        public DateTime? Return_Request_Date { get; set; }
        public DateTime? Return_Proceeding_Date { get; set; }
        public DateTime? Return_Complete_Date { get; set; }
        public DateTime? Stock_Date { get; set; }
        public string WisaFlag { get; set; }
    }
}
