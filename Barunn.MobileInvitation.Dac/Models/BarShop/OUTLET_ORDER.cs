using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class OUTLET_ORDER
    {
        public int ORDER_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string ORDER_TYPE { get; set; }
        public int? ORDER_COUNT { get; set; }
        public string ORDER_UID { get; set; }
        public string BUYER_NAME { get; set; }
        public string BUYER_EMAIL { get; set; }
        public string BUYER_PHONE { get; set; }
        public string BUYER_ZIP { get; set; }
        public string BUYER_ADDRESS { get; set; }
        public string BUYER_HPHONE { get; set; }
        public string RECV_NAME { get; set; }
        public string RECV_PHONE { get; set; }
        public string RECV_HPHONE { get; set; }
        public string RECV_ZIP { get; set; }
        public string RECV_ADDRESS { get; set; }
        public string BUYER_COMMENT { get; set; }
        public DateTime? ORDER_DATE { get; set; }
        public int STATUS_SEQ { get; set; }
        public byte? PROCLEVEL { get; set; }
        public string PG_AUTH_CODE { get; set; }
        public string CARD_AUTH_CODE { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string PG_RESULT_MSG { get; set; }
        public string PG_RESULT_CODE { get; set; }
        public int? SETTLE_PRICE { get; set; }
        public string SETTLE_METHOD { get; set; }
        public DateTime? SETTLE_DATE { get; set; }
        public string PAY_DATE { get; set; }
        public string TID { get; set; }
        public int? BANK_SEQ { get; set; }
        public string BANK_RECEIPTER { get; set; }
        public string RECV_EMAIL { get; set; }
        public int? ORDER_TOTAL_PRICE { get; set; }
        public int? REDUCE_PRICE { get; set; }
        public int? BUYER_DELIVERY_PRICE { get; set; }
        public int? ORDER_PRICE { get; set; }
        public string DELIVERY_MEMO { get; set; }
        public string DELIVERY_CODE_NUM { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
        public string DELIVERY_METHOD { get; set; }
        public string ADMIN_ID { get; set; }
        public double? ORDER_DISCOUNT_RATE { get; set; }
        public double? ORDER_MDISCOUNT_RATE { get; set; }
        public int? FEE_PRICE { get; set; }
        public int? POST_PRICE { get; set; }
        public int? PG_SETTLE_PRICE { get; set; }
        public string SETTLE_MOBILID { get; set; }
        public string COUPONSEQ { get; set; }
        public string BANK_VACCOUNT_NUM { get; set; }
        public DateTime? DELETE_DATE { get; set; }
        public string DELETE_ADMIN_ID { get; set; }
        public DateTime? SRC_CONFIRM_DATE { get; set; }
        public DateTime? PREPARE_DATE { get; set; }
        public DateTime? SRC_ERP_DATE { get; set; }
        public DateTime? CANCEL_DATE { get; set; }
        public string CANCEL_REASON { get; set; }
        public string ADMIN_MEMO { get; set; }
        public string ISDACOM { get; set; }
        public string pg_MertId { get; set; }
        public DateTime? SRC_COMPOSE_DATE { get; set; }
        public string SRC_COMPOSE_ADMIN_ID { get; set; }
        public DateTime? SRC_PRINTW_DATE { get; set; }
        public DateTime? SRC_PRINT_DATE { get; set; }
        public string pg_paydate { get; set; }
        public string pg_caldate { get; set; }
        public string pg_repaydate { get; set; }
        public string pg_recaldate { get; set; }
        public double? pg_fee { get; set; }
        public double? pg_refee { get; set; }
        public int? option_price { get; set; }
    }
}
