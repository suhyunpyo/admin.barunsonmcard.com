using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

namespace Barunn.MobileInvitation.Web.Admin.Models
{

    /// <summary>
    /// 송금 서비스 주문별 검색 모델
    /// </summary>
    public class KakaoRemitOrderSearchViewModel<T> : PageViewModel
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
        /// 정산
        /// </summary>
        public string CalculateType { get; set; } = "";
        public IEnumerable<SelectListItem> CalculateTypes { get; set; } = new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "전체", Value = "" },
                        new SelectListItem { Text = "정산완료", Value = "remit" },
                        new SelectListItem { Text = "미정산", Value = "remain" }
                    }, "Value", "Text");

        /// <summary>
        /// 총 송금건수
        /// </summary>
        public int RemitCount { get; set; }
        /// <summary>
        /// 총 송금액
        /// </summary>
        public int TotalPrice { get; set; }
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
                    { nameof(CalculateType), CalculateType },
                    { nameof(StartDate), StartDate.ToString("yyyy-MM-dd") },
                    { nameof(EndDate), EndDate.ToString("yyyy-MM-dd") },
                    { nameof(PageSize), PageSize.ToString() },
                };
                return routeall;
            }
        }
    }


    /// <summary>
    /// 송금 서비스 주문별  데이터 모델
    /// </summary>
    public class KakaoRemitOrderDataModel
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
        /// 초대장 ID
        /// </summary>
        public int InvitationID { get; set; }
        /// <summary>
        /// ID, 비회원은 Email 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 계좌수
        /// </summary>
        public int AccountCount { get; set; }

        /// <summary>
        /// 송금건수
        /// </summary>
        public int RemitCount { get; set; }
        /// <summary>
        /// 송금액
        /// </summary>
        public int TotalPrice { get; set; }
        /// <summary>
        /// 수수료
        /// </summary>
        public int RemitTax { get; set; }
        /// <summary>
        /// 정산완료
        /// </summary>
        public int RemitPrice { get; set; }
        /// <summary>
        /// 미정산
        /// </summary>
        public int RemainPrice { get; set; }

        /// <summary>
        /// 주문일
        /// </summary>
        public DateTime? RegistDate { get; set; }

        /// <summary>
        /// 예식일
        /// </summary>
        public DateTime? WeddingDate { get; set; }
        /// <summary>
        /// 계좌 설정일
        /// </summary>
        public DateTime? AccountDate { get; set; }
        /// <summary>
        /// 제품 정보 링크
        /// </summary>
        public string Invitation_Url { get; set; }
    }

    /// <summary>
    /// 송금 주문별 세부 팝업 뷰 모델
    /// </summary>
    public class KakaoRemitViewModel<T>
    {
        /// <summary>
        /// 초대장 ID
        /// </summary>
        public int InvitationID { get; set; }
        /// <summary>
        /// 이름
        /// </summary>
        public string UserName { get; set; }

        public List<T> DataModel { set; get; }

        /// <summary>
		/// 송금 대상 목록
		/// </summary>
        public SelectList AccountTypeList { get; set; }
        /// <summary>
		/// 은행 목록
		/// </summary>
		public SelectList BankList { get; set; }
    }
    /// <summary>
    /// 송금 내역 데이터 모델
    /// </summary>
    public class KakaoRemitDataModel
    {
        public int RemitID { get; set; }
        /// <summary>
		/// 송금대상
		/// </summary>
        public string AccountTypeName { get; set; }
        /// <summary>
		/// 은행코드
		/// </summary>
        public string BankName { get; set; }
        /// <summary>
		/// 예금주
		/// </summary>
		public string DepositorName { get; set; }
        /// <summary>
		/// 계좌번호
		/// </summary>
		public string AccountNumber { get; set; }
        /// <summary>
        /// 송금자 명
        /// </summary>
        public string RemitterName { get; set; }
        /// <summary>
        /// 송금액
        /// </summary>
        public int TotalPrice { get; set; }
        /// <summary>
        /// 수수료
        /// </summary>
        public int RemitTax { get; set; }
        /// <summary>
        /// 정산완료
        /// </summary>
        public int RemitPrice { get; set; }
        /// <summary>
        /// 미정산
        /// </summary>
        public int RemainPrice => (TotalPrice - RemitTax - RemitPrice);

        /// <summary>
        /// 송금일
        /// </summary>
        public DateTime? CompleteDateTime { get; set; }

        /// <summary>
        /// 정산일
        /// </summary>
        public DateTime? CalculateDateTime { get; set; }
    }

    public class KakaoAccountDataModel
    {
        /// <summary>
		/// 계좌 ID
		/// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 송금 대상 코드
        /// </summary>
        public string AccountTypeCode { get; set; }
        /// <summary>
		/// 은행코드
		/// </summary>
		public string BankCode { get; set; }
        /// <summary>
		/// 계좌번호
		/// </summary>
		public string AccountNumber { get; set; }
        /// <summary>
        /// 예금주
        /// </summary>
        public string DepositorName { get; set; }
        public int Sort { get; set; }
    }

    /// <summary>
    /// 송금 건별 데이터 모델
    /// </summary>
    public class KakaoRemitListDataModel
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
        /// 초대장 ID
        /// </summary>
        public int InvitationID { get; set; }
        public int RemitID { get; set; }
        /// <summary>
        /// ID, 비회원은 Email 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
		/// 송금대상
		/// </summary>
        public string AccountTypeName { get; set; }
        /// <summary>
		/// 은행코드
		/// </summary>
        public string BankName { get; set; }
        /// <summary>
		/// 예금주
		/// </summary>
		public string DepositorName { get; set; }
        /// <summary>
		/// 계좌번호
		/// </summary>
		public string AccountNumber { get; set; }
        /// <summary>
        /// 송금자 명
        /// </summary>
        public string RemitterName { get; set; }
        /// <summary>
        /// 송금액
        /// </summary>
        public int TotalPrice { get; set; }
        /// <summary>
        /// 수수료
        /// </summary>
        public int RemitTax { get; set; }
        /// <summary>
        /// 정산완료
        /// </summary>
        public int RemitPrice { get; set; }
        /// <summary>
        /// 미정산
        /// </summary>
        public int RemainPrice => (TotalPrice - RemitTax - RemitPrice);
        /// <summary>
        /// 예식일
        /// </summary>
        public DateTime? WeddingDate { get; set; }
        /// <summary>
        /// 계좌 설저일
        /// </summary>
        public DateTime? AccountDate { get; set; }
        /// <summary>
        /// 송금일
        /// </summary>
        public DateTime? CompleteDateTime { get; set; }

        /// <summary>
        /// 정산일
        /// </summary>
        public DateTime? CalculateDateTime { get; set; }
        /// <summary>
        /// 제품 정보 링크
        /// </summary>
        public string Invitation_Url { get; set; }
    }

    /// <summary>
    /// 송금수수료 모델
    /// </summary>
    public class KakaoRemitTaxDataModel
    {
        /// <summary>
        /// 수수료 ID
        /// </summary>
        public int TaxId { get; set; }
        /// <summary>
        /// 수수료
        /// </summary>
        public int Tax { get; set; }
        /// <summary>
        /// 이전 수수료
        /// </summary>
        public int? PreviousTax { get; set; }
        /// <summary>
        /// 등록일
        /// </summary>
        public DateTime? RegistDateTime { get; set; }
        /// <summary>
        /// 등록자
        /// </summary>
        public string RegistUserID { get; set; }
        public string RegistUserName { get; set; }
    }
}
