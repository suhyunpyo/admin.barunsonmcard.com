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
    public class PolicyController : BaseController
    {
        public PolicyController(ILogger<PolicyController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        { }

        public async Task<IActionResult> Index(PolicySearchViewModel model)
        {
            model.RouteAction = "Index";
            model.RouteController = "Policy";           

            var query = from a in _barunsonDb.TB_PolicyInfos                                                
                        where (string.IsNullOrEmpty(model.Keyword) || a.Title.Contains(model.Keyword) || a.Contents.Contains(model.Keyword)) &&                        
                        (string.IsNullOrEmpty(model.PolicyDiv) || a.PolicyDiv == model.PolicyDiv)
                        orderby a.Seq descending
                        select new PolicyViewModel
                        {
                            Seq = a.Seq,
                            Title = a.Title,
                            Contents = a.Contents,
                            PolicyDiv = a.PolicyDiv,
                            StartDate = a.StartDate,
                            EndDate = a.EndDate,
                            RegDate = a.RegDate,
                            AdminName = a.AdminName
                        };

            //총 아이템 수
            model.Count = await query.CountAsync();
            model.DataModel = await query.Skip(model.PageFrom).Take(model.PageSize).ToListAsync();

            return View(model);
        }

        public IActionResult Add(string returnUrl)
        {
            var model = new PolicyViewModel();
            model.Seq = 0;
            //model.PolicyDiv = "P";
            model.StartDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.EndDate ="2099-12-31";
            return View("Edit", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var query = from a in _barunsonDb.TB_PolicyInfos
                        where a.Seq == id 
                        select a;

            var item = await query.FirstAsync();

            var model = new PolicyViewModel
            {
                Seq = item.Seq,
                Title = item.Title,
                Contents = item.Contents,
                PolicyDiv = item.PolicyDiv,
                AdminName = item.AdminName,
                StartDate = item.StartDate,                
                EndDate = item.EndDate
            };            

            return View("Edit", model);
        }

        public async Task<IActionResult> Save(PolicyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currUser = "";
                var now = DateTime.Now;

                if (User.Identity.IsAuthenticated)
                {
                    currUser = User.Identity.Name;
                }

                TB_PolicyInfo item = null;
                if (model.Seq == 0)
                {
                    item = new TB_PolicyInfo();
                    item.PolicyDiv = model.PolicyDiv;
                    item.Title = model.Title;
                    item.Contents = System.Web.HttpUtility.UrlDecode(model.Contents);
                    item.RegDate = now;
                    item.StartDate = model.StartDate;
                    item.EndDate = model.EndDate;
                    item.AdminName = currUser;

                    _barunsonDb.TB_PolicyInfos.Add(item);
                }
                else
                {
                    item = await (from a in _barunsonDb.TB_PolicyInfos
                                  where a.Seq == model.Seq
                                  select a).FirstAsync();

                    item.PolicyDiv = model.PolicyDiv;
                    item.Title = model.Title;
                    item.Contents = System.Web.HttpUtility.UrlDecode(model.Contents);                    
                    item.StartDate = model.StartDate;
                    item.EndDate = model.EndDate;
                    item.AdminName = currUser;
                }                

                await _barunsonDb.SaveChangesAsync();
                
                return LocalRedirect("/Policy");
            }
            
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            if (id > 0)
            {
                var item = await (from a in _barunsonDb.TB_PolicyInfos
                                  where a.Seq == id
                                  select a).FirstAsync();
                _barunsonDb.TB_PolicyInfos.Remove(item);

                await _barunsonDb.SaveChangesAsync();

                result.status = true;
                result.message = "";
            }
            return Json(result);
        }
    }
}
