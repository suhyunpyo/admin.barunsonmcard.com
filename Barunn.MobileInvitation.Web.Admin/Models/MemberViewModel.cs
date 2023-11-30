using Barunn.MobileInvitation.Common.Models;
using Barunn.MobileInvitation.Dac.Models.BarunsonView;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 회원 검색 뷰 모델
    /// </summary>
    public class MemberViewModel<TEntity> : PageViewModel 
    {

        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public int TermType { get; set; } = 1;

        public string SeachMemberYn { get; set; } = "ALL";
        public IEnumerable<SelectListItem> MemberGubuns => MemberGubunModel.MemberGubuns;

        public string Searchtxt { get; set; } = "";

        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public TEntity DataModel { set; get; }

        public string ReturnUrl { get; set; }

    }
       
    /// <summary>
    /// 환불 수정 화면 뷰모델
    /// </summary>
    public class RefundInfoViewModel
    {
        public int Order_ID { get; set; }
        public string Order_Code { get; set; }
        public string Trading_Number { get; set; }
        public int? Payment_Price { get; set; }
        public string Payment_Method_Code { get; set; }
        public string Refund_Type_Code { get; set; }
        public int? Refund_Price { get; set; }
        public string Bank_Type_Code { get; set; }
        public string AccountNumber { get; set; }
        public string Refund_Status_Code { get; set; }
        public string Depositor_Name { get; set; }
        public string Refund_Content { get; set; }

        public IEnumerable<SelectListItem> RefundTyeCodes { get; set; }
        public IEnumerable<SelectListItem> RefundStatusCodes { get; set; }
        public IEnumerable<SelectListItem> BankCodes { get; set; }

        /// <summary>
        /// 저장 가능 여부
        /// </summary>
        public bool IsSave { get; set; } = true;

        /// <summary>
        /// 환분 은행 정보 입력 가능 여부
        /// </summary>
        public bool IsBankInput { get; set; }
    }
}
