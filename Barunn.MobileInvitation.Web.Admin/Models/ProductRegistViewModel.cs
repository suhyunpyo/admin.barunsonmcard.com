using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class ProductRegistViewModel
    {
        /// <summary>
        /// 신규 등록 여부
        /// </summary>
        public bool isNew => (Product_ID == 0);

        /// <summary>
        /// 제품 ID (Key)
        /// </summary>
        public int Product_ID { get; set; }

        /// <summary>
        /// 파일 저장소 폴더용 임시 ID
        /// </summary>
        public string TemporaryId { get { return "temp"; } }

        #region 기본정보
        /// <summary>
        /// 브랜드
        /// </summary>
        public string ProductBarnd { get; set; }
        /// <summary>
        /// 브렌드 목록 드롭다운
        /// </summary>
        public IEnumerable<SelectListItem> ProductBarnds { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary>
        public string Original_Product_Code { get; set; }
        /// <summary>
        /// 제품코드 (프론트 표시)
        /// </summary>

        public string Product_Code { get; set; }

        /// <summary>
        /// 제품 분류
        /// </summary>
        public string ProductCategoryCode { get; set; }
        /// <summary>
        /// 제품 분류 목록 라디오버튼
        /// </summary>
        public IEnumerable<SelectListItem> ProductCategoryCodes { get; set; }

        /// <summary>
        /// 포토형
        /// </summary>
        public bool Photo_YN { get; set; }

        /// <summary>
        /// 제품 이름
        /// </summary>
        [Required(ErrorMessage = "제품이름을 입력하세요.")]
        public string Product_Name { get; set; }

        /// <summary>
        /// 제품 설명
        /// </summary>
        public string Product_Description { get; set; }

        public string SetCard_URL { get; set; }
        public string SetCard_Mobile_URL { get; set; }
        public bool SetCard_Display_YN { get; set; }

        #endregion

        #region 판매/진열
        /// <summary>
        /// 판매가격
        /// </summary>
        [Required(ErrorMessage = "가격을 입력하세요.")]
        [Range(1, int.MaxValue, ErrorMessage = "가격을 잘못 입력 하였습니다.")]
        public int? Price { get; set; }

        /// <summary>
        /// 진열 상태
        /// </summary>
        public string Display_YN { get; set; } = "N";
        /// <summary>
        /// 진열 상태 목록 라디어버튼
        /// </summary>
        public List<SelectListItem> Display_YNs
        {
            get
            {
                return new List<SelectListItem>
                    {
                        new SelectListItem { Selected = false, Text = "진열함", Value = "Y"},
                        new SelectListItem { Selected = false, Text = "진열안함", Value ="N"},
                    };
            }
        }

        public List<int> SelectedProductCategories { get; set; } = new List<int>();
        
        /// <summary>
        /// 코드 카테고리 목록 - 리스트 박스
        /// 코드상의 모든 항목 표시
        /// code, 상위 > 하위 형식
        /// </summary>
        public List<SelectListItem> CodeProductCategories { get; set; } = new List<SelectListItem>();
        public List<ProductCategoryCode> ProductCategories { get; set; }

        /// <summary>
        /// 메인 카테고리 - 체크박스
        /// </summary>
        public List<SelectListItem> Main_Categories { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// 아이콘 - 체크박스
        /// </summary>
        public List<SelectListItem> Main_Icons { get; set; } = new List<SelectListItem>();
        #endregion

        #region 제품 상세 이미지
        /// <summary>
        /// Image 분류 코드
        /// </summary>
        public IEnumerable<SelectListItem> Image_Category_Codes { get; set; }

        /// <summary>
        /// 제품 이미지 
        /// </summary>
        public class ProductImage
        {
            public int Image_ID { get; set; }
            public string Image_URL { get; set; }
            public string Image_Type_Code { get; set; }
            public Uri ImageAbsoluteUri { get; set; }

            public DateTime UpdateDateTime { get; set; }
        }
        /// <summary>
        /// 제품이미지 목록
        /// </summary>
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        #endregion

        #region Templete
        /// <summary>
        /// 템플릿 ID
        /// </summary>
        public int Template_ID { get; set; }


        public string Main_Image_URL { get; set; }
        public Uri Main_ImageAbsoluteUri { get; set; }
        public string Preview_Image_URL { get; set; }
        public Uri Preview_ImageAbsoluteUri { get; set; }

        public TemplateViewModel TemplateModel { get; set; }
        #endregion

        public bool IsDelete { get; set; } = false;
    }

    public class ProductCategoryCode
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public List<ProductCategoryCode> Childs { get; set; }
    }
}
