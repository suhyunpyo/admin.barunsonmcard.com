using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class NoticeSearchViewModel: PageViewModel
    {
        /// <summary>
        /// 제목 검색
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 모든 기간 검색
        /// </summary>
        public bool AllRange { get; set; } = true;

        public int TermType { get; set; } = 0;

        /// <summary>
        /// 검색 시작일 
        /// </summary>
        public DateTime FromDate { get; set; } = DateTime.Now.AddDays(-7).Date;

        /// <summary>
        /// 검색 종료일
        /// </summary>
        public DateTime ToDate { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// 노출 여부
        /// </summary>
        public string DisplayYN { get; set; } = "";

        public IEnumerable<SelectListItem> DisplayYNs
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "전체", Value = "" },
                        new SelectListItem { Text = "노출", Value = "Y" },
                        new SelectListItem { Text = "미노출", Value = "N" }
                    }, "Value", "Text");
            }
        }

        /// <summary>
        /// 검색 결과
        /// </summary>
        public List<NoticeItem> DataModel { get; set; }
        /// <summary>
        /// 데이터
        /// </summary>
        public class NoticeItem
        {
            public int Board_ID { get; set; }
            public string Title { get; set; }
            public int? Hits { get; set; }
            public string StatusName { get; set; }

            public DateTime? Regist_DateTime { get; set; }
            public DateTime? Update_DateTime { get; set; }

            public string Update_User { get; set; }

        }
        public string ReturnUrl { get; set; }

        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(Keyword), Keyword },
                    { nameof(AllRange), AllRange.ToString() },
                    { nameof(FromDate), FromDate.ToString("yyyy-MM-dd") },
                    { nameof(ToDate), ToDate.ToString("yyyy-MM-dd") },
                    { nameof(DisplayYN), DisplayYN }
                };
                return routeall;
            }
        }
    }

    public class NoticeViewModel
    {
        public int Board_ID { get; set; }
        public bool Top_YN { get; set; }
        public bool Display_YN { get; set; }

        [Required(ErrorMessage = "제목을 입력하세요.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "내용을 입력하세요.")]
        public string Content { get; set; }
        public string ReturnUrl { get; set; }
    }
}
