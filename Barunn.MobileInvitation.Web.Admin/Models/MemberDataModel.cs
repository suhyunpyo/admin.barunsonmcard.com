using System;
using System.Collections.Generic;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 회원 정보 - 관리자용
    /// </summary>
    public class AdmimOrderMember
    {
        /// <summary>
        /// 주문 번호
        /// </summary>
        public int Order_ID { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string User_Name { get; set; }

        /// <summary>
        /// UserID or Email
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 청첩장 구배 여부
        /// </summary>
        public bool InvitationBuy { get; set; }

        /// <summary>
        /// 가입 몰 구분
        /// </summary>
        public string Join_Type { get; set; }

        /// <summary>
        /// 웨딩날짜
        /// </summary>
        public string Wedding_Date { get; set; }

        /// <summary>
        /// 주문 날짜
        /// </summary>
        public DateTime? Regist_Datetime { get; set; }

        /// <summary>
        /// 회원 구분: U - 회원 / G - 비회원
        /// </summary>
        public string Member_Type { get; set; }
        
        /// <summary>
        /// 모초유료제작 여부
        /// </summary>
        public bool BMBuy { get; set; }

        /// <summary>
        /// 모초무료제작 여부
        /// </summary>
        public bool BMFree { get; set; }

    }

    /// <summary>
    /// 회원 상세 정보 - 관리자용
    /// </summary>
    public class AdminOrderMemberDetail
    {
        #region 1.기본정보
        /// <summary>
        /// 이름
        /// </summary>
        public string User_Name { get; set; }

        /// <summary>
        /// ID, 비회원은 Email 
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 연락처 모바일
        /// </summary>
        public string User_Hp { get; set; }

        /// <summary>
        /// EMail
        /// </summary>
        public string User_Email { get; set; }

        /// <summary>
        /// 예식일
        /// </summary>
        public string Wedding_Date { get; set; }

        /// <summary>
        /// 회원 가입일
        /// </summary>
        public DateTime? Regist_Datetime { get; set; }

        public int? LastOrderID { get; set; }
        #endregion

        #region 2.주문정보
        /// <summary>
        /// 주문정보 목록
        /// </summary>
        public List<OrderInfo> OrderList { get; set; }

        /// <summary>
        /// 주문 정보
        /// </summary>
        public class OrderInfo
        {
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
            /// 쿠폰사용금액,  TB_Order_Coupon_Uses 의 Discount_Price
            /// </summary>
            public int? Coupon_Use_Price { get; set; }

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

            /// <summary>
            /// 주문일
            /// </summary>
            public DateTime? Regist_DateTime { get; set; }

            /// <summary>
            /// 결제일
            /// </summary>
            public DateTime? Order_DateTime { get; set; }
            /// <summary>
            /// 결제취소일
            /// </summary>
            public DateTime? Cancel_DateTime { get; set; }

            /// <summary>
            /// 제품 정보 링크
            /// </summary>
            public string Invitation_Url { get; set; }

            /// <summary>
            /// 예식일
            /// </summary>
            public string Wedding_Date { get; set; }

            /// <summary>
            /// 환불여부
            /// </summary>
            public bool RefundYn { get; set; }
        }
        #endregion

        #region 3.쿠폰정보
        /// <summary>
        /// 쿠폰 정보 목록
        /// </summary>
        public List<CouponInfo> CouponList { get; set; }

        /// <summary>
        /// 쿠폰 정보
        /// </summary>
        public class CouponInfo
        {
            /// <summary>
            /// 배포 ID
            /// </summary>
            public int Coupon_Publish_Id { get; set; }

            /// <summary>
            /// 쿠폰 코드
            /// </summary>
            public int Coupon_Id { get; set; }

            /// <summary>
            /// 쿠폰 이름
            /// </summary>
            public string Coupon_Name { get; set; }

            /// <summary>
            /// 쿠폰 사용여부 Y or N
            /// </summary>
            public string Use_Yn { get; set; }

            /// <summary>
            /// 쿠폰 금액
            /// </summary>
            public int? Discount_Price { get; set; }

            /// <summary>
            /// 쿠폰 비율
            /// </summary>
            public double? Discount_Rate { get; set; }

            /// <summary>
            /// 쿠폰 사용 구분
            /// </summary>
            public string Discount_Method_Code { get; set; }

            /// <summary>
            /// 쿠폰 사용일
            /// </summary>
            public DateTime? Use_DateTime { get; set; }

            /// <summary>
            /// 발급일
            /// </summary>
            public DateTime? Regist_Datetime { get; set; }

            /// <summary>
            /// 회수일
            /// </summary>
            public DateTime? Retrieve_Datetime { get; set; }

            /// <summary>
            /// 유효기간 - 만료일
            /// </summary>
            public string Expiration_Date { get; set; }
        }
        #endregion

        #region 4. 결재 환불 정보 
        /// <summary>
        /// 결제 환불 목록
        /// </summary>
        public List<PayAndRefundInfo> PayAndRefundInfoList { get; set; }

        /// <summary>
        /// 결제 환불 정보
        /// </summary>
        public class PayAndRefundInfo
        {
            /// <summary>
            /// 주문번호
            /// </summary>
            public int Order_Id { get; set; }

            /// <summary>
            /// 주문번호
            /// </summary>
            public string Order_Code { get; set; }

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
            /// 결제상태 - 명
            /// </summary>
            public string Payment_Status_Name { get; set; }

            /// <summary>
            /// 결제기관명
            /// </summary>
            public string Finance_Name { get; set; }

            /// <summary>
            /// 계좌정보
            /// </summary>
            public string Account_Number { get; set; }

            /// <summary>
            /// 주문일
            /// </summary>
            public DateTime? Regist_Datetime { get; set; }

            /// <summary>
            /// 결제일
            /// </summary>
            public DateTime? Payment_Datetime { get; set; }

            /// <summary>
            /// 환불금액
            /// </summary>
            public int? Refund_Price { get; set; }

            /// <summary>
            /// 환불날짜
            /// </summary>
            public DateTime? Refund_Datetime { get; set; }

            /// <summary>
            /// 환불타입_명
            /// </summary>
            public string Refund_Type_Name { get; set; }

            /// <summary>
            /// 환불타입_코드
            /// </summary>
            public string Refund_Type_Code { get; set; }

            /// <summary>
            /// 환불상태_코드
            /// </summary>
            public string Refund_Status_Code { get; set; }

            /// <summary>
            /// 환불상태_명
            /// </summary>
            public string Refund_Status_Name { get; set; }

            public string Invitation_Url { get; set; }
        }
        #endregion

        #region  5. 고객 상담
        /// <summary>
        /// 고객 QnA 목록
        /// </summary>
        public List<UserQnA> UserQnAList { get; set; }

        /// <summary>
        /// 고객 QnA
        /// </summary>
        public class UserQnA
        {
            /// <summary>
            /// 고객 문의
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 답변
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// 등록일
            /// </summary>
            public DateTime Regist_Datetime { get; set; }

            /// <summary>
            /// 답변일
            /// </summary>
            public DateTime? Answer_Datetime { get; set; }

            /// <summary>
            /// 답변자
            /// </summary>
            public string Answer_Id { get; set; }
        }
        #endregion

        #region  6. 관리자 메모 
        public List<AdminMemo> AdminMemoList { get; set; }
        public class AdminMemo
        {
            public int Memo_ID { get; set; }
            public string Content { get; set; }
            public string Regist_User_ID { get; set; }
            public string Regist_User_Name { get; set; }
            public DateTime? Regist_DateTime
            {
                get; set;
            }
            public int? Order_ID
            {
                get; set;
            }
        }
        #endregion

        public Uri BarunsonmCardUrl { get; set; }

    }

}
