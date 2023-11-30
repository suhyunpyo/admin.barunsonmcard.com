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
    public class CategoryController : BaseController
    {
        private readonly IStaticContentHelper _staticContent;
        public CategoryController(ILogger<CategoryController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb,
            IStaticContentHelper staticContent)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _staticContent = staticContent;
        }

        #region Main Category
       
        /// <summary>
        /// 메인 카테고리 관리
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MainIndex(int? cateid, string message)
        {
            var model = new CategoryViewModel();
            model.RootCategories = await GetSelectListsMainCategoryAsync();

            var query = from a in _barunsonDb.TB_Categories
                        join b in _barunsonDb.TB_Product_Categories on a.Category_ID equals b.Category_ID into gb
                        from b in gb.DefaultIfEmpty()
                        where a.Category_Type_Code == "CTC01"
                        group b by new
                        {
                            a.Category_ID,
                            a.Parent_Category_ID,
                            a.Category_Name,
                            a.Category_Name_PC_URL,
                            a.Category_Type_Code,
                            a.Category_Name_Type_Code,
                            a.Display_YN,
                            a.Sort
                        } into g
                        orderby g.Key.Sort ascending
                        select new CategoryViewModel.CategoryItem
                        {
                            CategoryId = g.Key.Category_ID,
                            ParentCategoryId = g.Key.Parent_Category_ID,
                            CategoryName = g.Key.Category_Name,
                            CategoryNamePCUrl = g.Key.Category_Name_PC_URL,
                            CategoryTypeCode = g.Key.Category_Type_Code,
                            CategoryNameTypeCode = g.Key.Category_Name_Type_Code,
                            DisplayYN = g.Key.Display_YN == "Y",
                            Sort = g.Key.Sort.Value,
                            ProdcutCount = g.Count(m => m != null)
                        };
            model.Items = await query.ToListAsync();

            foreach (var item in model.Items)
            {
                if (item.ParentCategoryId.HasValue)
                    item.CategoryFullName = $"{model.RootCategories.First(m => m.Value == item.ParentCategoryId.Value.ToString()).Text} > {item.CategoryName}";
                else
                    item.CategoryFullName = item.CategoryName;

                if (!string.IsNullOrEmpty(item.CategoryNamePCUrl))
                    item.CategoryNamePCAbsoluteUrl = _staticContent.AbsoluteUri(item.CategoryNamePCUrl).AbsoluteUri;
                else
                    item.CategoryNamePCAbsoluteUrl = "/img/noimg_208x58.gif";

                if (cateid.HasValue && !string.IsNullOrEmpty(message))
                {
                    if (item.CategoryId == cateid.Value)
                        item.Message = message;
                }

            }
            if (cateid.HasValue && cateid.Value == 0 && !string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }
            if (cateid.HasValue && cateid.Value == -1 && !string.IsNullOrEmpty(message))
            {
                ViewBag.info = message;
            }
            return View("MainIndex", model);
        }

        public async Task<IActionResult> MainUpdate(CategoryViewModel model)
        {
            var trans = await _barunsonDb.Database.BeginTransactionAsync();
            //분류_구분_코드(메인 CTC01 / 카테고리 CTC02)
            try
            {
                var now = DateTime.Now;
                var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currUser = CurrentUserId();
                bool isSave = true;
                var deleteImageFiles = new List<string>();

                var query = from a in _barunsonDb.TB_Categories
                            where a.Category_Type_Code == "CTC01"
                            select a;

                var dbitems = await query.ToListAsync();

                foreach (var item in model.Items)
                {
                    if (string.IsNullOrEmpty(item.CategoryName))
                    {
                        isSave = false;
                        item.Message = "분류 이름이 없습니다.";
                    }

                    var cateItem = dbitems.First(m => m.Category_ID == item.CategoryId);

                    if (string.IsNullOrEmpty(item.CategoryNamePCUrl) && !string.IsNullOrEmpty(cateItem.Category_Name_PC_URL))
                    {
                        deleteImageFiles.Add(cateItem.Category_Name_PC_URL);
                        cateItem.Category_Name_PC_URL = null;
                    }
                    else if (!string.IsNullOrEmpty(item.CategoryNamePCUrl) && item.CategoryNamePCUrl != cateItem.Category_Name_PC_URL)
                    {
                        //파일 이동. temp -> category
                        var tempPath = item.CategoryNamePCUrl;
                        item.CategoryNamePCUrl = await FileMoveToProductAsync(tempPath);

                        if (!string.IsNullOrEmpty(cateItem.Category_Name_PC_URL))
                        {
                            deleteImageFiles.Add(cateItem.Category_Name_PC_URL);
                        }
                        cateItem.Category_Name_PC_URL = item.CategoryNamePCUrl;
                    }

                    cateItem.Category_Name = item.CategoryName;
                    cateItem.Category_Name_PC = item.CategoryName;
                    cateItem.Category_Name_Type_Code = string.IsNullOrEmpty(cateItem.Category_Name_PC_URL) ? "CNC01" : "CNC02";  //분류_명_구분_코드(CNC01 텍스트 / CNC02   이미지)
                    cateItem.Sort = item.Sort;
                    cateItem.Display_YN = (item.DisplayYN) ? "Y" : "N";
                    cateItem.Update_DateTime = now;
                    cateItem.Update_IP = ipAddr;
                    cateItem.Update_User_ID = currUser;
                }

                if (isSave)
                {
                    await _barunsonDb.SaveChangesAsync();
                    await trans.CommitAsync();
                    //이미지 파일 삭제
                    foreach (var delFile in deleteImageFiles)
                        await _staticContent.DeleteFileAsync(delFile);

                    return RedirectToAction("MainIndex", new { cateid = -1, message = "저장 되었습니다." });
                }
                
            }
            catch(Exception e)
            {
                await trans.RollbackAsync();
                ViewBag.info = e.Message;
            }
            return View("MainIndex", model);
        }

        public async Task<IActionResult> MainAdd(CategoryViewModel.CategoryItem item)
        {
            var cateId = 0;
            var message = "";

            if (string.IsNullOrEmpty(item.CategoryName))
            {
                message = "분류 이름을 입력하세요.";
                return RedirectToAction("MainIndex", new { cateId, message });
            }

            //분류_구분_코드(메인 CTC01 / 카테고리 CTC02)

            var cateItem = new TB_Category();

            if (!string.IsNullOrEmpty(item.CategoryNamePCUrl))
            {
                //파일 이동. temp -> category
                var tempPath = item.CategoryNamePCUrl;
                item.CategoryNamePCUrl = await FileMoveToProductAsync(tempPath);

                cateItem.Category_Name_PC_URL = item.CategoryNamePCUrl;
            }
            var now = DateTime.Now;
            var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var currUser = CurrentUserId();

            cateItem.Parent_Category_ID = null;
            cateItem.Category_Type_Code = "CTC01";
            cateItem.Category_Name = item.CategoryName;
            cateItem.Category_Name_PC = item.CategoryName;
            cateItem.Category_Name_Type_Code = string.IsNullOrEmpty(cateItem.Category_Name_PC_URL) ? "CNC01" : "CNC02";  //분류_명_구분_코드(CNC01 텍스트 / CNC02   이미지)
            cateItem.Category_Step = 1;
            cateItem.Sort = item.Sort;
            cateItem.Display_YN = (item.DisplayYN) ? "Y" : "N";
            cateItem.Regist_DateTime = now;
            cateItem.Regist_IP = ipAddr;
            cateItem.Regist_User_ID = currUser;
            cateItem.Update_DateTime = now;
            cateItem.Update_IP = ipAddr;
            cateItem.Update_User_ID = currUser;


            _barunsonDb.TB_Categories.Add(cateItem);
            await _barunsonDb.SaveChangesAsync();

            return RedirectToAction("MainIndex", new { cateId, message });
        }

        [HttpPost]
        public async Task<IActionResult> MainDelete([FromBody] List<int> ids)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            if (ids.Count > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();


                var query = from a in _barunsonDb.TB_Categories
                            where ids.Contains(a.Category_ID)
                            select a;

                var cateItems = await query.ToListAsync();

                var prodItemsQuery = from a in _barunsonDb.TB_Product_Categories
                                     where ids.Contains(a.Category_ID)
                                     select a;
                var prodItems = await prodItemsQuery.ToListAsync();

                var deleteImageFiles = new List<string>();
                deleteImageFiles.AddRange(cateItems.Where(m => !string.IsNullOrEmpty(m.Category_Name_PC_URL)).Select(m => m.Category_Name_PC_URL));
                
                _barunsonDb.TB_Product_Categories.RemoveRange(prodItems);
                _barunsonDb.TB_Categories.RemoveRange(cateItems);

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

        #endregion

        #region Category

        /// <summary>
        /// 카테고리 관리
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? cateid, string message)
        {
            var model = new CategoryViewModel();
            model.RootCategories = await GetSelectListsCategoryAsync(null, true, "", "[대분류로 추가]" );

            var query = from a in _barunsonDb.TB_Categories
                        join b in _barunsonDb.TB_Product_Categories on a.Category_ID equals b.Category_ID into gb
                        from b in gb.DefaultIfEmpty()
                        where a.Category_Type_Code == "CTC02"
                        group b by new
                        {
                            a.Category_ID,
                            a.Parent_Category_ID,
                            a.Category_Name,
                            a.Category_Name_PC_URL,
                            a.Category_Type_Code,
                            a.Category_Name_Type_Code,
                            a.Display_YN,
                            a.Sort
                        } into g
                        orderby g.Key.Sort ascending
                        select new CategoryViewModel.CategoryItem
                        {
                            CategoryId = g.Key.Category_ID,
                            ParentCategoryId = g.Key.Parent_Category_ID,
                            CategoryName = g.Key.Category_Name,
                            CategoryNamePCUrl = g.Key.Category_Name_PC_URL,
                            CategoryTypeCode = g.Key.Category_Type_Code,
                            CategoryNameTypeCode = g.Key.Category_Name_Type_Code,
                            DisplayYN = g.Key.Display_YN == "Y",
                            Sort = g.Key.Sort.Value,
                            ProdcutCount = g.Count(m => m != null)
                        };
            model.Items = await query.ToListAsync();
            foreach (var item in model.Items)
            {
                if (item.ParentCategoryId.HasValue)
                    item.CategoryFullName = $"{model.RootCategories.First(m => m.Value == item.ParentCategoryId.Value.ToString()).Text} > {item.CategoryName}";
                else
                    item.CategoryFullName = item.CategoryName;

                if (!string.IsNullOrEmpty(item.CategoryNamePCUrl))
                    item.CategoryNamePCAbsoluteUrl = _staticContent.AbsoluteUri(item.CategoryNamePCUrl).AbsoluteUri;
                else
                    item.CategoryNamePCAbsoluteUrl = "/img/noimg_208x58.gif";

                if (cateid.HasValue && !string.IsNullOrEmpty(message))
                {
                    if (item.CategoryId == cateid.Value)
                        item.Message = message;
                }

            }
            if (cateid.HasValue && cateid.Value == 0 && !string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }
            if (cateid.HasValue && cateid.Value == -1 && !string.IsNullOrEmpty(message))
            {
                ViewBag.info = message;
            }
            return View("Index", model);
        }

        public async Task<IActionResult> CateUpdate(CategoryViewModel model)
        {
            var trans = await _barunsonDb.Database.BeginTransactionAsync();
            try
            {
                var now = DateTime.Now;
                var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currUser = CurrentUserId();
                bool isSave = true;
                var deleteImageFiles = new List<string>();

                var query = from a in _barunsonDb.TB_Categories
                            where a.Category_Type_Code == "CTC02"
                            select a;

                var dbitems = await query.ToListAsync();
                foreach (var item in model.Items)
                {
                    if (string.IsNullOrEmpty(item.CategoryName))
                    {
                        isSave = false;
                        item.Message = "분류 이름이 없습니다.";
                    }
                    var cateItem = dbitems.First(m => m.Category_ID == item.CategoryId);

                    if (string.IsNullOrEmpty(item.CategoryNamePCUrl) && !string.IsNullOrEmpty(cateItem.Category_Name_PC_URL))
                    {
                        deleteImageFiles.Add(cateItem.Category_Name_PC_URL);
                        cateItem.Category_Name_PC_URL = null;
                    }
                    else if (!string.IsNullOrEmpty(item.CategoryNamePCUrl) && item.CategoryNamePCUrl != cateItem.Category_Name_PC_URL)
                    {
                        //파일 이동. temp -> category
                        var tempPath = item.CategoryNamePCUrl;
                        item.CategoryNamePCUrl = await FileMoveToProductAsync(tempPath);

                        if (!string.IsNullOrEmpty(cateItem.Category_Name_PC_URL))
                        {
                            deleteImageFiles.Add(cateItem.Category_Name_PC_URL);
                        }
                        cateItem.Category_Name_PC_URL = item.CategoryNamePCUrl;
                    }

                    cateItem.Category_Name = item.CategoryName;
                    cateItem.Category_Name_PC = item.CategoryName;
                    cateItem.Category_Name_Type_Code = string.IsNullOrEmpty(cateItem.Category_Name_PC_URL) ? "CNC01" : "CNC02";  //분류_명_구분_코드(CNC01 텍스트 / CNC02   이미지)
                    cateItem.Sort = item.Sort;
                    cateItem.Display_YN = (item.DisplayYN) ? "Y" : "N";
                    cateItem.Update_DateTime = now;
                    cateItem.Update_IP = ipAddr;
                    cateItem.Update_User_ID = currUser;
                }

                if (isSave)
                {
                    await _barunsonDb.SaveChangesAsync();
                    await trans.CommitAsync();
                    //이미지 파일 삭제
                    foreach (var delFile in deleteImageFiles)
                        await _staticContent.DeleteFileAsync(delFile);

                    return RedirectToAction("Index", new { cateid = -1, message = "저장 되었습니다." });
                }
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
                ViewBag.info = e.Message;
            }
            return View("Index", model);
        }

        public async Task<IActionResult> CateAdd(CategoryViewModel.CategoryItem item)
        {
            var cateId = 0;
            var message = "";

            if (string.IsNullOrEmpty(item.CategoryName))
            {
                message = "분류 이름을 입력하세요.";
                return RedirectToAction("Index", new { cateId, message });
            }

            //분류_구분_코드(메인 CTC01 / 카테고리 CTC02)

            var cateItem = new TB_Category();

            if (!string.IsNullOrEmpty(item.CategoryNamePCUrl))
            {
                //파일 이동. temp -> category
                var tempPath = item.CategoryNamePCUrl;
                item.CategoryNamePCUrl = await FileMoveToProductAsync(tempPath);

                cateItem.Category_Name_PC_URL = item.CategoryNamePCUrl;
            }
            var now = DateTime.Now;
            var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var currUser = CurrentUserId();

            cateItem.Parent_Category_ID = item.ParentCategoryId;
            cateItem.Category_Type_Code = "CTC02";
            cateItem.Category_Name = item.CategoryName;
            cateItem.Category_Name_PC = item.CategoryName;
            cateItem.Category_Name_Type_Code = string.IsNullOrEmpty(cateItem.Category_Name_PC_URL) ? "CNC01" : "CNC02";  //분류_명_구분_코드(CNC01 텍스트 / CNC02   이미지)
            cateItem.Category_Step = item.ParentCategoryId.HasValue ? 2 : 1;
            cateItem.Sort = item.Sort;
            cateItem.Display_YN = (item.DisplayYN) ? "Y" : "N";
            cateItem.Regist_DateTime = now;
            cateItem.Regist_IP = ipAddr;
            cateItem.Regist_User_ID = currUser;
            cateItem.Update_DateTime = now;
            cateItem.Update_IP = ipAddr;
            cateItem.Update_User_ID = currUser;

            _barunsonDb.TB_Categories.Add(cateItem);
            await _barunsonDb.SaveChangesAsync();

            return RedirectToAction("Index", new { cateId, message });
        }

        [HttpPost]
        public async Task<IActionResult> CateDelete([FromBody] List<int> ids)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            if (ids.Count > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                //Current
                var query = from a in _barunsonDb.TB_Categories
                            where ids.Contains(a.Category_ID)
                            select a;

                var cateItems = await query.ToListAsync();
                var allCateIds = new List<int>();
                allCateIds.AddRange(cateItems.Select(m => m.Category_ID));

                //Child if exists
                var childQuery = from a in _barunsonDb.TB_Categories
                                 where allCateIds.Contains(a.Parent_Category_ID.Value)
                                 select a;
                var childItems = await childQuery.ToListAsync();

                allCateIds.AddRange(childItems.Select(m => m.Category_ID));

                //Prodcut cateogry
                var prodItemsQuery = from a in _barunsonDb.TB_Product_Categories
                                     where allCateIds.Contains(a.Category_ID)
                                     select a;
                var prodItems = await prodItemsQuery.ToListAsync();

                var deleteImageFiles = new List<string>();
                deleteImageFiles.AddRange(cateItems.Where(m => !string.IsNullOrEmpty(m.Category_Name_PC_URL)).Select(m => m.Category_Name_PC_URL));
                deleteImageFiles.AddRange(childItems.Where(m => !string.IsNullOrEmpty(m.Category_Name_PC_URL)).Select(m => m.Category_Name_PC_URL));

                _barunsonDb.TB_Product_Categories.RemoveRange(prodItems);
                _barunsonDb.TB_Categories.RemoveRange(childItems);
                _barunsonDb.TB_Categories.RemoveRange(cateItems);

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

        #endregion

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
                    return Json(new { status = true, path = message, absoluteUri = absoluteUri, itemId = itemId,  message = "" });
                }
                else
                {
                    return Json(new { status = false, path = "", absoluteUri = "", itemId = itemId, message = "File not found!" });
                }

            }
            catch (Exception e)
            {
                return Json(new { status = false, path = "", absoluteUri = "", itemId = itemId,  message = e.Message });
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
                var catePath = tempPath.Replace("img/temp/", "img/categories/");
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
