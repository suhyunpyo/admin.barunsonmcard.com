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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    [Authorize]
    public class BannerController : BaseController
    {
        private readonly IStaticContentHelper _staticContent;

        public BannerController(ILogger<BannerController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb,
            IStaticContentHelper staticContent)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _staticContent = staticContent;
        }

        #region 배너 관리

        /// <summary>
        /// 배너 목록 조회
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(BannerSearchViewModel model)
        {

            model.ReturnUrl = Request.GetEncodedPathAndQuery();
            model.RouteAction = "Index";
            model.RouteController = "Banner";
            model.Banner_Categories = await GetBannerCategoriesAsync(true);

            var bannerQueyr = from a in _barunsonDb.TB_Banners
                              join b in _barunsonDb.TB_Banner_Categories on a.Banner_Category_ID equals b.Banner_Category_ID
                              where (model.Search_Banner_Category_ID == 0 || a.Banner_Category_ID == model.Search_Banner_Category_ID)
                              orderby a.Regist_DateTime descending
                              select new BannerSearchViewModel.BannerItem
                              {
                                  Banner_ID = a.Banner_ID,
                                  Banner_Category_ID = b.Banner_Category_ID,
                                  Banner_Category_Name = b.Banner_Category_Name,
                                  Banner_PC_YN = a.Banner_PC_YN == "Y",
                                  Banner_Mobile_YN = a.Banner_Mobile_YN == "Y",
                                  Banner_Name = a.Banner_Name,
                                  Regist_DateTime = a.Regist_DateTime,
                                  Update_DateTime = a.Update_DateTime
                              };

            model.Count = await bannerQueyr.CountAsync();
            model.DataModel = await bannerQueyr.Skip(model.PageFrom).Take(model.PageSize).ToListAsync();

            return View(model);
        }
        /// <summary>
        /// 신규 배너 등록
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Register(string returnUrl)
        {
            var model = new BannerViewModel();
            model.ReturnUrl = returnUrl ?? Url.Action("Index", "Banner");

            model.Banner_ID = 0; //신규
            await InitBannerViewModelAsync( model);

            model.BannerItems = new List<BannerViewModel.BannerItem>();
            foreach (var typeCode in model.Banner_Type_Codes)
            {
                model.BannerItems.Add(new BannerViewModel.BannerItem
                {
                    itemId = Guid.NewGuid(),
                    Banner_Item_ID = 0,
                    Banner_Type_Code = typeCode.Value,
                    Sort = 1
                });
            }
            return View("Edit", model);
        }

        /// <summary>
        /// 베너 수정
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id, string returnUrl)
        {
            var model = new BannerViewModel();
            model.ReturnUrl = returnUrl ?? Url.Action("Index", "Banner");

            await InitBannerViewModelAsync(model);

            var query = from a in _barunsonDb.TB_Banners
                        where a.Banner_ID == id
                        select a;
            var item = await query.FirstOrDefaultAsync();
            if (item == null)
                return RedirectToAction("index");

            model.Banner_ID = item.Banner_ID;
            model.Banner_Category_ID = item.Banner_Category_ID;
            model.Banner_PC_YN = item.Banner_PC_YN == "Y";
            model.Banner_Mobile_YN = item.Banner_Mobile_YN == "Y";
            model.Banner_Name = item.Banner_Name;

            var bannerItemsQuery = from a in _barunsonDb.TB_Banner_Items
                                   where a.Banner_ID == model.Banner_ID
                                   select a;
            var bannerItems = await bannerItemsQuery.ToListAsync();

            model.BannerItems = new List<BannerViewModel.BannerItem>();
            var now = DateTime.Now;

            foreach (var bannerItem in bannerItems)
            {
                var newItem = new BannerViewModel.BannerItem
                {
                    itemId = Guid.NewGuid(),
                    Banner_Item_ID = bannerItem.Banner_Item_ID,
                    Banner_Type_Code = bannerItem.Banner_Type_Code,
                    Image_URL = bannerItem.Image_URL,
                    Image_AbsoluteURL = string.IsNullOrEmpty(bannerItem.Image_URL) ? null : _staticContent.AbsoluteUri(bannerItem.Image_URL),
                    Image_URL2 = bannerItem.Image_URL2,
                    Image_AbsoluteURL2 = string.IsNullOrEmpty(bannerItem.Image_URL2) ? null : _staticContent.AbsoluteUri(bannerItem.Image_URL2),
                    Deadline_Type_Code = bannerItem.Deadline_Type_Code == model.Deadline_Type_TrueValue,
                    Start_Date = string.IsNullOrEmpty(bannerItem.Start_Date) ? (DateTime?)null : DateTime.Parse(bannerItem.Start_Date),
                    Start_Time = string.IsNullOrEmpty(bannerItem.Start_Time) ? (int?)null : int.Parse(bannerItem.Start_Time),
                    End_Date = string.IsNullOrEmpty(bannerItem.End_Date) ? (DateTime?)null : DateTime.Parse(bannerItem.End_Date),
                    End_Time = string.IsNullOrEmpty(bannerItem.End_Time) ? (int?)null : int.Parse(bannerItem.End_Time),
                    Link_URL = bannerItem.Link_URL,
                    NewPage_YN = bannerItem.NewPage_YN == "Y",
                    Click_Count = bannerItem.Click_Count,
                    Sort = bannerItem.Sort.Value,
                    Banner_Main_Description = bannerItem.Banner_Main_Description,
                    Banner_Add_Description = bannerItem.Banner_Add_Description,
                    Status = ""
                };

                if (!newItem.Deadline_Type_Code && newItem.Start_Date.HasValue && newItem.Start_Time.HasValue && newItem.End_Date.HasValue && newItem.End_Time.HasValue)
                {
                    var startDatetime = newItem.Start_Date.Value.AddHours(newItem.Start_Time.Value);
                    var endDatetime = newItem.End_Date.Value.AddHours(newItem.End_Time.Value);

                    if (startDatetime > now)
                        newItem.Status = "예약";
                    else if (startDatetime <= now && endDatetime > now)
                        newItem.Status = "진행";
                    else if (endDatetime < now)
                        newItem.Status = "종료";
                }
                else if (newItem.Deadline_Type_Code)
                {
                    newItem.Status = "진행";
                }
                model.BannerItems.Add(newItem);
            }

            return View("Edit", model);
        }

        /// <summary>
        /// 배너 저장
        /// </summary>
        /// <param name="model"></param>
        /// <param name="handler"></param>
        /// <param name="handlerValue"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Save(BannerViewModel model, string handler, string handlerValue, Guid? itemId)
        {
            await InitBannerViewModelAsync(model);
            if (itemId.HasValue)
            {
                var item = model.BannerItems.FirstOrDefault(m => m.itemId == itemId.Value);
                if (item != null)
                {
                    var curSort = item.Sort;
                    switch (handler)
                    {
                        case "AddItem": //Item 추가
                            var nextitems = model.BannerItems.Where(m => m.Banner_Type_Code == handlerValue && m.Sort > curSort).OrderBy(m => m.Sort);
                            foreach (var nitem in nextitems)
                                nitem.Sort++;

                            model.BannerItems.Add(new BannerViewModel.BannerItem
                            {
                                itemId = Guid.NewGuid(),
                                Banner_Item_ID = 0,
                                Banner_Type_Code = handlerValue,
                                Sort = curSort + 1
                            });
                            BannerItemSort(ref model);
                            break;
                        case "RemoveItem":  //Item 제거
                            model.BannerItems.Remove(item);
                            BannerItemSort(ref model);
                            break;
                        case "UpItem":      //Item 정렬 위로
                            var preItem = model.BannerItems.OrderByDescending(m => m.Sort).FirstOrDefault(m => m.Sort < item.Sort && m.Banner_Type_Code == item.Banner_Type_Code);
                            if (preItem != null)
                            {
                                item.Sort = preItem.Sort;
                                preItem.Sort = curSort;

                                BannerItemSort(ref model);
                            }
                            break;
                        case "DownItem":    //Item 정렬 아래로
                            var nextItem = model.BannerItems.OrderBy(m => m.Sort).FirstOrDefault(m => m.Sort > item.Sort && m.Banner_Type_Code == item.Banner_Type_Code);
                            if (nextItem != null)
                            {
                                item.Sort = nextItem.Sort;
                                nextItem.Sort = curSort;

                                BannerItemSort(ref model);
                            }
                            break;
                        
                        default:
                            break;
                    }
                }
            }

            if(handler == "Save" && ModelState.IsValid)
            {
                if (!model.Banner_PC_YN && !model.Banner_Mobile_YN)
                    ModelState.AddModelError(nameof(model.Banner_PC_YN), "구분을 선택하세요.");

                if (model.Banner_Category_ID == 0)
                    ModelState.AddModelError(nameof(model.Banner_Category_ID), "배너 분류를 선택하세요.");

                if (string.IsNullOrEmpty(model.Banner_Name))
                    ModelState.AddModelError(nameof(model.Banner_Name), "제목을 입력하세요.");


                foreach (var item in model.BannerItems)
                {
                    //무제한이 아니면 날짜 입력
                    if (!item.Deadline_Type_Code)
                    {
                        if (item.Start_Date == null || item.End_Date == null)
                        {
                            ModelState.AddModelError($"BannerItems[{item.itemId}].Deadline_Type_Code", "기간을 입력하거나 무제한을 선택 하세요.");
                            item.Deadline_Type_ValidMessage = "기간을 입력하거나 무제한을 선택 하세요.";
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    //파일 이동. temp -> banner
                    foreach(var imgFile in model.BannerItems)
                    {
                        if (!string.IsNullOrEmpty(imgFile.Image_URL))
                        {
                            var tempPath = imgFile.Image_URL;
                            imgFile.Image_URL = await FileMoveToProductAsync(tempPath);
                        }
                        if (!string.IsNullOrEmpty(imgFile.Image_URL2))
                        {
                            var tempPath2 = imgFile.Image_URL2;
                            imgFile.Image_URL2 = await FileMoveToProductAsync(tempPath2);
                        }
                    }

                    var now = DateTime.Now;
                    var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    var currUser = CurrentUserId();

                    using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                    TB_Banner banner = null;
                    List<TB_Banner_Item> banner_Items = new List<TB_Banner_Item>();
                    if (model.Banner_ID == 0)
                    {
                        banner = new TB_Banner();
						banner.Banner_Name = model.Banner_Name;
						banner.Banner_Category_ID = model.Banner_Category_ID;
                        banner.Regist_DateTime = now;
                        banner.Regist_IP = ipAddr;
                        banner.Regist_User_ID = currUser;

                        _barunsonDb.TB_Banners.Add(banner);
                        await _barunsonDb.SaveChangesAsync();

                        model.Banner_ID = banner.Banner_ID;

                    }
                    else
                    {
                        banner = await (from a in _barunsonDb.TB_Banners
                                        where a.Banner_ID == model.Banner_ID
                                        select a).FirstAsync();

                        banner_Items = await (from a in _barunsonDb.TB_Banner_Items
                                              where a.Banner_ID == model.Banner_ID
                                              select a).ToListAsync();
                    }
                    banner.Banner_Name = model.Banner_Name;
                    banner.Banner_PC_YN = model.Banner_PC_YN ? "Y" : "M";
                    banner.Banner_Mobile_YN = model.Banner_Mobile_YN ? "Y" : "N";
                    banner.Update_DateTime = now;
                    banner.Update_IP = ipAddr;
                    banner.Update_User_ID = currUser;

                    var deleteImageFiles = new List<string>();
                    //Delete banner item
                    var removeItems = banner_Items.Where(m => !model.BannerItems.Any(x => x.Banner_Item_ID == m.Banner_Item_ID));
                    _barunsonDb.TB_Banner_Items.RemoveRange(removeItems);

                    deleteImageFiles.AddRange(removeItems.Where(m => !string.IsNullOrEmpty(m.Image_URL)).Select(m => m.Image_URL));
                    deleteImageFiles.AddRange(removeItems.Where(m => !string.IsNullOrEmpty(m.Image_URL2)).Select(m => m.Image_URL2));
                    

                    //Update item
                    foreach (var updateItem in banner_Items.Where(m => model.BannerItems.Any(x => x.Banner_Item_ID == m.Banner_Item_ID)))
                    {
                        var modelItem = model.BannerItems.First(m => m.Banner_Item_ID == updateItem.Banner_Item_ID);
                        //이미지 파일 변경시 기존 파일 삭제
                        if (!string.IsNullOrEmpty(updateItem.Image_URL) && updateItem.Image_URL != modelItem.Image_URL)
                            deleteImageFiles.Add(updateItem.Image_URL);
                        if (!string.IsNullOrEmpty(updateItem.Image_URL2) && updateItem.Image_URL2 != modelItem.Image_URL2)
                            deleteImageFiles.Add(updateItem.Image_URL2);

                        updateItem.Image_URL = modelItem.Image_URL;
                        updateItem.Image_URL2 = modelItem.Image_URL2;
                        updateItem.Deadline_Type_Code = modelItem.Deadline_Type_Code ? model.Deadline_Type_TrueValue : "";
                        updateItem.Start_Date = modelItem.Deadline_Type_Code ? "" : modelItem.Start_Date?.ToString("yyyy-MM-dd");
                        updateItem.Start_Time = modelItem.Deadline_Type_Code ? "" : modelItem.Start_Time?.ToString("D2");
                        updateItem.End_Date = modelItem.Deadline_Type_Code ? "" : modelItem.End_Date?.ToString("yyyy-MM-dd");
                        updateItem.End_Time = modelItem.Deadline_Type_Code ? "" : modelItem.End_Time?.ToString("D2");
                        updateItem.Link_URL = modelItem.Link_URL;
                        updateItem.NewPage_YN = modelItem.NewPage_YN ? "Y" : "N";
                        updateItem.Sort = modelItem.Sort;
                        updateItem.Banner_Main_Description = modelItem.Banner_Main_Description?.Trim();
                        updateItem.Banner_Add_Description = modelItem.Banner_Add_Description?.Trim();
                        updateItem.Update_User_ID = currUser;
                        updateItem.Update_DateTime = now;
                        updateItem.Update_IP = ipAddr;
                    }

                    //Add New
                    foreach (var newItem in model.BannerItems.Where(m => m.Banner_Item_ID == 0).OrderBy(x => x.Sort))
                    {
                        _barunsonDb.TB_Banner_Items.Add(new TB_Banner_Item
                        {
                            Banner_ID = model.Banner_ID,
                            Banner_Type_Code = newItem.Banner_Type_Code,
                            Image_URL = newItem.Image_URL,
                            Image_URL2 = newItem.Image_URL2,
                            Deadline_Type_Code = newItem.Deadline_Type_Code ? model.Deadline_Type_TrueValue : "",
                            Start_Date = newItem.Deadline_Type_Code ? "" : newItem.Start_Date?.ToString("yyyy-MM-dd"),
                            Start_Time = newItem.Deadline_Type_Code ? "" : newItem.Start_Time?.ToString("D2"),
                            End_Date = newItem.Deadline_Type_Code ? "" : newItem.End_Date?.ToString("yyyy-MM-dd"),
                            End_Time = newItem.Deadline_Type_Code ? "" : newItem.End_Time?.ToString("D2"),
                            Link_URL = newItem.Link_URL,
                            NewPage_YN = newItem.NewPage_YN ? "Y" : "N",
                            Sort = newItem.Sort,
                            Banner_Main_Description = newItem.Banner_Main_Description?.Trim(),
                            Banner_Add_Description = newItem.Banner_Add_Description?.Trim(),

                            Regist_User_ID = currUser,
                            Regist_DateTime = now,
                            Regist_IP = ipAddr,
                            Update_User_ID = currUser,
                            Update_DateTime = now,
                            Update_IP = ipAddr,
                        });
                    }

                    await _barunsonDb.SaveChangesAsync();

                    await transaction.CommitAsync();

                    //이미지 파일 삭제
                    foreach (var delFile in deleteImageFiles)
                        await _staticContent.DeleteFileAsync(delFile);

                    if (string.IsNullOrEmpty(model.ReturnUrl))
                        return RedirectToAction("Index");
                    else
                        return LocalRedirect(model.ReturnUrl);
                }
            }

            return View("Edit", model);
        }

        /// <summary>
        /// 배너 삭제
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            if (id > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                var query = from a in _barunsonDb.TB_Banners
                            where a.Banner_ID == id
                            select a;
                var item = await query.FirstAsync();

                var bannerItemsQuery = from a in _barunsonDb.TB_Banner_Items
                                       where a.Banner_ID == id
                                       select a;
                var bannerItems = await bannerItemsQuery.ToListAsync();

                var deleteImageFiles = new List<string>();
                deleteImageFiles.AddRange(bannerItems.Where(m => !string.IsNullOrEmpty(m.Image_URL)).Select(m => m.Image_URL));
                deleteImageFiles.AddRange(bannerItems.Where(m => !string.IsNullOrEmpty(m.Image_URL2)).Select(m => m.Image_URL2));

                _barunsonDb.TB_Banner_Items.RemoveRange(bannerItems);
                _barunsonDb.TB_Banners.Remove(item);

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

        /// <summary>
        /// 이미지 업로드
        /// </summary>
        /// <param name="files"></param>
        /// <param name="TemporaryId"></param>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string itemId, string itemnum)
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
                    return Json(new { status = true, path = message, absoluteUri = absoluteUri, itemId=itemId, itemnum = itemnum, message = "" });
                }
                else
                {
                    return Json(new { status = false, path = "", absoluteUri = "", itemId = itemId, itemnum = itemnum, message = "File not found!" });
                }
                
            }
            catch (Exception e)
            {
                return Json(new { status = false, path = "", absoluteUri = "", itemId = itemId, itemnum = itemnum, message = e.Message });
            }
        }
        #endregion

        #region 배너 분류
        
        public async Task<IActionResult> Category()
        {
            var query = from a in _barunsonDb.TB_Banner_Categories
                        orderby a.Banner_Category_Name
                        select a;
            List<TB_Banner_Category> model = await query.ToListAsync();
            
            return View(model);
        }

        public async Task<IActionResult> AddCategory(string CateName)
        {
            if (!string.IsNullOrEmpty(CateName))
            {
                var now = DateTime.Now;
                var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currUser = CurrentUserId();

                TB_Banner_Category item = new TB_Banner_Category
                {
                    Banner_Category_Name = CateName,
                    Regist_DateTime = now,
                    Regist_IP = ipAddr,
                    Regist_User_ID = currUser,
                    Update_DateTime = now,
                    Update_IP = ipAddr,
                    Update_User_ID = currUser
                };
                _barunsonDb.TB_Banner_Categories.Add(item);

                await _barunsonDb.SaveChangesAsync();
            }
            return RedirectToAction("Category");
        }
        public async Task<IActionResult> UpdateCategory(int id, string CateName)
        {
            if (id > 0 && !string.IsNullOrEmpty(CateName))
            {
                var item = await (from a in _barunsonDb.TB_Banner_Categories where a.Banner_Category_ID == id select a).FirstAsync();

                if (item.Banner_Category_Name != CateName)
                {
                    var now = DateTime.Now;
                    var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    var currUser = CurrentUserId();

                    item.Banner_Category_Name = CateName;
                    item.Update_DateTime = now;
                    item.Update_IP = ipAddr;
                    item.Update_User_ID = currUser;
                }
                await _barunsonDb.SaveChangesAsync();
            }
            return RedirectToAction("Category");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id > 0 )
            {
                var deleteImageFiles = new List<string>();

                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                var item = await (from a in _barunsonDb.TB_Banner_Categories where a.Banner_Category_ID == id select a).FirstAsync();
                _barunsonDb.TB_Banner_Categories.Remove(item);

                var banners = await (from a in _barunsonDb.TB_Banners
                                     where a.Banner_Category_ID == id
                                     select a).ToListAsync();
                foreach(var banner in banners)
                {
                    var bannerItems = await (from a in _barunsonDb.TB_Banner_Items
                                             where a.Banner_ID == banner.Banner_ID
                                             select a).ToListAsync();
                    deleteImageFiles.AddRange(bannerItems.Where(m => !string.IsNullOrEmpty(m.Image_URL)).Select(m => m.Image_URL));
                    deleteImageFiles.AddRange(bannerItems.Where(m => !string.IsNullOrEmpty(m.Image_URL2)).Select(m => m.Image_URL2));

                    _barunsonDb.TB_Banner_Items.RemoveRange(bannerItems);
                    _barunsonDb.TB_Banners.Remove(banner);
                }

                await _barunsonDb.SaveChangesAsync();

                await transaction.CommitAsync();

                //이미지 파일 삭제
                foreach (var delFile in deleteImageFiles)
                    await _staticContent.DeleteFileAsync(delFile);

            }
            return RedirectToAction("Category");
        }
        #endregion

        #region Private functions
        private async Task InitBannerViewModelAsync(BannerViewModel model)
        {
            model.Banner_Categories = await GetBannerCategoriesAsync();
            model.Banner_Type_Codes = await GetSelectListsCommonCodesAsync("Banner_Type_Code");
            model.Selected_Banner_Type_Codes = model.Banner_Type_Codes.First().Value;
        }
        private void BannerItemSort(ref BannerViewModel model)
        {
            foreach(var typecode in model.Banner_Type_Codes)
            {
                var orderItems = model.BannerItems.Where(m => m.Banner_Type_Code == typecode.Value).OrderBy(m => m.Sort);
                var i = 1;
                foreach (var item in orderItems)
                {
                    item.Sort = i;
                    i++;
                }
            }
        }
        protected async Task<IEnumerable<SelectListItem>> GetBannerCategoriesAsync(bool addAll = false)
        {
            var bannerCategoryQuery = from a in _barunsonDb.TB_Banner_Categories
                                      orderby a.Banner_Category_Name
                                      select new { a.Banner_Category_ID, a.Banner_Category_Name };

            var bannerCategoryItem = (await bannerCategoryQuery.ToListAsync())
                .Select(m => new SelectListItem { Text = m.Banner_Category_Name, Value = m.Banner_Category_ID.ToString() })
                .ToList();

            if (addAll)
                bannerCategoryItem.Insert(0, new SelectListItem { Text = "선택하세요", Value = "0" });

            return new SelectList(bannerCategoryItem, "Value", "Text");
        }

        /// <summary>
        /// 임시 파일을 운영환경으로 이동
        /// </summary>
        private async Task<string> FileMoveToProductAsync(string tempPath)
        {
            string result = tempPath;

            if (!string.IsNullOrEmpty(tempPath) && tempPath.Contains("img/temp/") && _staticContent.ExistsFile(tempPath))
            {
                var bannerPath = tempPath.Replace("img/temp/", "img/banners/");
                var (status, message) = await _staticContent.MoveToFileAsync(tempPath, bannerPath);
               
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
