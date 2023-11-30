using Barunn.MobileInvitation.Dac.Models.Barunson;
using Barunn.MobileInvitation.Dac.Models.BarunsonView;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 통계 공통 뷰 모델
    /// </summary>
    public class StatisticsViewModel<TEntity> where TEntity : class
    {
        /// <summary>
        /// 선택 - 년
        /// </summary>
        public int SelectedYear { set; get; }
        /// <summary>
        /// 선택 - 월
        /// </summary>
        public int SelectedMonth { set; get; }

        public int StartYear { set; get; } = 2021;
        /// <summary>
        /// 연도 선택 값 목록
        /// </summary>
        public IEnumerable<SelectListItem> Years
        {
            get
            {
                int start = StartYear;
                int count = DateTime.Now.Year - start + 1;
                return new SelectList(Enumerable.Range(start, count).Select(x =>
                     new SelectListItem()
                     {
                         Text = x.ToString() + "년",
                         Value = x.ToString()
                     }), "Value", "Text");
            }
        }

        /// <summary>
        /// 월 선택 값 목록
        /// </summary>
        public IEnumerable<SelectListItem> Months
        {
            get
            {
                return new SelectList(Enumerable.Range(1, 12).Select(x =>
                     new SelectListItem()
                     {
                         Text = x.ToString() + "월",
                         Value = x.ToString()
                     }), "Value", "Text");
            }
        }

        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<TEntity> Items { set; get; }

        /// <summary>
        /// 아이템 합계
        /// </summary>
        public TEntity Total { set; get; }
    }

    public class StatisticsProduceViewModel<TEntity> where TEntity : class
    {
        /// <summary>
        /// 선택 - 년
        /// </summary>
        public int SelectedYear { set; get; }
        /// <summary>
        /// 선택 - 월
        /// </summary>
        public int SelectedMonth { set; get; }

        /// <summary>
        /// 연도 선택 값 목록
        /// </summary>
        public IEnumerable<SelectListItem> Years
        {
            get
            {
                int start = 2022;
                int count = DateTime.Now.Year - start + 1;
                return new SelectList(Enumerable.Range(start, count).Select(x =>
                     new SelectListItem()
                     {
                         Text = x.ToString() + "년",
                         Value = x.ToString()
                     }), "Value", "Text");
            }
        }

        /// <summary>
        /// 월 선택 값 목록
        /// </summary>
        public IEnumerable<SelectListItem> Months
        {
            get
            {
                return new SelectList(Enumerable.Range(1, 12).Select(x =>
                     new SelectListItem()
                     {
                         Text = x.ToString() + "월",
                         Value = x.ToString()
                     }), "Value", "Text");
            }
        }

        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public List<TEntity> Items { set; get; }

        /// <summary>
        /// 아이템 합계
        /// </summary>
        public TEntity Total { set; get; }
    }

    /// <summary>
    /// 통계 구매 패턴 뷰 모델
    /// </summary>
    public class StatisticsPurchaseViewModel
    {
        public int TermType { get; set; } = 1;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public StatisticsOrderPattern Item { set; get; }
    }

    /// <summary>
    /// 제품 통계 뷰 모델
    /// </summary>
    public class StatisticsProductsViewModel
    {
        public int TermType { get; set; } = 1;

        /// <summary>
        /// 검색 시작일
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        /// <summary>
        /// 검색 종료일
        /// </summary>
        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// 브렌드 목록 체크박스
        /// </summary>
        public List<SelectListItem> Barans { get; set; }
       
        /// <summary>
        /// 검색 키워드
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 정력 필드 목록 선택박스
        /// </summary>
        public IEnumerable<SelectListItem> Orders
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "브랜드↑", Value = "brand_asc" },
                        new SelectListItem { Text = "브랜드↓", Value = "brand_desc" },
                        new SelectListItem { Text = "제품코드↑", Value = "code_asc" },
                        new SelectListItem { Text = "제품코드↓", Value = "code_desc" },
                        new SelectListItem { Text = "무료건수↑", Value = "free_asc" },
                        new SelectListItem { Text = "무료건수↓", Value = "free_desc" },
                        new SelectListItem { Text = "유료건수↑", Value = "pay_asc" },
                        new SelectListItem { Text = "유료건수↓", Value = "pay_desc" },
                        new SelectListItem { Text = "건수합계↑", Value = "total_asc" },
                        new SelectListItem { Text = "건수합계↓", Value = "total_desc" }
                    }, "Value", "Text");
            }
        }

        /// <summary>
        /// 정렬필드
        /// </summary>
        public string SelectedOrderby { get; set; } = "total_desc";
       
        /// <summary>
        /// 출력 목록
        /// </summary>
        public List<StatisticsOrderProduct> Items { set; get; }

        public Dictionary<string, string> ToRouteValues
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(StartDate), StartDate.ToString("yyyy-MM-dd") },
                    { nameof(EndDate), EndDate.ToString("yyyy-MM-dd") },
                    { nameof(Keyword), Keyword },
                    { nameof(SelectedOrderby), SelectedOrderby }
                };
                if (Barans != null)
                {
                    for (var i = 0; i < Barans.Count; i++)
                    {
                        routeall.Add($"Barans[{i}].Selected", Barans[i].Selected.ToString());
                        routeall.Add($"Barans[{i}].Value", Barans[i].Value);
                        routeall.Add($"Barans[{i}].Text", Barans[i].Text);
                    }
                }
                return routeall;
            }
        }
    }

}
