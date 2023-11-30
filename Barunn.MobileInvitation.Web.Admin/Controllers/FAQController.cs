using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Dac.Contexts;
using Barunn.MobileInvitation.Dac.Models.Barunson;
using Barunn.MobileInvitation.Web.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    [Authorize]
    public class FAQController : BaseController
    {
        public FAQController(ILogger<FAQController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        { }

        public async Task<IActionResult> Index(FAQSearchViewModel model)
        {
            model.RouteAction = "Index";
            model.RouteController = "FAQ";
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            var query = from a in _barunsonDb.TB_Boards
                        join b in _barunsonDb.TB_Invitation_Admins on a.Update_User_ID equals b.admin_id into user
                        from c in user.DefaultIfEmpty()
                        where a.Board_Category == "F" &&
                        (string.IsNullOrEmpty(model.Keyword) || a.Title.Contains(model.Keyword)) &&
                        (model.AllRange || (a.Regist_DateTime > model.FromDate && a.Regist_DateTime < model.ToDate.AddDays(1))) &&
                        (string.IsNullOrEmpty(model.DisplayYN) || a.Display_YN == model.DisplayYN)
                        orderby a.Regist_DateTime descending
                        select new FAQSearchViewModel.FAXItem
                        {
                            Board_ID = a.Board_ID,
                            Title = a.Title,
                            Hits = a.Hits,
                            StatusName = a.Display_YN == "Y" ? "노출" : "미노출",
                            Regist_DateTime = a.Regist_DateTime,
                            Update_DateTime = a.Update_DateTime,
                            Update_User = c.admin_name
                        };

            //총 아이템 수
            model.Count = await query.CountAsync();
            model.DataModel = await query.Skip(model.PageFrom).Take(model.PageSize).ToListAsync();

            return View(model);
        }
        public IActionResult Add(string returnUrl)
        {
            var model = new FAQViewModel();
            model.ReturnUrl = returnUrl ?? Url.Action("Index", "FAQ");

            return View("Edit", model);
        }
        public async Task<IActionResult> Edit(int id, string returnUrl)
        {
            var query = from a in _barunsonDb.TB_Boards
                        where a.Board_ID == id && a.Board_Category == "F"
                        select a;
            var item = await query.FirstAsync();

            var model = new FAQViewModel
            {
                Board_ID = item.Board_ID,
                Display_YN = item.Display_YN == "Y",
                Title = item.Title,
                Content = item.Content
            };
            model.ReturnUrl = returnUrl ?? Url.Action("Index", "FAQ");

            return View("Edit", model);
        }

        public async Task<IActionResult> Save(FAQViewModel model)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.Now;
                var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currUser = CurrentUserId();

                TB_Board item = null;
                if (model.Board_ID == 0)
                {
                    item = new TB_Board();
                    item.Board_Category = "F";
                    item.Top_YN = "N";

                    item.Hits = 0;
                    item.Regist_DateTime = now;
                    item.Regist_IP = ipAddr;
                    item.Regist_User_ID = currUser;

                    _barunsonDb.TB_Boards.Add(item);
                }
                else
                {
                    item = await (from a in _barunsonDb.TB_Boards
                                  where a.Board_ID == model.Board_ID && a.Board_Category == "F"
                                  select a).FirstAsync();
                }
                item.Display_YN = model.Display_YN ? "Y" : "N";
                item.Title = model.Title;
                item.Content = System.Web.HttpUtility.UrlDecode(model.Content);
                item.Update_DateTime = now;
                item.Update_IP = ipAddr;
                item.Update_User_ID = currUser;

                await _barunsonDb.SaveChangesAsync();

                if (string.IsNullOrEmpty(model.ReturnUrl))
                    return RedirectToAction("Index");
                else
                    return LocalRedirect(model.ReturnUrl);
            }
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            if (id > 0)
            {
                var item = await (from a in _barunsonDb.TB_Boards
                                  where a.Board_ID == id && a.Board_Category == "F"
                                  select a).FirstAsync();
                _barunsonDb.TB_Boards.Remove(item);

                await _barunsonDb.SaveChangesAsync();

                result.status = true;
                result.message = "";
            }
            return Json(result);
        }
    }
}
