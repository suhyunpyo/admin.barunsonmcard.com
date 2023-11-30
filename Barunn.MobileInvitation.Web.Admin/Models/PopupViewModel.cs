using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class PopupSearchViewModel : PageViewModel
    {
        public IEnumerable<SelectListItem> Popup_Type_Codes { get; set; }
        /// <summary>
        /// 검색 필터 - 상태코드
        /// </summary>
        public string Search_Status { get; set; } = "";

        /// <summary>
        /// 검색 필터 - 상태코드 목록
        /// </summary>
        public List<SelectListItem> Statuses
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                                    new SelectListItem { Text = "전체", Value = "" },
                                    new SelectListItem { Text = "진행", Value = "Active" },
                                    new SelectListItem { Text = "예약", Value = "Reservation" },
                                    new SelectListItem { Text = "종료", Value = "Closed" },
                    }, "Value", "Text").ToList();
            }
        }
        public List<PopupItem> DataModel { get; set; }
        public class PopupItem
        {
            public int Popup_ID { get; set; }
            public string Status { get; set; }
            public string Popup_Title { get; set; }

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
                    { nameof(Search_Status), Search_Status },

                };
                return routeall;
            }
        }

    }

    public class PopupViewModel
    {
        public int Popup_ID { get; set; }
        public string Popup_Title { get; set; }
        public bool Popup_PC_YN { get; set; }
        public bool Popup_Mobile_YN { get; set; }
        public int Popup_Location_Top { get; set; }
        public int Popup_Location_Left { get; set; }
        public int Popup_Height { get; set; }
        public int Popup_Width { get; set; }

        public List<PopupItem> PopupItems { get; set; } = new List<PopupItem>();

        public class PopupItem
        {
            public Guid itemId { get; set; }
            public int Popup_Item_ID { get; set; }
            public string Popup_Type_Code { get; set; }
            public string Image_URL { get; set; }
            public Uri Image_AbsoluteURL { get; set; }
            public string Link_URL { get; set; }
            public bool Period_Type_Code { get; set; }
            public string Period_Type_ValidMessage { get; set; }
            public DateTime? Start_Date { get; set; } = DateTime.Now.Date;
            public int? Start_Time { get; set; } = 0;
            public DateTime? End_Date { get; set; } = DateTime.Now.Date;
            public int? End_Time { get; set; } = 0;
            public string Status { get; set; }
        }
        /// <summary>
        /// 기간 구분 값 - 무제한
        /// </summary>
        public string Period_Type_TrueValue = "PTC02";

        /// <summary>
        /// 시간 목록 - Select
        /// </summary>
        public IEnumerable<SelectListItem> PeriodTimes
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
        /// Popup Type Code - Select - PC or Mobile
        /// </summary>
        public IEnumerable<SelectListItem> Popup_Type_Codes { get; set; }
        public string Selected_Popup_Type_Codes { get; set; }
        public string ReturnUrl { get; set; }
    }
}
