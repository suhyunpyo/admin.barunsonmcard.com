using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CUSTOM_SAMPLE_ORDER
    {
        public CUSTOM_SAMPLE_ORDER()
        {
            CUSTOM_SAMPLE_ORDER_ITEMs = new HashSet<CUSTOM_SAMPLE_ORDER_ITEM>();
        }

        public int sample_order_seq { get; set; }
        public string SALES_GUBUN { get; set; }
        public int COMPANY_SEQ { get; set; }
        public string MEMBER_ID { get; set; }
        public string MEMBER_NAME { get; set; }
        public string MEMBER_EMAIL { get; set; }
        public string MEMBER_FAX { get; set; }
        public string MEMBER_PHONE { get; set; }
        public string MEMBER_ZIP { get; set; }
        public string MEMBER_ADDRESS { get; set; }
        public string MEMBER_ADDRESS_DETAIL { get; set; }
        public DateTime REQUEST_DATE { get; set; }
        public DateTime? PREPARE_DATE { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
        public string DELIVERY_CODE_NUM { get; set; }
        public string DELIVERY_METHOD { get; set; }
        public string DELIVERY_COM { get; set; }
        public string MEMO { get; set; }
        public byte? DELIVERY_CHANGO { get; set; }
        public int STATUS_SEQ { get; set; }
        public string INVOICE_PRINT_YORN { get; set; }
        public string JOB_ORDER_PRINT_YORN { get; set; }
        public string DSP_PRINT_YORN { get; set; }
        public string SETTLE_MOBILID { get; set; }
        public DateTime? SETTLE_DATE { get; set; }
        public string SETTLE_HPHONE { get; set; }
        public short? CARD_PRICE { get; set; }
        public short? REDUCE_PRICE { get; set; }
        public short? DELIVERY_PRICE { get; set; }
        public int? SETTLE_PRICE { get; set; }
        public string SETTLE_CANCEL { get; set; }
        public string BUY_CONF { get; set; }
        public string ADMIN_ID { get; set; }
        public string SETTLE_METHOD { get; set; }
        public string MEMBER_HPHONE { get; set; }
        public string ISDACOM { get; set; }
        public string PG_MERTID { get; set; }
        public string PG_TID { get; set; }
        public string DACOM_TID { get; set; }
        public string PG_RESULTINFO { get; set; }
        public string PG_RESULTINFO2 { get; set; }
        public string card_installmonth { get; set; }
        public string card_nointyn { get; set; }
        public DateTime? CANCEL_DATE { get; set; }
        public string CANCEL_REASON { get; set; }
        public double? PG_FEE { get; set; }
        public double? PG_REFEE { get; set; }
        public string PG_PAYDATE { get; set; }
        public string PG_CALDATE { get; set; }
        public string PG_REPAYDATE { get; set; }
        public string PG_RECALDATE { get; set; }
        public string SRC_ERP_DATE { get; set; }
        public string isAscrow { get; set; }
        public string isHJ { get; set; }
        public string isVar { get; set; }
        public string call_admin_id { get; set; }
        public string etc_info_s { get; set; }
        public string join_division { get; set; }
        public int? order_g_seq { get; set; }
        public string isOneClickSample { get; set; }
        public int? MULTI_PACK_SEQ { get; set; }
        public int? MULTI_PACK_SUB_SEQ { get; set; }
        public DateTime? MULTI_PACK_REG_DATE { get; set; }
        public string WEDD_DATE { get; set; }
        public string OPT_GUBUN { get; set; }
        public string OPT_VALUES { get; set; }
        public string WisaFlag { get; set; }

        public virtual ICollection<CUSTOM_SAMPLE_ORDER_ITEM> CUSTOM_SAMPLE_ORDER_ITEMs { get; set; }
    }
}
