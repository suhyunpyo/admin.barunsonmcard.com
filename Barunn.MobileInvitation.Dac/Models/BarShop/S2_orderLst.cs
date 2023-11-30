using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_orderLst
    {
        public int order_seq { get; set; }
        public string order_type { get; set; }
        public string isChoanRisk { get; set; }
        public string isSpecial { get; set; }
        public string sales_Gubun { get; set; }
        public string site_gubun { get; set; }
        public string pay_Type { get; set; }
        public int? company_seq { get; set; }
        public string COMPANY_NAME { get; set; }
        public string erp_partcode { get; set; }
        public int? up_order_seq { get; set; }
        public string order_add_flag { get; set; }
        public string order_add_type { get; set; }
        public int status_seq { get; set; }
        public string member_id { get; set; }
        public string order_hphone { get; set; }
        public string order_email { get; set; }
        public string src_compose_admin_id { get; set; }
        public string src_compose_mod_admin_id { get; set; }
        public DateTime? order_date { get; set; }
        public DateTime? src_compose_date { get; set; }
        public DateTime? src_confirm_date { get; set; }
        public DateTime? src_modRequest_date { get; set; }
        public DateTime? src_compose_mod_date { get; set; }
        public DateTime? src_printW_date { get; set; }
        public DateTime? src_print_date { get; set; }
        public DateTime? src_printCopy_date { get; set; }
        public DateTime? src_CloseCopy_date { get; set; }
        public DateTime? src_jebon_date { get; set; }
        public DateTime? src_packing_date { get; set; }
        public DateTime? src_send_date { get; set; }
        public string order_name { get; set; }
        public byte? settle_status { get; set; }
        public string isinpaper { get; set; }
        public string ishandmade { get; set; }
        public string isEnvInsert { get; set; }
        public int? fticket_price { get; set; }
        public int? embo_price { get; set; }
        public string isEmbo { get; set; }
        public int? envInsert_price { get; set; }
        public int? mini_price { get; set; }
        public string isColorInpaper { get; set; }
        public string isCompose { get; set; }
        public string Expr1 { get; set; }
        public byte? ProcLevel { get; set; }
        public string couponseq { get; set; }
        public string card_code { get; set; }
        public string old_code { get; set; }
        public string card_code_str { get; set; }
        public int? order_count { get; set; }
        public int? order_price { get; set; }
        public int settle_price { get; set; }
        public int? unicef_price { get; set; }
        public int? last_total_price { get; set; }
        public string etc_name { get; set; }
        public string wedd_name { get; set; }
        public DateTime? Expr2 { get; set; }
        public short? brand { get; set; }
        public string map_trans_method { get; set; }
        public string isCorel { get; set; }
        public string isBaesongRisk { get; set; }
        public string isRibon { get; set; }
        public string isColorPrint { get; set; }
        public string trouble_type { get; set; }
        public string isVar { get; set; }
        public string settle_method { get; set; }
        public string ftype { get; set; }
        public string fetype { get; set; }
        public string AUTO_CHOAN_STATUS_CODE { get; set; }
        public int? sasik_price { get; set; }
        public string isCCG { get; set; }
    }
}
