using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class PHOTOBOOK_ORDER
    {
        public int id { get; set; }
        public string site_code { get; set; }
        public string sales_gubun { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string order_phone { get; set; }
        public string order_hphone { get; set; }
        public string recv_name { get; set; }
        public string recv_phone { get; set; }
        public string recv_hphone { get; set; }
        public string recv_zip { get; set; }
        public string recv_addr { get; set; }
        public string recv_addr_detail { get; set; }
        public string recv_msg { get; set; }
        public byte? status_seq { get; set; }
        public DateTime? order_date { get; set; }
        public string order_time { get; set; }
        public int? order_price { get; set; }
        public int? point_price { get; set; }
        public string discount_type { get; set; }
        public short? discount_rate { get; set; }
        public int? reduce_price { get; set; }
        public string coupon_code { get; set; }
        public int? coupon_price { get; set; }
        public int? delivery_price { get; set; }
        public string delivery_code { get; set; }
        public int? total_price { get; set; }
        public int? settle_price { get; set; }
        public string pay_type { get; set; }
        public string employer_name { get; set; }
        public DateTime? settle_date { get; set; }
        public DateTime? cancel_date { get; set; }
        public string cancel_admin_id { get; set; }
        public int? worder_id { get; set; }
        public string worder_gubun { get; set; }
        public string settle_method { get; set; }
        public string pg_resultinfo { get; set; }
        public string pg_resultinfo2 { get; set; }
        public string pg_shopid { get; set; }
        public int? pg_fee { get; set; }
        public string pg_tid { get; set; }
        public string dacom_tid { get; set; }
        public string card_installmonth { get; set; }
        public string card_nointyn { get; set; }
        public string isReceipt { get; set; }
        public string isSystemUpdate { get; set; }
        public string isBalju { get; set; }
        public string isOpen { get; set; }
        public DateTime? balju_date { get; set; }
        public DateTime? merge_date { get; set; }
        public DateTime? entering_date { get; set; }
        public DateTime? packing_date { get; set; }
        public DateTime? delivery_date { get; set; }
        public string admin_memo { get; set; }
        public DateTime? request_date { get; set; }
        public DateTime? src_erp_date { get; set; }
        public string ip { get; set; }
        public string delivery_com { get; set; }
    }
}
