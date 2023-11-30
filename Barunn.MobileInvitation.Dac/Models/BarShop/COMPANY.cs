using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class COMPANY
    {
        public COMPANY()
        {
            ADMIN_LSTs = new HashSet<ADMIN_LST>();
        }

        public int COMPANY_SEQ { get; set; }
        public string SALES_GUBUN { get; set; }
        public string JAEHU_KIND { get; set; }
        public string JUMUN_TYPE { get; set; }
        public int COMPANY_UPPER_SEQ { get; set; }
        public string COMPANY_NAME { get; set; }
        public string COMPANY_NUM { get; set; }
        public DateTime? REGIST_DATE { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string STATUS { get; set; }
        public string IMG_DIR { get; set; }
        public string LOGIN_ID { get; set; }
        public string PASSWD { get; set; }
        public string BOSS_NM { get; set; }
        public string BOSS_TEL_NO { get; set; }
        public string UP_TAE { get; set; }
        public string FAX_NO { get; set; }
        public string KIND { get; set; }
        public string E_MAIL { get; set; }
        public string ZIP_CODE { get; set; }
        public string FRONT_ADDR { get; set; }
        public string BACK_ADDR { get; set; }
        public string MNG_NM { get; set; }
        public string MNG_E_MAIL { get; set; }
        public string MNG_TEL_NO { get; set; }
        public string MNG_HP_NO { get; set; }
        public string MNG_ZIP_CODE { get; set; }
        public string MNG_ADDRESS { get; set; }
        public string MNG_ADDR_DETAIL { get; set; }
        public string ACC_NM { get; set; }
        public string ACC_E_MAIL { get; set; }
        public string ACC_TEL_NO { get; set; }
        public string ACC_HP_NO { get; set; }
        public string CORP_EXP { get; set; }
        public string BANK_NM { get; set; }
        public string ACCOUNT_NO { get; set; }
        public short? SUPPLY_DISRATE { get; set; }
        public string REG_ID { get; set; }
        public string CHG_ID { get; set; }
        public DateTime CHG_DT { get; set; }
        public string ONOFF { get; set; }
        public string INFO_TMP { get; set; }
        public string INFO_TMP2 { get; set; }
        public string INFO_TMP3 { get; set; }
        public string INFO_TMP4 { get; set; }
        public string mypage_url { get; set; }
        public short? ewed_val { get; set; }
        public string ERP_CODE { get; set; }
        public string UP_TAE_STR { get; set; }
        public byte PRICE_GUBUN { get; set; }
        public string SASIK_GUBUN { get; set; }
        public string ERP_Dept { get; set; }
        public string ERP_TaxType { get; set; }
        public string ERP_PartCode { get; set; }
        public string ERP_StaffCode { get; set; }
        public string ERP_CostCode { get; set; }
        public string ERP_PriceLevel { get; set; }
        public string ERP_PGcheck { get; set; }
        public string ERP_PayLater { get; set; }
        public string COMPANY_UPPER_YN { get; set; }
        public string jehu_grade { get; set; }
        public string feeType { get; set; }
        public double? feeRate { get; set; }
        public string FIRST_ALARM { get; set; }
        public string area { get; set; }
        public string bank { get; set; }
        public string bank_account_name { get; set; }
        public string bank_account_no { get; set; }

        public virtual ICollection<ADMIN_LST> ADMIN_LSTs { get; set; }
    }
}
