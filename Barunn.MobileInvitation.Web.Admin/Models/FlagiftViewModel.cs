using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 화환 선물 매출 현황
    /// </summary>
    public class FlagiftSaleStatistic
    {
        /// <summary>
        /// 날짜 (일, 월)
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 설정 수
        /// </summary>
        public int SetCount { get; set; }
        /// <summary>
        /// 설정 취소 수
        /// </summary>
        public int SetCancelCount { get; set; }
        
        /// <summary>
        /// 선물 건수
        /// </summary>
        public int GiftCount { get; set; }
        /// <summary>
        /// 취소 환불 수
        /// </summary>
        public int GiftCancelCount { get; set; }
        /// <summary>
        /// 매출액
        /// </summary>
        public int GiftAmount { get; set; }

    }

    /// <summary>
    /// 화환 선물 검색 모델
    /// </summary>
    public class FlaOrderSearchViewModel<T> : PageViewModel
    {
        /// <summary>
        /// 검색어
        /// </summary>
        public string Keyword { get; set; } = "";
        /// <summary>
        /// 검색 날짜 조건 - Select box
        /// </summary>
        public string Period { get; set; } = "Order";
        public IEnumerable<SelectListItem> Periods { get; set; } = new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "주문일", Value = "Order" },
                        new SelectListItem { Text = "예식일", Value = "Wedding" }
                    }, "Value", "Text");
        
        public int TermType { get; set; } = 1;
        /// <summary>
        /// 시작일
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        /// <summary>
        /// 종료일
        /// </summary>
        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<T> DataModel { set; get; }

        public string ReturnUrl { get; set; }
        public Uri BarunsonmCardUrl { get; set; }

        public override int PageSize { get; set; } = 50;

        public IEnumerable<SelectListItem> PageSizes
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "50개씩 보기", Value = "50" },
                        new SelectListItem { Text = "100개씩 보기", Value = "100" },
                        new SelectListItem { Text = "150개씩 보기", Value = "150" },
                        new SelectListItem { Text = "200개씩 보기", Value = "200" }
                    }, "Value", "Text");
            }
        }
        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(Keyword), Keyword },
                    { nameof(Period), Period },
                    { nameof(StartDate), StartDate.ToString("yyyy-MM-dd") },
                    { nameof(EndDate), EndDate.ToString("yyyy-MM-dd") },
                    { nameof(PageSize), PageSize.ToString() },
                };
                return routeall;
            }
        }
    }

    /// <summary>
    /// 화환 선물 주문별 데이터 모델
    /// </summary>
    public class FlaOrderSearchDataModel
    {
        /// <summary>
        /// 주문 ID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 주문번호
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 회원 구분: U - 회원 / G - 비회원
        /// </summary>
        public string MemberType { get; set; }

        /// <summary>
        /// ID, 비회원은 Email 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// EMail
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 선물 건수
        /// </summary>
        public int FlaGiftCount { get; set; }

        /// <summary>
        /// 매출액
        /// </summary>
        public int FlaGiftAmount  { get; set; }

        /// <summary>
        /// 주문일
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 예식일
        /// </summary>
        public DateTime? WeddingDate { get; set; }
        /// <summary>
        /// 제품 정보 링크
        /// </summary>
        public string Invitation_Url { get; set; }
    }

    /// <summary>
    /// 화환 선물 주문별 상세 검색 모델
    /// </summary>
    public class FlaOrderItemsViewModel : PageViewModel
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }

        public List<FlaOrderItemDataModel> DataModel { set; get; }

        public override int PageSize { get; set; } = 10;
        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(OrderId), OrderId.ToString() },
                    { nameof(PageSize), PageSize.ToString() },
                };
                return routeall;
            }
        }
    }

    /// <summary>
    /// 화환 선물 주문별 상세 데이터 모델
    /// </summary>
    public class FlaOrderItemDataModel
    {
        public string OrderCode { get; set; }
        public string OrderTitle { get; set; }
        public string OrderName { get; set; }
        public string ProductName { get; set; }
        public int SalePrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderStateName { get; set; }
    }

    /// <summary>
    /// 화환 선물 건별 데이터모델
    /// </summary>
    public class FlaOrderDataModel
    {
        /// <summary>
        /// 주문 ID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 주문번호
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 회원 구분: U - 회원 / G - 비회원
        /// </summary>
        public string MemberType { get; set; }

        /// <summary>
        /// ID, 비회원은 Email 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// EMail
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 주문일
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 예식일
        /// </summary>
        public DateTime? WeddingDate { get; set; }
        /// <summary>
        /// 제품 정보 링크
        /// </summary>
        public string Invitation_Url { get; set; }

        public string OrderTitle { get; set; }
        public string OrderName { get; set; }
        public string ProductName { get; set; }
        public int SalePrice { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public string OrderStateName { get; set; }

        public string POrderCode { get; set; }
        public DateTime? PPaymentDate { get; set; }
    }

    /// <summary>
    /// 환불 취소 데이터 모델
    /// </summary>
    public class FlaRefundDataModel
    {
        // <summary>
        /// 주문 ID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 주문번호
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 회원 구분: U - 회원 / G - 비회원
        /// </summary>
        public string MemberType { get; set; }

        /// <summary>
        /// ID, 비회원은 Email 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// EMail
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 주문일
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 취소 환불 일
        /// </summary>
        public DateTime? RefundDate { get; set; }
        /// <summary>
        /// 예식일
        /// </summary>
        public DateTime? WeddingDate { get; set; }
        /// <summary>
        /// 제품 정보 링크
        /// </summary>
        public string Invitation_Url { get; set; }

        public string OrderTitle { get; set; }
        public string OrderName { get; set; }
        public string ProductName { get; set; }
        public int SalePrice { get; set; }

        /// <summary>
        /// 결제방법 - 코드
        /// </summary>
        public string Payment_Method_Code { get; set; }

        /// <summary>
        /// 결제방법 - 명
        /// </summary>
        public string Payment_Method_Name { get; set; }

        /// <summary>
        /// 결제상태 - 코드
        /// </summary>
        public string Payment_Status_Code { get; set; }

        /// <summary>
        /// 결제상태 = 명
        /// </summary>
        public string Payment_Status_Name { get; set; }
        public string POrderCode { get; set; }
        public DateTime? PPaymentDate { get; set; }

    }

    /// <summary>
    /// 화한 배너 관리 검색
    /// </summary>
    public class FlaBannerManageSearchViewModel : PageViewModel
    {
        /// <summary>
        /// 검색어
        /// </summary>
        public string Keyword { get; set; } = "";
        public string Allowed { get; set; } = "ALL";
        public IEnumerable<SelectListItem> Alloweds { get; set; } = new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "전체", Value = "ALL" },
                        new SelectListItem { Text = "노출", Value = "True" },
                        new SelectListItem { Text = "노출안함", Value = "False" }
                    }, "Value", "Text");


        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<FlaBannerManageDataModel> DataModel { set; get; }
               

        public override int PageSize { get; set; } = 10;

        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(Keyword), Keyword },
                    { nameof(Allowed), Allowed },
                    { nameof(PageSize), PageSize.ToString() },
                };
                return routeall;
            }
        }
    }

    /// <summary>
    /// 화환배너관리 데이터
    /// </summary>
    public class FlaBannerManageDataModel
    {
        public int Id { get; set; }
          
        public string GroupName { get; set; } = "";

        public string Allowed { get; set; } = "";

        public string? Keywords { get; set; } = null;

        public DateTime RegistDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public string RegistUserName { get; set; } = "";
    }

    public class FlaBannerManageEditModel
    {
        public int Id { get; set; } = 0;

        public string GroupName { get; set; } = "";

        public bool Allowed { get; set; } = false;
        public IEnumerable<SelectListItem> Alloweds { get; set; } = new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "노출", Value = "True" },
                        new SelectListItem { Text = "노출안함", Value = "False" }
                    }, "Value", "Text");

        public List<string> Keywords { get; set; } = new List<string>();

        public string? Message { get; set; } = null;
    }
}
