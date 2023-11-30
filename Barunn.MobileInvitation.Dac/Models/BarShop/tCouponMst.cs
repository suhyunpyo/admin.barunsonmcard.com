using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tCouponMst
    {
        public string CouponCD { get; set; }
        public string Cd { get; set; }
        public string Subject { get; set; }
        public int? Amt { get; set; }
        public string amtGb { get; set; }
        public DateTime? FromDT { get; set; }
        public DateTime? ToDT { get; set; }
        public int? ValidAmt { get; set; }
        public string CouponURL { get; set; }
        public string AddFile { get; set; }
        public int? TakeCNT { get; set; }
        public DateTime? InsertDT { get; set; }
        public string UseYN { get; set; }
        public string UseStoreCD { get; set; }
        public int? LimitCnt { get; set; }
        public string EmpYN { get; set; }
        public string MultiUseYN { get; set; }
        public string MobileOnlyYN { get; set; }
        public string LimitAmtChkYN { get; set; }
        public int? LimitAmt { get; set; }
        public string UseTarget { get; set; }
        public int? CouponCnt { get; set; }
        public string partnerYN { get; set; }
        public string DeliveryYN { get; set; }
        public string IssueYN { get; set; }
        public byte IssueDayCnt { get; set; }
        public string TermType { get; set; }
        public string TermMarginUseYN { get; set; }
        public byte TermMargin { get; set; }
        public string TermItemUseYN { get; set; }
        public string TermVndUseYN { get; set; }
        public string TermEventUseYN { get; set; }
        public string TermCategoryUseYN { get; set; }
        public string TermBrandUseYN { get; set; }
        public string ApplyType { get; set; }
        public string DeliveryOverSaleYN { get; set; }
        public string LimitSalePriceUseYN { get; set; }
        public int LimitSalePrice { get; set; }
        public string LimitSaleItemYN { get; set; }
        public string LimitSaleItemCD { get; set; }
        public int LimitSaleItemCnt { get; set; }
        public string SalePriceViewYN { get; set; }
        public string CoverageComment { get; set; }
    }
}
