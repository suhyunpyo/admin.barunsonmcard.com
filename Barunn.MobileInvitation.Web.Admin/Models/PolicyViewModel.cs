using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class PolicySearchViewModel: PageViewModel
    {
        /// <summary>
        /// 제목 검색
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 노출 여부
        /// </summary>
        public string PolicyDiv { get; set; } = "";

        public IEnumerable<SelectListItem> PolicyDivs
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "전체", Value = "" },
                        new SelectListItem { Text = "개인정보 처리방침", Value = "P" },
                        new SelectListItem { Text = "이용약관", Value = "U" }
                    }, "Value", "Text");
            }
        }

        /// <summary>
        /// 검색 결과
        /// </summary>
        public List<PolicyViewModel> DataModel { get; set; }               

        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(Keyword), Keyword },                    
                    { nameof(PolicyDiv), PolicyDiv }
                };
                return routeall;
            }
        }
    }

    public class PolicyViewModel
    {
        public int Seq { get; set; }
        public string AdminName { get; set; }
        [Required(ErrorMessage = "제목을 입력하세요.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "내용을 입력하세요.")]
        public string Contents { get; set; }
        [Required(ErrorMessage = "섹션을 선택하세요.")]
        public string PolicyDiv { get; set; }
        [Required(ErrorMessage = "시작일을 입력하세요.")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "종료일을 입력하세요.")]
        public string EndDate { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
