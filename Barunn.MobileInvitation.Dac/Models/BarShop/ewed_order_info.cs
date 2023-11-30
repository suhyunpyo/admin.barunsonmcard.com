using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ewed_order_info
    {
        public int order_seq { get; set; }
        public string uid { get; set; }
        public byte? order_status { get; set; }
        public byte? job_status { get; set; }
        public string isNew { get; set; }
        public int? card_seq { get; set; }
        public int? settle_price { get; set; }
        public string order_div { get; set; }
        public string order_tp { get; set; }
        public string wd_img { get; set; }
        public string wd_file { get; set; }
        public string output_img { get; set; }
        public string output_swf { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public string addr_detail { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public DateTime? order_date { get; set; }
        public string end_date { get; set; }
        public int? shop_order_seq { get; set; }
        public string partner_div { get; set; }
        public int? order_price { get; set; }
        public int? last_total_price { get; set; }
        public string settle_type { get; set; }
        public DateTime? cancel_date { get; set; }
        public DateTime? settle_date { get; set; }
        public DateTime? settle_cancel_date { get; set; }
        public string settle_method { get; set; }
        public string couponseq { get; set; }
        public int? coupon_price { get; set; }
        public short? etc_price { get; set; }
        public string etc_price_ment { get; set; }
        public string pg_resultinfo { get; set; }
        public string pg_tid { get; set; }
        public string pg_name { get; set; }
        public string pg_shopid { get; set; }
        public DateTime? job_start_date { get; set; }
        public DateTime? job_end_date { get; set; }
        public DateTime? job_confirm_date { get; set; }
        public byte? result_code { get; set; }
        public byte? payment_way { get; set; }
        public int? company_seq { get; set; }
        public string auth_time { get; set; }
        public string order_no { get; set; }
        public string pg_cancel_date { get; set; }
        public string cancel_reason { get; set; }
        public string admin_id { get; set; }
        public string isAuto { get; set; }
        public string Daum_uid { get; set; }
        public int? shop_card_seq { get; set; }
        public string shop_order_div { get; set; }
        public string minicd_adm_msg { get; set; }
        public string bj_use { get; set; }
        public string is_bujoo { get; set; }
        public string bj_agency { get; set; }
        public string bj_jumin { get; set; }
        public string bj_hp { get; set; }
        public string map_path { get; set; }
        public byte? ticket3 { get; set; }
        public byte? ticket5 { get; set; }
        public byte? ticket10 { get; set; }
        public DateTime? src_erp_date { get; set; }
    }
}
