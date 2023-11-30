using Barunn.MobileInvitation.Dac.Models.Barunson;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 상품/템플릿 리스트 및 검색 뷰 모델
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ProductListViewModel : PageViewModel
    {
        public string SearchKind { get; set; } = "";
        public string SearchViewYn { get; set; } = "";
        public string SearchTxt { get; set; } = "";

        public List<SelectListItem> ProductCategoryCodes { get; set; }

        /// <summary>
        /// 브렌드 목록 체크박스
        /// </summary>
        public List<SelectListItem> ProductBarnds { get; set; }

        /// <summary>
        /// 진열 선택
        /// </summary>
        public List<SelectListItem> ViewYns
        {
            get
            {
                return new List<SelectListItem>
                    {
                        new SelectListItem { Selected = true, Text = "전체", Value = ""},
                        new SelectListItem { Selected = false, Text = "진열", Value = "Y"},
                        new SelectListItem { Selected = false, Text = "미진열", Value ="N"},
                    };
            }
        }
        /// <summary>
        /// 데이터 바인딩 모델
        /// </summary>
        public List<TB_Product> Products { get; set; }

        public string ReturnUrl { get; set; }

        public override Dictionary<string, string> RouteData {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(SearchKind), SearchKind },
                    { nameof(SearchViewYn), SearchViewYn },
                    { nameof(SearchTxt), SearchTxt }
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
                return routeall;
            }
        }

    }

    public class CategoryDisplayViewModel
    {
        public string Keyword { get; set; }

        public int Category { get; set; }

        /// <summary>
        /// 메인 카테고리 - Select Box
        /// </summary>
        public List<SelectListItem> Categories { get; set; }

        public List<DisplayItem> DisplayItems { get; set; }
        public List<DisplayItem> UnDisplayItems { get; set; }
        public class DisplayItem
        {
            /// <summary>
            /// 제품 ID (Key)
            /// </summary>
            public int Product_ID { get; set; }

            public string Product_Brand_Code { get; set; }
            /// <summary>
            /// 브랜드 명
            /// </summary>
            public string ProductBarndName { get; set; }

            public int? Category_ID { get; set; }
            public string CategoryName { get; set; }

            /// <summary>
            /// 제품 이름
            /// </summary>
            public string Product_Name { get; set; }

            /// <summary>
            /// 제품코드 (프론트 표시)
            /// </summary>
            public string Product_Code { get; set; }

            /// <summary>
            /// 판매가격
            /// </summary>
            public int Price { get; set; }

            /// <summary>
            /// 진열날짜
            /// </summary>
            public DateTime? Regist_DateTime { get; set; }

            public int PayCount { get; set; }
            public int FreeCount { get; set; }

            /// <summary>
            /// 진열 순서
            /// </summary>
            public int? Sort { get; set; }

            public string Main_Image_URL { get; set; }
            public Uri Main_ImageAbsoluteUri { get; set; }

            public bool DisplayYN { get; set; }
        }
    }

}
