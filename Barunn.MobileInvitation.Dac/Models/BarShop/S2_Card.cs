using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_Card
    {
        public int Card_Seq { get; set; }
        public string CardBrand { get; set; }
        public string Card_Code { get; set; }
        public string Card_ERPCode { get; set; }
        public string Card_Div { get; set; }
        public string Card_Name { get; set; }
        public string Card_Image { get; set; }
        public int? CardSet_Price { get; set; }
        public int Card_Price { get; set; }
        public DateTime? ERP_EXPECTED_ARRIVAL_DATE { get; set; }
        public string ERP_EXPECTED_ARRIVAL_DATE_USE_YORN { get; set; }
        public int? ERP_MIN_STOCK_QTY { get; set; }
        public string ERP_MIN_STOCK_QTY_USE_YORN { get; set; }
        public DateTime? RegDate { get; set; }
        public int? Card_WSize { get; set; }
        public int? Card_HSize { get; set; }
        public string Old_Code { get; set; }
        public string t_env_code { get; set; }
        public string t_inpaper_code { get; set; }
        public string admin_id { get; set; }
        public string new_code { get; set; }
        public string CARD_GROUP { get; set; }
        public int? CardFactory_Price { get; set; }
        public DateTime? REGIST_DATES { get; set; }
        public string DISPLAY_YORN { get; set; }
        public DateTime? DISPLAY_UPDATE_DATE { get; set; }
        public string DISPLAY_UPDATE_UID { get; set; }
        public string FPRINT_YORN { get; set; }
        public string WisaFlag { get; set; }
        public decimal? View_Discount_Percent { get; set; }
        public int? Cost_Price { get; set; }
        public string Video_URL { get; set; }
    }
}
