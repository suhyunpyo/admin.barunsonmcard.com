using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_CardSalesSite
    {
        public int card_seq { get; set; }
        public int Company_Seq { get; set; }
        public int? CardDiscount_Seq { get; set; }
        public string IsDisplay { get; set; }
        public string IsJumun { get; set; }
        public string IsNew { get; set; }
        public string IsBest { get; set; }
        public string IsExtra { get; set; }
        public string IsJehyu { get; set; }
        public short? Ranking { get; set; }
        public short? Ranking_w { get; set; }
        public short? Ranking_m { get; set; }
        public string input_date { get; set; }
        public string IsSale { get; set; }
        public short? SampRankNo { get; set; }
        public short? PostRankNo { get; set; }
        public short? ZzimRankNo { get; set; }
        public string AppSample { get; set; }
        public string isNotCoupon { get; set; }
        public string IsExtra2 { get; set; }
        public int? isRecommend { get; set; }
        public int? isSSPre { get; set; }
        public string isSummary { get; set; }
        public string isBgcolor { get; set; }
        public string isDigitalCard { get; set; }
        public DateTime? Display_Date { get; set; }
        public string IsInProduct { get; set; }
        public string MovieURL { get; set; }
        public string DisplayLabel { get; set; }
        public string Tip { get; set; }
        public int? sealingsticker_seq { get; set; }
        public int? sealingsticker_groupseq { get; set; }
        public int? ribbon_seq { get; set; }
        public int? ribbon_groupseq { get; set; }
        public int? papercover_seq { get; set; }
        public int? papercover_groupseq { get; set; }
        public int? Flower_seq { get; set; }
        public int? Flower_GroupSeq { get; set; }
        public int? pocket_seq { get; set; }
        public int? pocket_groupseq { get; set; }
    }
}
