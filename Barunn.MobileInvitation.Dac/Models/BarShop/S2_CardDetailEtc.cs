using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardDetailEtc
    {
        public int card_seq { get; set; }
        public string card_category { get; set; }
        public string isNew { get; set; }
        public string isBest { get; set; }
        public string isPlan { get; set; }
        public string composition { get; set; }
        public string summary { get; set; }
        public string origin { get; set; }
        public string etc1 { get; set; }
        public string etc2 { get; set; }
        public int? min_onum { get; set; }
        public string option_str1 { get; set; }
        public string option_str2 { get; set; }
        public string option_str3 { get; set; }
        public string option_str4 { get; set; }
        public string option_str5 { get; set; }
        public int? card_sale_price { get; set; }
        public string card_content { get; set; }
        public int? isDisplay { get; set; }
        public int? delivery_price { get; set; }
        public string brand_story { get; set; }
        public string delivery_policy { get; set; }
        public string refund_policy { get; set; }
        public int? free_delivery_price { get; set; }
        public string contents { get; set; }
        public string Delivery_Ty { get; set; }
        public byte? Delivery_Request_Dt { get; set; }
        public string Hover_Title { get; set; }
        public string Hover_Main_Title { get; set; }
        public string Hover_Sub_Title { get; set; }
        public string Main_Title { get; set; }
        public string Sub_Title { get; set; }
        public string Delivery_Price_Str { get; set; }
        public string Contents_Main_Copy { get; set; }
        public string Contents_Sub_Copy { get; set; }
        public string Contents_Option_Name { get; set; }
        public string Contents_Option_Summary { get; set; }
        public string QnA_Title { get; set; }
        public string QnA_Info { get; set; }
    }
}
