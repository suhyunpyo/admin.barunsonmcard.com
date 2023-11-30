using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class CategoryViewModel
    {
        /// <summary>
        /// 메인 카테고리 - Select Box
        /// </summary>
        public IEnumerable<SelectListItem> RootCategories { get; set; }

        public List<CategoryItem> Items { get; set; }

        public class CategoryItem
        {
            public int CategoryId { get; set; }
            /// <summary>
            /// 상위 카테고리 ID
            /// </summary>
            public int? ParentCategoryId { get; set; }
            /// <summary>
            /// 이름
            /// </summary>
            public string CategoryName { get; set; }
            /// <summary>
            /// 이름 - 전체경로
            /// </summary>
            public string CategoryFullName { get; set; }
            /// <summary>
            /// 이미지 상대 경로
            /// </summary>
            public string CategoryNamePCUrl { get; set; }
            /// <summary>
            /// 이미지 절대 경로
            /// </summary>
            public string CategoryNamePCAbsoluteUrl { get; set; }
            /// <summary>
            /// 분류_구분_코드(메인 CTC01 / 카테고리 CTC02)
            /// </summary>
            public string CategoryTypeCode { get; set; }
            /// <summary>
            /// 분류_명_구분_코드(CNC01 텍스트 / CNC02   이미지)
            /// </summary>
            public string CategoryNameTypeCode { get; set; }
            /// <summary>
            /// 진열 여부
            /// </summary>
            public bool DisplayYN { get; set; }
            /// <summary>
            /// 진열 순서
            /// </summary>
            public int Sort { get; set; }

            /// <summary>
            /// 포함 제품 수
            /// </summary>
            public int ProdcutCount { get; set; }

            public string Message { get; set; }
        }
    }
}
