using Barunn.MobileInvitation.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;


namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 주문 목록 검색 모델
    /// </summary>
    public class OrderSearchViewModel : PageViewModel
    {
        /// <summary>
        /// 검색어
        /// </summary>
        public string Keyword { get; set; } = "";
        /// <summary>
        /// 검색 날짜 조건 - Select box
        /// </summary>
        public string Period { get; set; } = "Order";
        public IEnumerable<SelectListItem> Periods
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "주문일", Value = "Order" },
                        new SelectListItem { Text = "결제일", Value = "Pay" }
                    }, "Value", "Text");
            }
        }
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
        /// 분류 목록 - 다중 선택, Checkbox
        /// </summary>
        public List<SelectListItem> ProductCategories { get; set; }
        /// <summary>
        /// 브렌드 목록 - 다중 선택, CheckBox
        /// </summary>
        public List<SelectListItem> ProductBarnds { get; set; }

        /// <summary>
        /// 결제상태
        /// </summary>
        public int PaymentStatus { get; set; } = 0;

        /// <summary>
        /// 결제상태 목록 - Radio button
        /// </summary>
        public IEnumerable<SelectListItem> PaymentStatuses 
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "전체", Value = "0" },
                        new SelectListItem { Text = "결제완료", Value = "1" },
                        new SelectListItem { Text = "입금대기", Value = "2" },
                        new SelectListItem { Text = "취소/환불", Value = "3" }
                    }, "Value", "Text");
                    
            }
        }
        public Dictionary<int, List<string>> PaymentStatuseCodes
        {
            get
            {
                return new Dictionary<int, List<string>>
                {
                    {0, null },
                    {1, new List<string>{ "PSC02" } },
                    {2, new List<string>{ "PSC04" } },
                    {3, new List<string>{ "PSC03", "PSC05" } },
                };
            }
        }

        /// <summary>
        /// 중복구매 여부
        /// </summary>
        public bool OverlapPurchase { get; set; } = false;

        /// <summary>
        /// 회원 - Radio Button
        /// </summary>
        public string MemberYn { get; set; } = "ALL";
        public IEnumerable<SelectListItem> MemberGubuns => MemberGubunModel.MemberGubuns;

        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<OrderSearchDataModel> DataModel { set; get; }

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
                    { nameof(TermType), TermType.ToString() },
                    { nameof(StartDate), StartDate.ToString("yyyy-MM-dd") },
                    { nameof(EndDate), EndDate.ToString("yyyy-MM-dd") },
                    { nameof(PaymentStatus), PaymentStatus.ToString() },
                    { nameof(OverlapPurchase), OverlapPurchase.ToString() },
                    { nameof(MemberYn), MemberYn},
                    { nameof(PageSize), PageSize.ToString() },
                };
                if (ProductCategories != null)
                {
                    for (var i = 0; i < ProductCategories.Count; i++)
                    {
                        routeall.Add($"ProductCategories[{i}].Selected", ProductCategories[i].Selected.ToString());
                        routeall.Add($"ProductCategories[{i}].Value", ProductCategories[i].Value);
                        routeall.Add($"ProductCategories[{i}].Text", ProductCategories[i].Text);
                    }
                }
                if (ProductBarnds != null)
                {
                    for (var i = 0; i < ProductBarnds.Count; i++)
                    {
                        routeall.Add($"ProductBarnds[{i}].Selected", ProductBarnds[i].Selected.ToString());
                        routeall.Add($"ProductBarnds[{i}].Value", ProductBarnds[i].Value);
                        routeall.Add($"ProductBarnds[{i}].Text", ProductBarnds[i].Text);
                    }
                }
                return routeall;
            }
        }
    }
    public class OrderSearchDataModel
    {
        public string Product_Brand_Code { get; set; }
        public string Product_Brand_Name{ get; set; }

        public string Sales_Gubun_Code { get; set; }
        public string Sales_Gubun_Name { get; set; }


        /// <summary>
        /// ID, 비회원은 Email 
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string User_Name { get; set; }

        /// <summary>
        /// EMail
        /// </summary>
        public string User_Email { get; set; }

        /// <summary>
        /// 회원 구분: U - 회원 / G - 비회원
        /// </summary>
        public string Member_Type { get; set; }
        public string Order_Path { get; set; }
        public string Payment_Path { get; set; }

        /// <summary>
        /// 주문 ID
        /// </summary>
        public int Order_ID { get; set; }

        /// <summary>
        /// 주문번호
        /// </summary>
        public string Order_Code { get; set; }

        /// <summary>
        /// 제품 ID
        /// </summary>
        public int Product_ID { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary>
        public string Product_Code { get; set; }

        /// <summary>
        /// 주문금액
        /// </summary>
        public int Order_Price { get; set; }

        /// <summary>
        /// 쿠폰사용금액
        /// </summary>
        public int? Coupon_Price { get; set; }

        /// <summary>
        /// 결제금액
        /// </summary>
        public int? Payment_Price { get; set; }

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
        public DateTime? Payment_DateTime { get; set; }

        /// <summary>
        /// 주문일
        /// </summary>
        public DateTime? Regist_DateTime { get; set; }

        /// <summary>
        /// 예식일
        /// </summary>
        public string Wedding_Date { get; set; }
        /// <summary>
        /// 제품 정보 링크
        /// </summary>
        public string Invitation_Url { get; set; }

        /// <summary>
        /// 환불여부
        /// </summary>
        public bool RefundYn { get; set; }
    }

    public class OrderCancelSearchViewModel : PageViewModel
    {
        /// <summary>
        /// 검색어
        /// </summary>
        public string Keyword { get; set; } = "";

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
        /// 브렌드 목록 - 다중 선택, CheckBox
        /// </summary>
        public List<SelectListItem> ProductBarnds { get; set; }

        /// <summary>
        /// 환불 상태 목록 - 다중 선택, CheckBox
        /// </summary>
        public List<SelectListItem> RefundStatuses { get; set; }

        /// <summary>
        /// 환불 방법 목록 - 다중 선택, CheckBox
        /// </summary>
        public List<SelectListItem> RefundTypes { get; set; }

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
        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<OrderCancelSearchDataModel> DataModel { set; get; }

        public string ReturnUrl { get; set; }
        public Uri BarunsonmCardUrl { get; set; }
        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(Keyword), Keyword },
                    { nameof(StartDate), StartDate.ToString("yyyy-MM-dd") },
                    { nameof(EndDate), EndDate.ToString("yyyy-MM-dd") },
                    { nameof(PageSize), PageSize.ToString() },
                };
                if (ProductBarnds != null)
                {
                    for (var i = 0; i < ProductBarnds.Count; i++)
                    {
                        routeall.Add($"ProductBarnds[{i}].Selected", ProductBarnds[i].Selected.ToString());
                        routeall.Add($"ProductBarnds[{i}].Value", ProductBarnds[i].Value);
                        routeall.Add($"ProductBarnds[{i}].Text", ProductBarnds[i].Text);
                    }
                }
                if (RefundStatuses != null)
                {
                    for (var i = 0; i < RefundStatuses.Count; i++)
                    {
                        routeall.Add($"RefundStatuses[{i}].Selected", RefundStatuses[i].Selected.ToString());
                        routeall.Add($"RefundStatuses[{i}].Value", RefundStatuses[i].Value);
                        routeall.Add($"RefundStatuses[{i}].Text", RefundStatuses[i].Text);
                    }
                }
                if (RefundTypes != null)
                {
                    for (var i = 0; i < RefundTypes.Count; i++)
                    {
                        routeall.Add($"RefundTypes[{i}].Selected", RefundTypes[i].Selected.ToString());
                        routeall.Add($"RefundTypes[{i}].Value", RefundTypes[i].Value);
                        routeall.Add($"RefundTypes[{i}].Text", RefundTypes[i].Text);
                    }
                }
                return routeall;
            }
        }
    }

    public class OrderCancelSearchDataModel : OrderSearchDataModel
    {
        public DateTime? Order_DateTime { get; set; }

        public string Refund_Type_Code { get; set; }
        public string Refund_Type_Name { get; set; }

        public string Refund_Status_Code { get; set; }
        public string Refund_Status_Name { get; set; }

        public DateTime? Refund_DateTime { get; set; }
        public string Image_URL { get; set; }

    }
}
