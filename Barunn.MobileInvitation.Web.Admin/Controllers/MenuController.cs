using Barunn.MobileInvitation.Common.Helpers;
using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Dac.Contexts;
using Barunn.MobileInvitation.Dac.Models.Barunson;
using Barunn.MobileInvitation.Web.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    [Authorize]
    public class MenuController : BaseController
    {
        private readonly IStaticContentHelper _staticContent;
        public MenuController(ILogger<MenuController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb,
            IStaticContentHelper staticContent)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _staticContent = staticContent;
        }

        /// <summary>
        /// 메뉴 목록
        /// </summary>
        /// <param name="id">Menu_Type_Code</param>
        /// <param name="menuid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string id, int? menuid, string message)
        {
            ViewBag.id = id;
            var model = new MenuViewModel();
            ViewBag.MenuTitle = id == "MTC01" ? "GNB 관리" : "푸터 관리"; 
            var query = from a in _barunsonDb.TB_Common_Menus
                        where a.Menu_Type_Code == id
                        orderby a.Sort
                        select new MenuViewModel.MenuItem
                        {
                            MenuID = a.Menu_ID,
                            ParentMenuID = a.Parent_Menu_ID,
                            MenuName = a.Menu_Name,
                            MenuTypeCode = a.Menu_Type_Code,
                            MenuURL = a.Menu_URL,
                            Sort = a.Sort.Value,
                            BoldYN = false,
                            ImageURL = a.Image_URL
                        };

            model.Items = await query.ToListAsync();
            foreach (var item in model.Items)
            {
                if (item.MenuName.Contains("<strong>"))
                {
                    item.BoldYN = true;
                    item.MenuName = item.MenuName.Replace("<strong>", "").Replace("</strong>", "");
                }
                if (!string.IsNullOrEmpty(item.ImageURL))
                    item.ImageAbsoluteUrl = _staticContent.AbsoluteUri(item.ImageURL).AbsoluteUri;
                else
                    item.ImageAbsoluteUrl = "/img/noimg_208x58.gif";

                if (menuid.HasValue && !string.IsNullOrEmpty(message))
                {
                    if (item.MenuID == menuid.Value)
                        item.Message = message;
                }
            }
            if (menuid.HasValue && menuid.Value == 0 && !string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }
            if (menuid.HasValue && menuid.Value == -1 && !string.IsNullOrEmpty(message))
            {
                ViewBag.info = message;
            }
            return View(model);
        }
        public async Task<IActionResult> ManuAdd(string id, MenuViewModel.MenuItem item)
        {
            var menuid = 0;
            var message = "";

            if (string.IsNullOrEmpty(item.MenuName))
            {
                message = "분류명을 입력하세요.";
                return RedirectToAction("Index", new { menuid, message });
            }
            
            var menuItem = new TB_Common_Menu();

            if (!string.IsNullOrEmpty(item.ImageURL))
            {
                //파일 이동. temp -> category
                var tempPath = item.ImageURL;
                item.ImageURL = await FileMoveToProductAsync(tempPath);

                menuItem.Image_URL = item.ImageURL;
            }
            var now = DateTime.Now;
            var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var currUser = CurrentUserId();

            var name = item.MenuName;
            if (item.BoldYN)
                name = $"<strong>{item.MenuName}</strong>";

            menuItem.Parent_Menu_ID = null;
            menuItem.Menu_Name = name;
            menuItem.Menu_Type_Code = id;
            menuItem.Menu_URL = item.MenuURL;
            menuItem.Menu_Step = 1;
            menuItem.Sort = item.Sort;
            menuItem.Display_YN = "Y";
            menuItem.Regist_DateTime = now;
            menuItem.Regist_IP = ipAddr;
            menuItem.Regist_User_ID = currUser;
            menuItem.Update_DateTime = now;
            menuItem.Update_IP = ipAddr;
            menuItem.Update_User_ID = currUser;

            _barunsonDb.TB_Common_Menus.Add(menuItem);
            await _barunsonDb.SaveChangesAsync();

            return RedirectToAction("Index", new { id, menuid, message });
        }

        public async Task<IActionResult> MenuUpdate(string id, MenuViewModel model)
        {
            var trans = await _barunsonDb.Database.BeginTransactionAsync();
            try
            {
                var now = DateTime.Now;
                var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currUser = CurrentUserId();
                bool isSave = true;
                var deleteImageFiles = new List<string>();

                var query = from a in _barunsonDb.TB_Common_Menus
                            where a.Menu_Type_Code == id
                            select a;

                var dbitems = await query.ToListAsync();
                foreach (var item in model.Items)
                {
                    if (string.IsNullOrEmpty(item.MenuName))
                    {
                        isSave = false;
                        item.Message = "분류 이름이 없습니다.";
                    }
                    var menuItem = dbitems.First(m => m.Menu_ID == item.MenuID);

                    if (string.IsNullOrEmpty(item.ImageURL) && !string.IsNullOrEmpty(menuItem.Image_URL))
                    {
                        deleteImageFiles.Add(menuItem.Image_URL);
                        menuItem.Image_URL = null;
                    }
                    else if (!string.IsNullOrEmpty(item.ImageURL) && item.ImageURL != menuItem.Image_URL)
                    {
                        //파일 이동. temp -> menu
                        var tempPath = item.ImageURL;
                        item.ImageURL = await FileMoveToProductAsync(tempPath);

                        if (!string.IsNullOrEmpty(menuItem.Image_URL))
                        {
                            deleteImageFiles.Add(menuItem.Image_URL);
                        }
                        menuItem.Image_URL = item.ImageURL;
                    }

                    var name = item.MenuName;
                    if (item.BoldYN)
                        name = $"<strong>{item.MenuName}</strong>";

                    menuItem.Menu_Name = name;
                    menuItem.Menu_URL = item.MenuURL;
                    menuItem.Sort = item.Sort;
                    menuItem.Update_DateTime = now;
                    menuItem.Update_IP = ipAddr;
                    menuItem.Update_User_ID = currUser;
                }

                if (isSave)
                {
                    await _barunsonDb.SaveChangesAsync();
                    await trans.CommitAsync();
                    //이미지 파일 삭제
                    foreach (var delFile in deleteImageFiles)
                        await _staticContent.DeleteFileAsync(delFile);

                    return RedirectToAction("Index", new { id = id});
                }
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
                ViewBag.info = e.Message;
            }
            return View("Index", model);
        }

        public async Task<IActionResult> MenuDelete(string id, [FromBody] List<int> ids)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            if (ids.Count > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                //Current
                var query = from a in _barunsonDb.TB_Common_Menus
                            where a.Menu_Type_Code == id &&
                                ids.Contains(a.Menu_ID)
                            select a;

                var menuItem = await query.ToListAsync();

                var deleteImageFiles = new List<string>();
                deleteImageFiles.AddRange(menuItem.Where(m => !string.IsNullOrEmpty(m.Image_URL)).Select(m => m.Image_URL));

                _barunsonDb.TB_Common_Menus.RemoveRange(menuItem);

                await _barunsonDb.SaveChangesAsync();

                await transaction.CommitAsync();

                //이미지 파일 삭제
                foreach (var delFile in deleteImageFiles)
                    await _staticContent.DeleteFileAsync(delFile);

                result.status = true;
                result.message = "";
            }

            return Json(result);
        }


        #region Private fuctions

        /// <summary>
        /// 이미지 업로드
        /// </summary>
        /// <param name="files"></param>
        /// <param name="TemporaryId"></param>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string itemId)
        {
            try
            {
                if (file.Length > 0)
                {

                    var ext = Path.GetExtension(file.FileName).ToLower();
                    var filename = Guid.NewGuid().ToString() + ext;
                    var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "img", "temp", filename);
                    var (status, message) = await _staticContent.UploadFileAsync(file, rPath);

                    var absoluteUri = _staticContent.AbsoluteUri(message);
                    return Json(new { status = true, path = message, absoluteUri = absoluteUri, itemId = itemId, message = "" });
                }
                else
                {
                    return Json(new { status = false, path = "", absoluteUri = "", itemId = itemId, message = "File not found!" });
                }

            }
            catch (Exception e)
            {
                return Json(new { status = false, path = "", absoluteUri = "", itemId = itemId, message = e.Message });
            }
        }

        /// <summary>
        /// 임시 파일을 운영환경으로 이동
        /// </summary>
        private async Task<string> FileMoveToProductAsync(string tempPath)
        {
            string result = tempPath;

            if (!string.IsNullOrEmpty(tempPath) && tempPath.Contains("img/temp/") && _staticContent.ExistsFile(tempPath))
            {
                var catePath = tempPath.Replace("img/temp/", "img/gnb/");
                var (status, message) = await _staticContent.MoveToFileAsync(tempPath, catePath);
                if (status == true)
                {
                    if (!message.StartsWith("/"))
                        message = $"/{message}";
                    result = message;
                }
            }
            return result;
        }
        #endregion
    }
}
