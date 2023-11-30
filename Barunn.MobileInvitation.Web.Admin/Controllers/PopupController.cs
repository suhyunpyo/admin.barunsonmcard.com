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
    public class PopupController : BaseController
    {
        private readonly IStaticContentHelper _staticContent;

        public PopupController(ILogger<PopupController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb,
            IStaticContentHelper staticContent)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _staticContent = staticContent;
        }
        #region 팝업 관리
        /// <summary>
        /// 팝업 목록 조회
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(PopupSearchViewModel model)
        {

            model.ReturnUrl = Request.GetEncodedPathAndQuery();
            model.RouteAction = "Index";
            model.RouteController = "Popup";
            model.Popup_Type_Codes = await GetSelectListsCommonCodesAsync("Popup_Type_Code");

            var query = from a in _barunsonDb.TB_Popups
                        join b in _barunsonDb.TB_Popup_Items on a.Popup_ID equals b.Popup_ID
                        orderby a.Regist_DateTime descending
                        select new 
                        {
                            a.Popup_ID,
                            a.Popup_Title,
                            b.Popup_Item_ID,
                            b.Popup_Type_Code,
                            b.Period_Type_Code,
                            b.Start_Date,
                            b.Start_Time,
                            b.End_Date,
                            b.End_Time,
                            a.Regist_DateTime,
                            a.Update_DateTime
                        };
            var dbItems = await query.ToListAsync();

            var viewDatas = new List<PopupSearchViewModel.PopupItem>();
            var NowDate = DateTime.Now;
            foreach (var dbItem in dbItems)
            {
                bool isAdd = false;
                var status = "Error";

                //Period_Type_Code == "PTC02" : 무제한
                if (dbItem.Period_Type_Code == "PTC02")
                    status = "Active";
                else
                {
                    if (!string.IsNullOrEmpty(dbItem.Start_Date) && !string.IsNullOrEmpty(dbItem.Start_Time) 
                        && !string.IsNullOrEmpty(dbItem.End_Date) && !string.IsNullOrEmpty(dbItem.End_Time))
                    {
                        DateTime startDatetime = Convert.ToDateTime(dbItem.Start_Date + " " + dbItem.Start_Time + ":00:00");
                        DateTime endDatetime = Convert.ToDateTime(dbItem.End_Date + " " + dbItem.End_Time + ":00:00");

                        if (startDatetime > NowDate)
                            status = "Reservation";
                        else if (startDatetime <= NowDate && endDatetime > NowDate)
                            status = "Active";
                        else if (endDatetime < NowDate)
                            status = "Closed";
                    }

                }
                if (string.IsNullOrEmpty(model.Search_Status))
                    isAdd = true;
                else
                {
                    if (model.Search_Status == status)
                        isAdd = true;
                }

                if (isAdd)
                {
                    var existsItem = viewDatas.FirstOrDefault(m => m.Popup_ID == dbItem.Popup_ID);
                    if (existsItem == null)
                    {
                        viewDatas.Add(new PopupSearchViewModel.PopupItem
                        {
                            Popup_ID = dbItem.Popup_ID,
                            Popup_Title = dbItem.Popup_Title,
                            Status = $"{model.Popup_Type_Codes.Single(m => m.Value == dbItem.Popup_Type_Code).Text}({model.Statuses.SingleOrDefault(m => m.Value == status)?.Text})",
                            Regist_DateTime = dbItem.Regist_DateTime,
                            Update_DateTime = dbItem.Update_DateTime
                        });
                    }
                    else
                    {
                        existsItem.Status = string.Join(",", existsItem.Status, $"{model.Popup_Type_Codes.Single(m => m.Value == dbItem.Popup_Type_Code).Text}({model.Statuses.SingleOrDefault(m => m.Value == status)?.Text})");
                    }
                }
            }
            model.Count = viewDatas.Count;
            model.DataModel = viewDatas.Skip(model.PageFrom).Take(model.PageSize).ToList();

            return View(model);
        }

        public async Task<IActionResult> Register(string returnUrl)
        {
            var model = new PopupViewModel();
            model.ReturnUrl = returnUrl ?? Url.Action("Index");

            model.Popup_ID = 0; //신규
            await InitViewModelAsync(model);

            return View("Edit", model);
        }

        public async Task<IActionResult> Edit(int id, string returnUrl)
        {
            var model = new PopupViewModel();
            model.ReturnUrl = returnUrl ?? Url.Action("Index");

            await InitViewModelAsync(model);

            var query = from a in _barunsonDb.TB_Popups
                        where a.Popup_ID == id
                        select a;
            var item = await query.FirstOrDefaultAsync();
            if (item == null)
                return RedirectToAction("index");

            model.Popup_ID = item.Popup_ID;
            model.Popup_Title = item.Popup_Title;
            model.Popup_PC_YN = item.Popup_PC_YN == "Y";
            model.Popup_Mobile_YN = item.Popup_Mobile_YN == "Y";
            model.Popup_Location_Top = item.Popup_Location_Top;
            model.Popup_Location_Left = item.Popup_Location_Left;
            model.Popup_Height = item.Popup_Height;
            model.Popup_Width = item.Popup_Width;

            var itemsQuery = from a in _barunsonDb.TB_Popup_Items
                                   where a.Popup_ID == model.Popup_ID
                                   select a;
            var popupItems = await itemsQuery.ToListAsync();

            var now = DateTime.Now;

            foreach (var popupItem in popupItems)
            {
                var pitem = model.PopupItems.First(m => m.Popup_Type_Code == popupItem.Popup_Type_Code);
                
                pitem.Popup_Item_ID = popupItem.Popup_Item_ID;
                pitem.Image_URL = popupItem.Image_URL;
                pitem.Image_AbsoluteURL = string.IsNullOrEmpty(popupItem.Image_URL) ? null : _staticContent.AbsoluteUri(popupItem.Image_URL);
                pitem.Link_URL = popupItem.Link_URL;
                pitem.Period_Type_Code = popupItem.Period_Type_Code == model.Period_Type_TrueValue;
                pitem.Start_Date = string.IsNullOrEmpty(popupItem.Start_Date) ? (DateTime?)null : DateTime.Parse(popupItem.Start_Date);
                pitem.Start_Time = string.IsNullOrEmpty(popupItem.Start_Time) ? (int?)null : int.Parse(popupItem.Start_Time);
                pitem.End_Date = string.IsNullOrEmpty(popupItem.End_Date) ? (DateTime?)null : DateTime.Parse(popupItem.End_Date);
                pitem.End_Time = string.IsNullOrEmpty(popupItem.End_Time) ? (int?)null : int.Parse(popupItem.End_Time);
                

                if (!pitem.Period_Type_Code && pitem.Start_Date.HasValue && pitem.Start_Time.HasValue && pitem.End_Date.HasValue && pitem.End_Time.HasValue)
                {
                    var startDatetime = pitem.Start_Date.Value.AddHours(pitem.Start_Time.Value);
                    var endDatetime = pitem.End_Date.Value.AddHours(pitem.End_Time.Value);

                    if (startDatetime > now)
                        pitem.Status = "예약";
                    else if (startDatetime <= now && endDatetime > now)
                        pitem.Status = "진행";
                    else if (endDatetime < now)
                        pitem.Status = "종료";
                }
                else if (pitem.Period_Type_Code)
                {
                    pitem.Status = "진행";
                }
            }

            return View("Edit", model);
        }

        public async Task<IActionResult> Save(PopupViewModel model)
        {
            await InitViewModelAsync(model);

            if (ModelState.IsValid)
            {
                if (!model.Popup_PC_YN && !model.Popup_Mobile_YN)
                    ModelState.AddModelError(nameof(model.Popup_PC_YN), "구분을 선택하세요.");

                if (string.IsNullOrEmpty(model.Popup_Title))
                    ModelState.AddModelError(nameof(model.Popup_Title), "제목을 입력하세요.");

                foreach (var item in model.PopupItems)
                {
                    //무제한이 아니면 날짜 입력
                    if (!item.Period_Type_Code)
                    {
                        if (item.Start_Date == null || item.End_Date == null)
                        {
                            ModelState.AddModelError($"PopupItems[{item.itemId}].Period_Type_Code", "기간을 입력하거나 무제한을 선택 하세요.");
                            item.Period_Type_ValidMessage = "기간을 입력하거나 무제한을 선택 하세요.";
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    //파일 이동. temp -> popup
                    foreach (var imgFile in model.PopupItems)
                    {
                        if (!string.IsNullOrEmpty(imgFile.Image_URL))
                        {
                            var tempPath = imgFile.Image_URL;
                            imgFile.Image_URL = await FileMoveToProductAsync(tempPath);
                        }
                    }

                    var now = DateTime.Now;
                    var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    var currUser = CurrentUserId();
                    var deleteImageFiles = new List<string>();

                    using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                    TB_Popup popup = null;
                    List<TB_Popup_Item> popup_Items = new List<TB_Popup_Item>();
                    if (model.Popup_ID == 0)
                    {
                        popup = new TB_Popup();

                        popup.Popup_Title = model.Popup_Title;

                        popup.Regist_DateTime = now;
                        popup.Regist_IP = ipAddr;
                        popup.Regist_User_ID = currUser;

                        _barunsonDb.TB_Popups.Add(popup);
                        await _barunsonDb.SaveChangesAsync();

                        model.Popup_ID = popup.Popup_ID;

                        foreach (var newItem in model.PopupItems)
                        {
                            _barunsonDb.TB_Popup_Items.Add(new TB_Popup_Item
                            {
                                Popup_ID = model.Popup_ID,
                                Popup_Type_Code = newItem.Popup_Type_Code,
                                Image_URL = newItem.Image_URL,
                                Period_Type_Code = newItem.Period_Type_Code ? model.Period_Type_TrueValue : "",
                                Start_Date = newItem.Period_Type_Code ? "" : newItem.Start_Date?.ToString("yyyy-MM-dd"),
                                Start_Time = newItem.Period_Type_Code ? "" : newItem.Start_Time?.ToString("D2"),
                                End_Date = newItem.Period_Type_Code ? "" : newItem.End_Date?.ToString("yyyy-MM-dd"),
                                End_Time = newItem.Period_Type_Code ? "" : newItem.End_Time?.ToString("D2"),
                                Link_URL = newItem.Link_URL,
                                Regist_User_ID = currUser,
                                Regist_DateTime = now,
                                Regist_IP = ipAddr,
                                Update_User_ID = currUser,
                                Update_DateTime = now,
                                Update_IP = ipAddr,
                            });
                        }
                    }
                    else
                    {
                        popup = await (from a in _barunsonDb.TB_Popups
                                       where a.Popup_ID == model.Popup_ID
                                       select a).FirstAsync();

                        popup_Items = await (from a in _barunsonDb.TB_Popup_Items
                                             where a.Popup_ID == model.Popup_ID
                                             select a).ToListAsync();


                        //Update item
                        foreach (var updateItem in popup_Items)
                        {
                            var modelItem = model.PopupItems.First(m => m.Popup_Item_ID == updateItem.Popup_Item_ID);
                            //이미지 파일 변경시 기존 파일 삭제
                            if (!string.IsNullOrEmpty(updateItem.Image_URL) && updateItem.Image_URL != modelItem.Image_URL)
                                deleteImageFiles.Add(updateItem.Image_URL);

                            updateItem.Image_URL = modelItem.Image_URL;
                            updateItem.Period_Type_Code = modelItem.Period_Type_Code ? model.Period_Type_TrueValue : "";
                            updateItem.Start_Date = modelItem.Period_Type_Code ? "" : modelItem.Start_Date?.ToString("yyyy-MM-dd");
                            updateItem.Start_Time = modelItem.Period_Type_Code ? "" : modelItem.Start_Time?.ToString("D2");
                            updateItem.End_Date = modelItem.Period_Type_Code ? "" : modelItem.End_Date?.ToString("yyyy-MM-dd");
                            updateItem.End_Time = modelItem.Period_Type_Code ? "" : modelItem.End_Time?.ToString("D2");
                            updateItem.Link_URL = modelItem.Link_URL;
                            updateItem.Update_User_ID = currUser;
                            updateItem.Update_DateTime = now;
                            updateItem.Update_IP = ipAddr;
                        }
                    }
                    popup.Popup_Title = model.Popup_Title;
                    popup.Popup_PC_YN = model.Popup_PC_YN ? "Y" : "M";
                    popup.Popup_Mobile_YN = model.Popup_Mobile_YN ? "Y" : "N";
                    popup.Popup_Location_Top = model.Popup_Location_Top;
                    popup.Popup_Location_Left = model.Popup_Location_Left;
                    popup.Popup_Height = model.Popup_Height;
                    popup.Popup_Width = model.Popup_Width;
                    popup.Update_DateTime = now;
                    popup.Update_IP = ipAddr;
                    popup.Update_User_ID = currUser;
                  
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

        public async Task<IActionResult> Delete(int id)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            if (id > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                var query = from a in _barunsonDb.TB_Popups
                            where a.Popup_ID == id
                            select a;
                var item = await query.FirstAsync();

                var popItemsQuery = from a in _barunsonDb.TB_Popup_Items
                                       where a.Popup_ID == id
                                       select a;
                var popup_Items = await popItemsQuery.ToListAsync();

                var deleteImageFiles = new List<string>();
                deleteImageFiles.AddRange(popup_Items.Where(m => !string.IsNullOrEmpty(m.Image_URL)).Select(m => m.Image_URL));

                _barunsonDb.TB_Popup_Items.RemoveRange(popup_Items);
                _barunsonDb.TB_Popups.Remove(item);

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
                    return Json(new { status = true, path = message, absoluteUri = absoluteUri, itemId = itemId, itemnum = itemnum, message = "" });
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

        #region Private functions
        private async Task InitViewModelAsync(PopupViewModel model)
        {
            model.Popup_Type_Codes = await GetSelectListsCommonCodesAsync("Popup_Type_Code");
            if (string.IsNullOrEmpty(model.Selected_Popup_Type_Codes))
                model.Selected_Popup_Type_Codes = model.Popup_Type_Codes.First().Value;

            if (model.PopupItems == null || model.PopupItems.Count == 0)
            {
                model.PopupItems = new List<PopupViewModel.PopupItem>();
                foreach (var pitem in model.Popup_Type_Codes)
                {
                    model.PopupItems.Add(new PopupViewModel.PopupItem
                    {
                        itemId = Guid.NewGuid(),
                        Popup_Item_ID = 0,
                        Popup_Type_Code = pitem.Value,
                    });
                }
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
                var bannerPath = tempPath.Replace("img/temp/", "img/popup/");
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
