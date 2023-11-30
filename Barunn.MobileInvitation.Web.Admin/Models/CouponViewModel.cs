using Barunn.MobileInvitation.Dac.Models.Barunson;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 쿠폰 검색 뷰 모델
    /// </summary>
    public class CouponSearchViewModel : PageViewModel
    {
        /// <summary>
        /// 검색어
        /// </summary>
        public string Keyword { get; set; } = "";

        public string ReturnUrl { get; set; }

        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(Keyword), Keyword }
                };
                return routeall;
            }
        }


        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<CouponSearchDataModel> DataModel { set; get; }
    }

    /// <summary>
    /// 쿠폰 검색 데이터 모델
    /// </summary>
    public class CouponSearchDataModel
    {
        /// <summary>
        /// 쿠폰 Id
        /// </summary>
        public int Coupon_ID { get; set; }

        /// <summary>
        /// 쿠폰명
        /// </summary>
        public string Coupon_Name { get; set; }

        /// <summary>
        /// 발급방식 코드
        /// </summary>
        public string Publish_Method_Code { get; set; }

        /// <summary>
        /// 발급방식 명
        /// </summary>
        public string Publish_Method_Name { get; set; }

        /// <summary>
        /// 할인 비울
        /// </summary>
        public double? Discount_Rate { get; set; }

        /// <summary>
        /// 할인 금액
        /// </summary>
        public int? Discount_Price { get; set; }

        /// <summary>
        /// 할인방식 코드
        /// </summary>
        public string Discount_Method_Code { get; set; }

        /// <summary>
        /// 할인방식 명
        /// </summary>
        public string Discount_Method_Name { get; set; }

        /// <summary>
        /// 발행건수
        /// </summary>
        public int Publish_count { get; set; }

        /// <summary>
        /// 사용건수
        /// </summary>
        public int Use_count { get; set; }

        /// <summary>
        /// 미사용건수
        /// </summary>
        public int UnUse_count { get; set; }

        /// <summary>
        /// 회수건수
        /// </summary>
        public int Retrive_count { get; set; }

        /// <summary>
        /// 등록일
        /// </summary>
        public DateTime? Regist_DateTime { get; set; }

        public string Use_YN { get; set; }
        public string Use_YN_Text { get; set; }

    }

    /// <summary>
    /// 쿠폰 등록/수정 뷰 모델
    /// </summary>
    public class CouponEditModel
    {
        /// <summary>
        /// 쿠폰 Id
        /// </summary>
        public int Coupon_ID { get; set; }

        /// <summary>
        /// 쿠폰명
        /// </summary>
        [Required(ErrorMessage = "쿠폰명을 입력해주세요")]
        public string Coupon_Name { get; set; }

        /// <summary>
        /// 발급방식 코드
        /// </summary>
        [Required(ErrorMessage = "발급방식을 선택해주세요")]
        public string Publish_Method_Code { get; set; }
        public IEnumerable<SelectListItem> Publish_Method_Codes { get; set; }

        /// <summary>
        /// 발급대상
        /// </summary>
        [Required(ErrorMessage = "발급대상을 선택해주세요")]
        public string Publish_Target_Code { get; set; }
        public IEnumerable<SelectListItem> Publish_Target_Codes { get; set; }

        /// <summary>
        /// 사용가능 기준
        /// </summary>
        [Required(ErrorMessage = "사용가능 기준을 선택해주세요")]
        public string Use_Available_Standard_Code { get; set; }
        public IEnumerable<SelectListItem> Use_Available_Standard_Codes { get; set; }
        /// <summary>
        /// 사용가능 - 구매금액 기준시 기준금액
        /// </summary>
        public int? Standard_Purchase_Price { get; set; }

        /// <summary>
        /// 적용 상품
        /// </summary>
        [Required(ErrorMessage = "적용 상품을 선택해주세요")]
        public string Coupon_Apply_Code { get; set; }
        public IEnumerable<SelectListItem> Coupon_Apply_Codes { get; set; }
        /// <summary>
        /// 적용 상품 - 적용 Prodoct ID , TB_COUPON_APPLY_PRODUCT 의 키로 신규시 자동 넘버링 생성
        /// </summary>
        public int? Coupon_Apply_Product_ID { get; set; }
        /// <summary>
        /// 적용 상품 - 적용/제외 Prodoct Code 목록, TB_APPLY_PRODUCT 테이블에 기록
        /// </summary>
        public List<string> Apply_Product_Codes { get; set; } = new List<string>();

        /// <summary>
        /// 할인금액
        /// DMC01	금액
        /// DMC02	%
        /// DMC03 전액할인
        /// </summary>
        [Required(ErrorMessage = "할인금액을 선택해주세요")]
        public string Discount_Method_Code { get; set; }
        public IEnumerable<SelectListItem> Discount_Method_Codes { get; set; }
        /// <summary>
        /// 할인 값 - 금액(int), 비율
        /// </summary>
        public double? Discount_Value { get; set; }

        /// <summary>
        /// 유효기간  구분
        /// PMC01 시작일~종료일
        /// PMC02 발행일로부터
        /// PMC03 무제한
        /// </summary>
        [Required(ErrorMessage = "유효기간을 선택해주세요")]
        public string Period_Method_Code { get; set; }
        public IEnumerable<SelectListItem> Period_Method_Codes { get; set; }

        /// <summary>
        /// 시작일, pmc가 PMC01 일 경우
        /// </summary>
        public DateTime? Publish_Start_Date { get; set; }
        /// <summary>
        /// 종료일 pmc가 PMC01 일 경우
        /// </summary>
        public DateTime? Publish_End_Date { get; set; }

        /// <summary>
        /// pmc가 PMC02 일 경우  유효기간 - 발행일부터 시 날짜 값
        /// </summary>
        public string Publish_Period_Code { get; set; }
        /// <summary>
        /// pmc가 PMC02 일 경우  유효기간 - 발행일부터 시 날짜 값 목록 5,10,~100
        /// </summary>
        public IEnumerable<SelectListItem> Publish_Period_Codes { get; set; }

        /// <summary>
        /// 쿠폰 사용여부
        /// </summary>
        [Required(ErrorMessage = "사용 여부를 선택해주세요")]
        public string Use_YN { get; set; }
        public IEnumerable<SelectListItem> Use_YNs { 
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "사용", Value = "Y" },
                        new SelectListItem { Text = "중지", Value = "N" }
                    }, "Value", "Text");
            }
        }
        public string Description { get; set; }

        public bool IsSave { get; set; }
    }

    /// <summary>
    /// 쿠폰 상품 데이터 모델
    /// </summary>
    public class CouponApplyProdcutListDataModel
    {
        public int Product_ID { get; set; }
        public string Product_Category_Name { get; set; }
        public string Product_Name { get; set; }
        public string Product_Code { get; set; }
        public string Display_YN { get; set; }
        public int Price { get; set; }

    }

    /// <summary>
    /// 쿠폰 발행 검색 뷰 모델
    /// </summary>
    public class CouponPublishSearchViewModel : PageViewModel
    {
        /// <summary>
        /// 쿠폰 Id
        /// </summary>
        public int Coupon_ID { get; set; }
        public string Coupon_Name { get; set; }
        /// <summary>
        /// 검색어
        /// </summary>
        public string Keyword { get; set; } = "";
        /// <summary>
        /// 사용여부
        /// </summary>
        public string Use_YN { get; set; } = "ALL";
        /// <summary>
        /// 사용여부 목록
        /// </summary>
        public IEnumerable<SelectListItem> Use_YNs
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "전체", Value = "ALL" },
                        new SelectListItem { Text = "사용", Value = "Y" },
                        new SelectListItem { Text = "미사용", Value = "N" }
                    }, "Value", "Text");
            }
        }
        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(Coupon_ID), Coupon_ID.ToString() },
                    { nameof(Coupon_Name), Coupon_Name},
                    { nameof(Keyword), Keyword},
                    { nameof(Use_YN), Use_YN}
                };
                return routeall;
            }
        }

        public override int PageSize  { get; set; } = 50;

        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<CouponPublishSearchDataModel> DataModel { set; get; }
        public class CouponPublishSearchDataModel
        {
            public int Coupon_Publish_ID { get; set; }
            public int Coupon_ID { get; set; }
            public string User_ID { get; set; }
            public string User_Name { get; set; }
            public string Phone_Number { get; set; }
            public string Use_YN { get; set; }
            public DateTime? Regist_DateTime { get; set; }
            public DateTime? Use_DateTime { get; set; }
            public string Expiration_Date { get; set; }
            public DateTime? Retrieve_DateTime { get; set; }
        }
    }

    public class CouponPublishAddViewModel
    {
        /// <summary>
        /// 쿠폰 Id
        /// </summary>
        public int Coupon_ID { get; set; }
        public string Coupon_Name { get; set; }
        /// <summary>
        /// 검색어
        /// </summary>
        public string Keyword { get; set; } = "";

        
        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<CouponPublishAddDataModel> DataModel { set; get; }
        public class CouponPublishAddDataModel
        {
            /// <summary>
            /// Id
            /// </summary>
            public string User_ID { get; set; }
            /// <summary>
            /// 이름
            /// </summary>
            public string User_Name { get; set; }
            /// <summary>
            /// 연락처
            /// </summary>
            public string Phone_Number { get; set; }
            /// <summary>
            /// 가입일
            /// </summary>
            public DateTime Regist_DateTime { get; set; }
            /// <summary>
            /// 웨딩날짜
            /// </summary>
            public DateTime? Wedding_Date { get; set; }
        }

    }

    public class CouponPublishAddSaveViewModel
    {
        /// <summary>
        /// 쿠폰 Id
        /// </summary>
        public int Coupon_ID { get; set; }
        public List<SelectListItem> UserIds { get; set; } = new List<SelectListItem>();

    }
}