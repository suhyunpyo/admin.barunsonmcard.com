using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardDetail
    {
        public int Card_Seq { get; set; }
        public int? Env_Seq { get; set; }
        public int? Env_GroupSeq { get; set; }
        public int? Inpaper_Seq { get; set; }
        public int? Inpaper_GroupSeq { get; set; }
        public int? Acc1_Seq { get; set; }
        public int? Acc1_GroupSeq { get; set; }
        public int? Acc2_Seq { get; set; }
        public int? Acc2_GroupSeq { get; set; }
        public int? MapCard_Seq { get; set; }
        public int? MapCard_GroupSeq { get; set; }
        public int? GreetingCard_Seq { get; set; }
        public int? GreetingCard_GroupSeq { get; set; }
        public int? Lining_Seq { get; set; }
        public int? Lining_GroupSeq { get; set; }
        public string Card_Text { get; set; }
        public string Card_Content { get; set; }
        public string Card_Keyword { get; set; }
        public string Card_Shape { get; set; }
        public string Card_Folding { get; set; }
        public string Card_PrintMethod { get; set; }
        public string Card_Material { get; set; }
        public string Card_PrintOffice { get; set; }
        public short? Minimum_Count { get; set; }
        public short? AddMinimum_count { get; set; }
        public string Unit_Count { get; set; }
        public string env_code { get; set; }
        public string inpaper_code { get; set; }
        public int? ColorInpaper_seq { get; set; }
        public string Acc_Type { get; set; }
        public string Card_Text_Premier { get; set; }
        public int? seal_seq { get; set; }
        public int Sticker_seq { get; set; }
        public int Sticker_GroupSeq { get; set; }
        public string CuttingLineType { get; set; }
        public string EnvCharge { get; set; }
        public int? Flower_seq { get; set; }
        public int? Flower_GroupSeq { get; set; }
        public int? SealingSticker_seq { get; set; }
        public int? SealingSticker_GroupSeq { get; set; }
        public string EnvPrintMethod1 { get; set; }
        public string EnvPrintMethod2 { get; set; }
    }
}
