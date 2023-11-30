using Barunn.MobileInvitation.Common.Models;
using System;
using System.Collections.Generic;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 페이지 메뉴 모델
    /// </summary>
    public class NavMenuModel
    {
        /// <summary>
        /// 메뉴 ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Controller Action name
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Controller Name
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 메뉴 명
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 상위 메뉴 ID, 최상위는 0
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 선택된 메뉴 여부
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 메뉴에 표시 여부
        /// </summary>
        public bool IsDisplay { get; set; } 

        /// <summary>
        /// 간단한 스크립트 실행
        /// 링크동작 하지 않음
        /// </summary>
        public string AlertText { get; set; }

        /// <summary>
        /// 메뉴 허용 권한
        /// 기본값 없음. 전체 허용
        /// </summary>
        public AdminRole AllowRole { get; set; } = AdminRole.None;
        /// <summary>
        /// 메뉴 접근 권한 여부
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public bool HasRole(string userRole)
        {
            AdminRole _userRole;
            if (!string.IsNullOrWhiteSpace(userRole) && Enum.TryParse(userRole, true, out _userRole))
            {
                return HasRole(_userRole);
            }
            else
                return false;
        }
        /// <summary>
        /// 메뉴 접근 권한 여부
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public bool HasRole(AdminRole userRole)
        {
            if (AllowRole == AdminRole.None || userRole.HasFlag(AdminRole.Admin))
                return true;
            else
                return (AllowRole & userRole) != 0;
        }

        public Dictionary<string, string> RouteData { get; set; }

        public int MenuPosition { get; set; } = 1;
        public NavMenuModel(int id, string area, string action, string controller, string title, int parentId, AdminRole allowRole = AdminRole.None, bool isDisplay = true, string alert = null, Dictionary<string, string> routeData = null)
        {
            Id = id;
            Area = area;
            Action = action;
            Controller = controller;
            Title = title;
            ParentId = parentId;
            AllowRole = allowRole;
            IsDisplay = isDisplay;
            AlertText = alert;
            IsSelected = false;
            RouteData = routeData;
        }
    }
}
