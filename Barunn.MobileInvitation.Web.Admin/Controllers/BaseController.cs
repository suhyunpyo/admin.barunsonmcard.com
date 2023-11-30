using Barunn.MobileInvitation.Common.Models;
using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Dac.Contexts;
using Barunn.MobileInvitation.Dac.Models.Barunson;
using Barunn.MobileInvitation.Web.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly BarunnConfig _barunnConfig;
        protected readonly BarunsonContext _barunsonDb;
        protected readonly BarShopContext _barshopDb;

        public BaseController(ILogger logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb)
        {
            _logger = logger;
            _barunnConfig = barunnConfig.Value;
            _barunsonDb = barunsonDb;
            _barshopDb = barshopDb;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //현재 페이지
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            //현재 페이지 메뉴 - 탑메뉴 제외

            var selectedMenus = NavMenuService.Menus.MenuItems
                .Where(m =>
                    actionName.Equals(m.Action, StringComparison.InvariantCultureIgnoreCase) &&
                    controllerName.Equals(m.Controller, StringComparison.InvariantCultureIgnoreCase) &&
                    m.ParentId > 0
                    );
            if (selectedMenus.Count() == 1)
                ViewData["SelectedMenu"] = selectedMenus.First();
            else if (selectedMenus.Count() > 1)
            {
                foreach(var selectedMenu in selectedMenus)
                {
                    foreach(var route in selectedMenu.RouteData)
                    {
                        if (this.ControllerContext.RouteData.Values[route.Key].ToString() == route.Value)
                            ViewData["SelectedMenu"] = selectedMenu;
                    }
                }
            }

            base.OnActionExecuting(context);
        }
        /// <summary>
        /// 로그인 관리자 ID
        /// </summary>
        /// <returns></returns>
        protected string CurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
                return User.Claims.First(m => m.Type == ClaimTypes.NameIdentifier).Value;
            else
                return null;
        }
        /// <summary>
        /// 로그인 관리자 Role
        /// </summary>
        /// <returns></returns>
        public AdminRole CurrentUserRole()
        {
            var userRole = AdminRole.None;
            if (User.Identity.IsAuthenticated)
            {
                var roleName = User.Claims.First(m => m.Type == ClaimTypes.Role).Value;
                if (roleName != null)
                {
                    Enum.TryParse(roleName, true, out userRole);
                }
            }
            return userRole;
        }
        /// <summary>
        /// 로그인 관리자 권한 여부
        /// </summary>
        /// <param name="requiredRole"></param>
        /// <returns></returns>
        public bool CurrentUserHasRole(AdminRole requiredRole)
        {
            var result = false;

            var curRole = CurrentUserRole();
            if (curRole != AdminRole.None)
            {
                result = (requiredRole & curRole) != 0;
            }
            return result;
        }

        /// <summary>
        /// 공통 코드 목록 - 코드 그룹
        /// </summary>
        /// <param name="codeGroup"></param>
        /// <returns></returns>
        protected async Task<List<TB_Common_Code>> GetCommonCodeAsync(string codeGroup)
        {
            var query = from r in _barunsonDb.TB_Common_Codes
                        where r.Code_Group == codeGroup
                        orderby r.Sort
                        select r;

            return await query.ToListAsync();
        }
        /// <summary>
        /// 공통 코드 SelectList 출력
        /// </summary>
        /// <param name="codeGroup"></param>
        /// <param name="addAll">All 추가 여부</param>
        /// <param name="allValue"></param>
        /// <param name="allText"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<SelectListItem>> GetSelectListsCommonCodesAsync(string codeGroup,
            bool addAll = false, string allValue = "", string allText = "전체")
        {
            var codes = await GetCommonCodeAsync(codeGroup);
            var items = codes.Select(m => new SelectListItem { Text = m.Code_Name, Value = m.Code }).ToList();
            if (addAll)
                items.Insert(0, new SelectListItem { Text = allText, Value = allValue });

            return new SelectList(items, "Value", "Text");
        }


        /// <summary>
        /// 카테고리 목록
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <returns></returns>
        protected async Task<List<TB_Category>> GetCategoryAsync(int? parentCategoryId = null, string type = "CTC02")
        {
            var query = from r in _barunsonDb.TB_Categories
                        where r.Category_Type_Code == type && (parentCategoryId == null ? r.Parent_Category_ID == null : r.Parent_Category_ID == parentCategoryId)
                        orderby r.Sort
                        select r;

            return await query.ToListAsync();
        }


        /// <summary>
        /// 카테고리 SelectList 출력
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <param name="addAll"></param>
        /// <param name="allValue"></param>
        /// <param name="allText"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<SelectListItem>> GetSelectListsCategoryAsync(int? parentCategoryId,
            bool addAll = false, string allValue = "", string allText = "전체")
        {
            var codes = await GetCategoryAsync(parentCategoryId);
            var items = codes.Select(m => new SelectListItem 
            { 
                Text = m.Category_Name + (m.Display_YN == "Y" ? "" : "(미진열)"), 
                Value = m.Category_ID.ToString() 
            }).ToList();

            if (addAll)
                items.Insert(0, new SelectListItem { Text = allText, Value = allValue });

            return new SelectList(items, "Value", "Text");
        }
        protected async Task<IEnumerable<SelectListItem>> GetSelectListsMainCategoryAsync(bool addAll = false, string allValue = "", string allText = "전체")
        {
            var codes = await GetCategoryAsync(null, "CTC01");
            var items = codes.Select(m => new SelectListItem
            {
                Text = m.Category_Name + (m.Display_YN == "Y" ? "" : "(미진열)"),
                Value = m.Category_ID.ToString()
            }).ToList();

            if (addAll)
                items.Insert(0, new SelectListItem { Text = allText, Value = allValue });

            return new SelectList(items, "Value", "Text");
        }

        protected string ItemTypeCodeToResourceTypeCode(string itc)
        {
            //type = a.Item_Type_Code == "ITC01" ? "txt" : a.Item_Type_Code == "ITC02" ? "img" : "photo",
            var result = "";
            switch (itc)
            {
                case "ITC01":
                    result = "txt";
                    break;
                case "ITC02":
                    result = "img";
                    break;
                case "ITC03":
                    result = "photo";
                    break;
                case "ITC04":
                    result = "profile";
                    break;
                default:
                    break;
            }
            return result;
        }
        protected string ResourceTypeCodeToItemTypeCode(string rtc)
        {
            //item.type == "txt" ? "ITC01" : item.type == "img" ? "ITC02" : "ITC03";
            var result = "";
            switch (rtc)
            {
                case "txt":
                    result = "ITC01";
                    break;
                case "img":
                    result = "ITC02";
                    break;
                case "photo":
                    result = "ITC03";
                    break;
                case "profile":
                    result = "ITC04";
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// 통계 필터 기준 날짜
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        protected DateTime GetTargetDate(int? year, int? month = null)
        {
            var now = DateTime.Now;

            if (!year.HasValue)
                year = now.Year;
            if (!month.HasValue)
                month = now.Month;

            DateTime targetDate;
            if (!DateTime.TryParse($"{year}-{month}-01", out targetDate))
            {
                //실패시
                targetDate = now;
            }
            return targetDate.Date;
        }

        /// <summary>
        /// DB상의 결혼일및 시간을 날짜형식으로 변환
        /// </summary>
        /// <param name="weddingdate"></param>
        /// <param name="whoure"></param>
        /// <param name="wmin"></param>
        /// <param name="typecode"></param>
        /// <returns></returns>
        protected DateTime? GetWeddingDate(string weddingdate, string whoure, string wmin, string typecode)
        {
            if (string.IsNullOrEmpty(weddingdate))
                return null;

            try
            {
                var wdate = DateTime.Parse(weddingdate);
                if (!string.IsNullOrEmpty(whoure))
                {
                    var h = int.Parse(whoure.Trim());
                    if (typecode == "오후" && h < 12)
                        h += 12;

                    wdate = wdate.AddHours(h);
                }
                if (!string.IsNullOrEmpty(wmin))
                    wdate = wdate.AddMinutes(int.Parse(wmin.Trim()));

                return wdate;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 일자별 유니크 숫자 생성
        /// </summary>
        /// <param name="date"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<int> GetTelegramNoAsync(string date)
        {
            var uniquNum = -1;

            using (var tran = await _barunsonDb.Database.BeginTransactionAsync())
            {
                var q = from a in _barunsonDb.TB_Daily_Uniques
                        where a.Request_Date == date
                        select a.Unique_Number;
                var item = await q.MaxAsync(m => (int?)m);
                if (!item.HasValue)
                    item = 0;

                uniquNum = item.Value + 1;

                var addItem = new TB_Daily_Unique { Request_Date = date, Unique_Number = uniquNum };
                _barunsonDb.TB_Daily_Uniques.Add(addItem);
                await _barunsonDb.SaveChangesAsync();

                await tran.CommitAsync();
            }
            return uniquNum;
        }

    }
}
