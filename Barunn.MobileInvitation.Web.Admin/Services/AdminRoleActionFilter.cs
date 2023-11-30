using Barunn.MobileInvitation.Common.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace Barunn.MobileInvitation.Web.Admin.Services
{
    /// <summary>
    /// Role 권한에 따른 Action메소드 실행 권한 필터
    /// </summary>
    public class AdminRoleActionFilter : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// 컨톨롤러,액션 메소드의 실행 Role 값.
        /// </summary>
        private AdminRole _allowRole { get; set; }
        public AdminRoleActionFilter()
        {
            _allowRole = AdminRole.None;
        }
        /// <summary>
        /// 컨톨롤러,액션 메소드의 권한 Role 값.
        /// </summary>
        /// <param name="allowRole"></param>
        public AdminRoleActionFilter(AdminRole allowRole)
        {
            _allowRole = allowRole;
        }
        /// <summary>
        /// 컨톨롤러,액션 메소드의 권한 Role 값. 텍스트형식
        /// </summary>
        /// <param name="allowRole"></param>
        public AdminRoleActionFilter(string allowRole)
        {
            _allowRole = AdminRole.None;
            var roleNames = allowRole.Split(',');
            foreach (var roleName in roleNames)
            {
                AdminRole role;
                if (Enum.TryParse(roleName, true, out role))
                {
                    _allowRole |= role;
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            bool allow = false;
            if (_allowRole == AdminRole.None)
                allow = true;
            else if (user != null && user.Identity.IsAuthenticated)
            {
                var claimsIdentity = user.Identity as ClaimsIdentity;
                var roleName = claimsIdentity?.FindFirst(ClaimTypes.Role)?.Value;
                AdminRole userRole;
                if (roleName != null && Enum.TryParse(roleName, true, out userRole))
                {
                    //권한 여부 확인
                    allow = (_allowRole & userRole) != 0;
                    //Admin은 무조건 허용
                    if (userRole.HasFlag(AdminRole.Admin))
                        allow = true;
                }

            }
            if (allow)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "PermissionDenied"
                };
            }

        }

    }
}
