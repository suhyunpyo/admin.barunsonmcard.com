using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{

    public class StatisticsOrderPattern
    {
        public int McardOrder1Count { set; get; }
        public int McardOrder2Count { set; get; }
        public int MultiOrder1Count { set; get; }
        public int MultiOrder2Count { set; get; }
        public int OrderUserCount { set; get; }
        public int JoinCount { set; get; }
    }

    public class StatisticsOrderProduct
    {
        public string ProductBrandCode { set; get; }
        public string ProductBrandName { set; get; }
        public string ProductCode { set; get; }
        public string ProductName { set; get; }
        public int PayCount { set; get; }
        public int FreeCount { set; get; }
        public int TotalCount { set; get; }
    }

    /// <summary>
    /// 제작 통계 모델
    /// </summary>
    public class StatisticsOrderProduce
    {
        /// <summary>
        /// 날짜: 년월, yyyy-MM
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 종이 청첩장 제작 수
        /// </summary>
        public int CustomOrderCount { set; get; }

        /// <summary>
        /// 모바일 청첩창 제작 수
        /// </summary>
        public int MCardWithPaperCount { set; get; }

        /// <summary>
        /// 모바일 청첩창 제작 하지 않은 수
        /// </summary>
        public int MCardWithoutPaperCount { set; get; }

        /// <summary>
        /// 모바일 청첩창 합계
        /// </summary>
        public int MCardTotalCount => MCardWithPaperCount + MCardWithoutPaperCount;

        /// <summary>
        /// 계좌설정-카카오페이 사용설정 수
        /// </summary>
        public int KaKaoPayConfCount { set; get; }
        /// <summary>
        ///  계좌설정-카카오페이 사용 설정 후 취소한 수
        /// </summary>
        public int KaKaoPayCancelCount { set; get; }
        /// <summary>
        ///  계좌설정-일반송금 사용설정 수
        /// </summary>
        public int RemitConfCount { set; get; }

        /// <summary>
        /// 계좌설정-일반송금 사용 설정 후 취소한 수
        /// </summary>
        public int RemitCancelCount { set; get; }

        /// <summary>
        /// 카카오페이 거래액
        /// </summary>
        public int AccountTotalAmount { set; get; }

        /// <summary>
        /// 화환설정 - 화환선물 사용 설정 수 
        /// </summary>
        public int FlowerConfCount { set; get; }
        /// <summary>
        ///  화환설정 - 화환선물 사용 설정 후 취소한 수
        /// </summary>
        public int FlowerCancelCount { set; get; }

        /// <summary>
        /// 화환 선물 총 매출
        /// </summary>
        public int FlowerTotalAmount { set; get; }
    }

}
