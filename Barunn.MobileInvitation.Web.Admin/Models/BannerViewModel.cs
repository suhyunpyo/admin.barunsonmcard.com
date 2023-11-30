using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 배너 검색 모델
    /// </summary>
    public class BannerSearchViewModel: PageViewModel
    {
        /// <summary>
        /// 검색 필터 - 카테고리
        /// </summary>
        public int Search_Banner_Category_ID { get; set; }

        /// <summary>
        /// 검색 필터 - 카테고리 목록
        /// </summary>
        public IEnumerable<SelectListItem> Banner_Categories { get; set; }

        /// <summary>
        /// 베터 검색 결과
        /// </summary>
        public List<BannerItem> DataModel { get; set; }
        /// <summary>
        /// 배너 데이터
        /// </summary>
        public class BannerItem
        {
            public int Banner_ID { get; set; }
            public int Banner_Category_ID { get; set; }
            public string Banner_Category_Name { get; set; }
            public bool Banner_PC_YN { get; set; }
            public bool Banner_Mobile_YN { get; set; }

            public string Banner_Name { get; set; }

            public DateTime? Regist_DateTime { get; set; }
            public DateTime? Update_DateTime { get; set; }
        }

        public string ReturnUrl { get; set; }

        public override Dictionary<string, string> RouteData
        {
            get
            {
                var routeall = new Dictionary<string, string>
                {
                    { nameof(Search_Banner_Category_ID), Search_Banner_Category_ID.ToString() },
                    
                };
                return routeall;
            }
        }
    }

    /// <summary>
    /// Banner 등록/수정 모델
    /// </summary>
    public class BannerViewModel
    {
        /// <summary>
        /// Banner ID
        /// </summary>
        public int Banner_ID { get; set; }

        /// <summary>
        /// Banner Category
        /// </summary>
        public int Banner_Category_ID { get; set; }

        /// <summary>
        /// Banner Categories - Select
        /// </summary>
        public IEnumerable<SelectListItem> Banner_Categories { get; set; }

        /// <summary>
        /// PC 표시 여부
        /// </summary>
        public bool Banner_PC_YN { get; set; }

        /// <summary>
        /// 모바일 표시 여부
        /// </summary>
        public bool Banner_Mobile_YN { get; set; }

        /// <summary>
        /// 배너 명
        /// </summary>
        public string Banner_Name { get; set; }

        /// <summary>
        /// 배너 아이템 목록ㅉ
        /// </summary>
        public List<BannerItem> BannerItems { get; set; } = new List<BannerItem>();

        public class BannerItem
        {
            public Guid itemId { get; set; }
            public int Banner_Item_ID { get; set; }
            public string Banner_Type_Code { get; set; }
            public string Image_URL { get; set; }
            public Uri Image_AbsoluteURL { get; set; }
            public bool Deadline_Type_Code { get; set; }
            public string Deadline_Type_ValidMessage { get; set; }

            public DateTime? Start_Date { get; set; } = DateTime.Now.Date;
            public int? Start_Time { get; set; } = 0;
            public DateTime? End_Date { get; set; } = DateTime.Now.Date;
            public int? End_Time { get; set; } = 0;
            public string Link_URL { get; set; }
            public bool NewPage_YN { get; set; }
            public int? Click_Count { get; set; }
            public int Sort { get; set; }

            public string Banner_Main_Description { get; set; }
            public string Banner_Add_Description { get; set; }
            public string Image_URL2 { get; set; }
            public Uri Image_AbsoluteURL2 { get; set; }
            public string Status { get; set; }
        }
        /// <summary>
        /// Banner Type Code - Select - PC or Mobile
        /// </summary>
        public IEnumerable<SelectListItem> Banner_Type_Codes { get; set; }
        public string Selected_Banner_Type_Codes { get; set; }
        /// <summary>
        /// 시간 목록 - Select
        /// </summary>
        public IEnumerable<SelectListItem> DeadlineTimes
        {
            get
            {
                return new SelectList(Enumerable.Range(0, 24).Select(x =>
                     new SelectListItem()
                     {
                         Text = x.ToString("D2"),
                         Value = x.ToString()
                     }), "Value", "Text");
            }
        }
        /// <summary>
        /// 기간 구분 값 - 무제한
        /// </summary>
        public string Deadline_Type_TrueValue = "PTC02";

        public string ReturnUrl { get; set; }
    }
}
