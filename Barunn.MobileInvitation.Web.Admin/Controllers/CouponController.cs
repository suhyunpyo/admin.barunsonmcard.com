﻿using Barunn.MobileInvitation.Common.Models.Configs;
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
    public class CouponController : BaseController
    {
        public CouponController(ILogger<CouponController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
        }

        /// <summary>
        /// 쿠폰 조회
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(CouponSearchViewModel model)
        {
            var query = from c in _barunsonDb.TB_Coupons
                        join p in _barunsonDb.TB_Coupon_Publishes on c.Coupon_ID equals p.Coupon_ID into pg
                        from pub in pg.DefaultIfEmpty()
                        join pmc in _barunsonDb.TB_Common_Codes on new { Code = c.Publish_Method_Code, Code_Group = "Publish_Method_Code" } equals new { pmc.Code, pmc.Code_Group }
                        join dmc in _barunsonDb.TB_Common_Codes on new { Code = c.Discount_Method_Code, Code_Group = "Discount_Method_Code" } equals new { dmc.Code, dmc.Code_Group }
                        where (string.IsNullOrEmpty(model.Keyword) || c.Coupon_Name.Contains(model.Keyword))
                        group pub by new
                        {
                            c.Coupon_ID,
                            c.Coupon_Name,
                            c.Publish_Method_Code,
                            Publish_Method_Name = pmc.Code_Name,
                            c.Discount_Price,
                            c.Discount_Rate,
                            c.Discount_Method_Code,
                            Discount_Method_Name = dmc.Code_Name,
                            c.Regist_DateTime,
                            c.Use_YN

                        } into g
                        orderby g.Key.Regist_DateTime descending
                        select new CouponSearchDataModel
                        {
                            Coupon_ID = g.Key.Coupon_ID,
                            Coupon_Name = g.Key.Coupon_Name,
                            Publish_Method_Code = g.Key.Publish_Method_Code,
                            Publish_Method_Name = g.Key.Publish_Method_Name,
                            Discount_Price = g.Key.Discount_Price,
                            Discount_Rate = g.Key.Discount_Rate,
                            Discount_Method_Code = g.Key.Discount_Method_Code,
                            Discount_Method_Name = g.Key.Discount_Method_Name,
                            Regist_DateTime = g.Key.Regist_DateTime,
                            Publish_count = g.Count(m => m != null),
                            Use_count = g.Sum(m => m.Use_YN == "Y" ? 1 : 0),
                            UnUse_count = g.Sum(m => (m.Use_YN == "N" && m.Retrieve_DateTime == null) ? 1 : 0),
                            Retrive_count = g.Sum(m => (m.Retrieve_DateTime != null) ? 1 : 0),
                            Use_YN = g.Key.Use_YN 
                        };
            //총 아이템 수
            model.Count = await query.CountAsync();
            //페이지 수 만큼 데이터 읽기
            model.DataModel = await query.Skip(model.PageFrom).Take(model.PageSize).ToListAsync();
            model.DataModel.ForEach(m => m.Use_YN_Text = m.Use_YN == "Y" ? "사용" : "중지");

            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "Index";
            model.RouteController = "Coupon";

            return View(model);
        }

        /// <summary>
        /// 쿠폰 삭제
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id, string returnUrl = null)
        {

            var query = from c in _barunsonDb.TB_Coupons
                        where c.Coupon_ID == id
                        select c;
            var items = await query.FirstAsync();
            var capItem = await (from r in _barunsonDb.TB_Coupon_Apply_Products where r.Product_Apply_ID == items.Coupon_Apply_Product_ID.Value select r).FirstOrDefaultAsync();
            var apItems = await (from r in _barunsonDb.TB_Apply_Products where r.Product_Apply_ID == items.Coupon_Apply_Product_ID select r).ToListAsync();

            if (apItems != null && apItems.Count > 0)
                _barunsonDb.RemoveRange(apItems);

            if (capItem != null)
                _barunsonDb.Remove(capItem);

            _barunsonDb.Remove(items);

            _ = await _barunsonDb.SaveChangesAsync();

            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index");
            else
                return LocalRedirect(returnUrl);

        }

        /// <summary>
        /// 쿠폰 편집
        /// </summary>
        /// <param name="id">Coupon Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id, string returnUrl = null)
        {
            var query = from c in _barunsonDb.TB_Coupons
                        where c.Coupon_ID == id
                        select new CouponEditModel
                        { 
                            Coupon_ID = c.Coupon_ID,
                            Coupon_Name = c.Coupon_Name,
                            Publish_Method_Code = c.Publish_Method_Code,
                            Publish_Target_Code = c.Publish_Target_Code,
                            Use_Available_Standard_Code = c.Use_Available_Standard_Code,
                            Standard_Purchase_Price = c.Standard_Purchase_Price,
                            Coupon_Apply_Code = c.Coupon_Apply_Code,
                            Coupon_Apply_Product_ID = c.Coupon_Apply_Product_ID,
                            Discount_Method_Code = c.Discount_Method_Code,
                            Discount_Value = (c.Discount_Method_Code == "DMC01") ? (double?)c.Discount_Price : (c.Discount_Method_Code == "DMC02") ? c.Discount_Rate : (double?)null,
                            Period_Method_Code = c.Period_Method_Code,
                            Publish_Start_Date = !string.IsNullOrEmpty(c.Publish_Start_Date) ? DateTime.Parse(c.Publish_Start_Date) : (DateTime?)null,
                            Publish_End_Date = !string.IsNullOrEmpty(c.Publish_End_Date) ? DateTime.Parse(c.Publish_End_Date): (DateTime?)null,
                            Publish_Period_Code = c.Publish_Period_Code,
                            Description = c.Description,
                            Use_YN = c.Use_YN
                        };

            var model = await query.FirstOrDefaultAsync();
            if (model == null)
                return LocalRedirect(returnUrl);

            //수정 가능여부
            var publish = await (from r in _barunsonDb.TB_Coupon_Publishes where r.Coupon_ID == model.Coupon_ID select r).FirstOrDefaultAsync();
            model.IsSave = (publish == null);

            model.Apply_Product_Codes = new List<string>();
            if (model.Coupon_Apply_Product_ID.HasValue && model.Coupon_Apply_Product_ID > 0)
            {
                var applyprodQuery = from cap in _barunsonDb.TB_Coupon_Apply_Products
                                     join ap in _barunsonDb.TB_Apply_Products on cap.Product_Apply_ID equals ap.Product_Apply_ID
                                     where cap.Product_Apply_ID == model.Coupon_Apply_Product_ID.Value
                                     select ap.Product_Code;

                model.Apply_Product_Codes = await applyprodQuery.ToListAsync();
            }

            model.Publish_Method_Codes = await GetSelectListsCommonCodesAsync("Publish_Method_Code");
            model.Publish_Target_Codes = await GetSelectListsCommonCodesAsync("Publish_Target_Code", true, "", "선택하세요");
            model.Use_Available_Standard_Codes = await GetSelectListsCommonCodesAsync("Use_Available_Standard_Code", true, "", "선택하세요");
            model.Coupon_Apply_Codes = await GetSelectListsCommonCodesAsync("Coupon_Apply_Code", true, "", "선택하세요");
            model.Discount_Method_Codes = await GetSelectListsCommonCodesAsync("Discount_Method_Code");
            model.Period_Method_Codes = await GetSelectListsCommonCodesAsync("Period_Method_Code");
            model.Publish_Period_Codes = await GetSelectListsCommonCodesAsync("Publish_Period_Code");

            ViewData["Title"] = "쿠폰수정";
            ViewData["ReturnUrl"] = returnUrl;
            return View("Edit", model);
        }

        /// <summary>
        /// 쿠폰 등록
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Add()
        {
            var model = new CouponEditModel();

            model.IsSave = true;
            model.Coupon_ID = 0;
            model.Apply_Product_Codes = new List<string>();

            model.Publish_Method_Codes = await GetSelectListsCommonCodesAsync("Publish_Method_Code");
            model.Publish_Target_Codes = await GetSelectListsCommonCodesAsync("Publish_Target_Code", true, "", "선택하세요");
            model.Use_Available_Standard_Codes = await GetSelectListsCommonCodesAsync("Use_Available_Standard_Code", true, "", "선택하세요");
            model.Coupon_Apply_Codes = await GetSelectListsCommonCodesAsync("Coupon_Apply_Code", true, "", "선택하세요");
            model.Discount_Method_Codes = await GetSelectListsCommonCodesAsync("Discount_Method_Code");
            model.Period_Method_Codes = await GetSelectListsCommonCodesAsync("Period_Method_Code");
            model.Publish_Period_Codes = await GetSelectListsCommonCodesAsync("Publish_Period_Code");
            model.Use_YN = "Y";

            ViewData["Title"] = "쿠폰등록";
            ViewData["ReturnUrl"] = string.Empty;
            return View("Edit", model);
        }

        /// <summary>
        /// 쿠폰 등록/수정 저장
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(CouponEditModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (model.Use_Available_Standard_Code == "USC02" && (model.Standard_Purchase_Price == null || model.Standard_Purchase_Price <= 0)) //구매기준
                    ModelState.AddModelError(nameof(model.Standard_Purchase_Price), "사용가능 기준 금액을 입력해주세요");

                if (model.Coupon_Apply_Code != "CET01")
                {
                    //if (model.Coupon_Apply_Product_ID == null || model.Coupon_Apply_Product_ID == 0 || model.Apply_Product_Codes.Count == 0)
                    if (model.Apply_Product_Codes.Count == 0)
                        ModelState.AddModelError(nameof(model.Coupon_Apply_Code), "적용/제외된 상품이 없습니다.");
                }
                if (model.Discount_Method_Code == "DMC01")
                {
                    if (model.Discount_Value == null || model.Discount_Value <=0 )
                        ModelState.AddModelError(nameof(model.Discount_Method_Code), "할인금액을 입략해주세요");
                }
                else if (model.Discount_Method_Code == "DMC02")
                {
                    if (model.Discount_Value == null || model.Discount_Value <= 0 || model.Discount_Value > 100)
                        ModelState.AddModelError(nameof(model.Discount_Method_Code), "할인율을 입력해주세요. 할인율은 100을 초과하면 안됩니다.");
                }

                if (model.Period_Method_Code == "PMC01" && (model.Publish_Start_Date == null || model.Publish_End_Date == null)) //시작일~종료일
                    ModelState.AddModelError(nameof(model.Period_Method_Code), "유효기간 시작, 종료 날짜를 입력해주세요.");

                if (ModelState.IsValid)
                {
                    var now = DateTime.Now;
                    var isModify = false;

                    using var transaction = _barunsonDb.Database.BeginTransaction();

                    TB_Coupon_Apply_Product capItem = null;
                    List<TB_Apply_Product> apItems = new List<TB_Apply_Product>();

                    TB_Coupon item;
                    //DB 저장
                    if (model.Coupon_ID == 0) //신규
                    {
                        capItem = new TB_Coupon_Apply_Product { Coupon_ID = null };
                        _barunsonDb.Add(capItem);
                        await _barunsonDb.SaveChangesAsync();

                        item = new TB_Coupon();
                        item.Regist_DateTime = now;
                        item.Regist_User_ID = CurrentUserId();
                        item.Regist_IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        item.Coupon_Image_URL = @"\img\coupon\/img/108x108_1.gif";
                        
                        item.Coupon_Apply_Product_ID = capItem.Product_Apply_ID;

                        item.Coupon_Name = model.Coupon_Name;
                        item.Publish_Method_Code = model.Publish_Method_Code;
                        item.Publish_Target_Code = model.Publish_Target_Code;
                        item.Use_Available_Standard_Code = model.Use_Available_Standard_Code;
                        item.Standard_Purchase_Price = model.Use_Available_Standard_Code == "USC02" ? model.Standard_Purchase_Price : null;
                        item.Discount_Method_Code = model.Discount_Method_Code;
                        item.Discount_Price = model.Discount_Method_Code == "DMC01" ? (int?)model.Discount_Value : null;
                        item.Discount_Rate = model.Discount_Method_Code == "DMC02" ? (int?)model.Discount_Value : null;
                        item.Period_Method_Code = model.Period_Method_Code;
                        item.Publish_Start_Date = model.Period_Method_Code == "PMC01" ? model.Publish_Start_Date.Value.ToString("yyyy-MM-dd") : null;
                        item.Publish_End_Date = model.Period_Method_Code == "PMC01" ? model.Publish_End_Date.Value.ToString("yyyy-MM-dd") : null;
                        item.Publish_Period_Code = model.Publish_Period_Code;
                        item.Description = model.Description;
                        item.Coupon_Apply_Code = model.Coupon_Apply_Code;

                        _barunsonDb.Add(item);
                        isModify = true;
                    }
                    else
                    {
                        item = await (from r in _barunsonDb.TB_Coupons where r.Coupon_ID == model.Coupon_ID select r).FirstAsync();
                        //수정 가능여부
                        var publish = await (from r in _barunsonDb.TB_Coupon_Publishes where r.Coupon_ID == model.Coupon_ID select r).FirstOrDefaultAsync();
                        isModify = (publish == null);

                        item.Coupon_Apply_Code = model.Coupon_Apply_Code;
                        if (model.Coupon_Apply_Code != "CET01" && (model.Coupon_Apply_Product_ID == null || model.Coupon_Apply_Product_ID == 0)) //상품전체가 아닐 경우
                        {
                            capItem = new TB_Coupon_Apply_Product { Coupon_ID = null };
                            _barunsonDb.Add(capItem);
                            await _barunsonDb.SaveChangesAsync();

                            item.Coupon_Apply_Product_ID = capItem.Product_Apply_ID;
                        }
                        else
                            capItem = await (from r in _barunsonDb.TB_Coupon_Apply_Products where r.Product_Apply_ID == model.Coupon_Apply_Product_ID.Value select r).FirstAsync();

                        apItems = await (from r in _barunsonDb.TB_Apply_Products where r.Product_Apply_ID == model.Coupon_Apply_Product_ID select r).ToListAsync();
                        if (apItems.Count > 0) //기존 상품 있으면 제거
                            _barunsonDb.RemoveRange(apItems);

                    }
                    
                    item.Use_YN = model.Use_YN;
                    item.Description = model.Description;
                    item.Update_DateTime = now;
                    item.Update_User_ID = CurrentUserId();
                    item.Update_IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        
                    if (model.Coupon_Apply_Code != "CET01" && capItem != null)
                    {
                        //상품 추가
                        foreach (var product_Code in model.Apply_Product_Codes.Where(m => !string.IsNullOrEmpty(m)))
                        {
                            _barunsonDb.Add(new TB_Apply_Product { Product_Code = product_Code, Product_Apply = capItem });
                        }
                    }

                    await _barunsonDb.SaveChangesAsync();

                    transaction.Commit();

                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index");
                    else
                        return LocalRedirect(returnUrl);
                }
            }

            model.Publish_Method_Codes = await GetSelectListsCommonCodesAsync("Publish_Method_Code");
            model.Publish_Target_Codes = await GetSelectListsCommonCodesAsync("Publish_Target_Code", true, "", "선택하세요");
            model.Use_Available_Standard_Codes = await GetSelectListsCommonCodesAsync("Use_Available_Standard_Code", true, "", "선택하세요");
            model.Coupon_Apply_Codes = await GetSelectListsCommonCodesAsync("Coupon_Apply_Code", true, "", "선택하세요");
            model.Discount_Method_Codes = await GetSelectListsCommonCodesAsync("Discount_Method_Code");
            model.Period_Method_Codes = await GetSelectListsCommonCodesAsync("Period_Method_Code");
            model.Publish_Period_Codes = await GetSelectListsCommonCodesAsync("Publish_Period_Code");

            ViewData["ReturnUrl"] = returnUrl;
            model.IsSave = true;

            return View("Edit", model);
        }

        /// <summary>
        /// 쿠폰 적용/제외 상품 조회 및 등록
        /// </summary>
        /// <param name="Category_Id1"></param>
        /// <param name="Category_Id2"></param>
        /// <param name="Searchtxt"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Coupon_Apply_ProdcutList(int? Category_Id1, int? Category_Id2, string Searchtxt)
        {
            IQueryable<CouponApplyProdcutListDataModel> query;

            //카테고리 조건 없으면 
            if (Category_Id1 == null && Category_Id2 == null)
            {
                query = from a in _barunsonDb.TB_Products
                        join pcc in _barunsonDb.TB_Common_Codes on new { Code = a.Product_Category_Code, Code_Group = "Product_Category_Code" } equals new { pcc.Code, pcc.Code_Group }
                        where
                            ((string.IsNullOrEmpty(Searchtxt) || (a.Product_Code.Contains(Searchtxt) || a.Product_Name.Contains(Searchtxt))))
                        orderby a.Product_Name
                        select new CouponApplyProdcutListDataModel
                        {
                            Product_ID = a.Product_ID,
                            Product_Category_Name = pcc.Code_Name,
                            Product_Name = a.Product_Name,
                            Product_Code = a.Product_Code,
                            Display_YN = a.Display_YN,
                            Price = a.Price
                        };
            }
            else
            {
                int cateogry = 0;

                if (Category_Id2 != null)
                    cateogry = Category_Id2.Value;
                else 
                    cateogry = Category_Id1.Value;

                query = from a in _barunsonDb.TB_Products
                        join b in _barunsonDb.TB_Product_Categories on a.Product_ID equals b.Product_ID
                        join c in _barunsonDb.TB_Categories on b.Category_ID equals c.Category_ID
                        join pcc in _barunsonDb.TB_Common_Codes on new { Code = a.Product_Category_Code, Code_Group = "Product_Category_Code" } equals new { pcc.Code, pcc.Code_Group }
                        where 
                            c.Category_Type_Code == "CTC02" &&
                            b.Category_ID == cateogry &&
                            ((string.IsNullOrEmpty(Searchtxt) || (a.Product_Code.Contains(Searchtxt) || a.Product_Name.Contains(Searchtxt))))
                        orderby a.Product_Name
                        select new CouponApplyProdcutListDataModel
                        {
                            Product_ID = a.Product_ID,
                            Product_Category_Name = pcc.Code_Name,
                            Product_Name = a.Product_Name,
                            Product_Code = a.Product_Code,
                            Display_YN = a.Display_YN,
                            Price = a.Price
                        };
            }
             var model = await query.ToListAsync();

            return PartialView("Coupon_Apply_ProdcutListPartial", model);
        }

        public async Task<IActionResult> Categories(int? id)
        {
            var items = await GetCategoryAsync(id);
            var model = items.Select(m => new { value = m.Category_ID, text = m.Category_Name });
            return Json(model);
        }

        /// <summary>
        /// 쿠폰 발행 조회
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PublishList(CouponPublishSearchViewModel model)
        {
            //검색어
            var searchUserIds = new List<string>();
            if (!string.IsNullOrEmpty(model.Keyword))
            {
                searchUserIds = await (from a in _barshopDb.S2_UserInfo_TheCards where a.uid.Contains(model.Keyword) || a.uname.Contains(model.Keyword) select a.uid).ToListAsync();
            }

            var query = from a in _barunsonDb.TB_Coupon_Publishes
                        where a.Coupon_ID == model.Coupon_ID &&
                            (model.Use_YN == "ALL" || a.Use_YN == model.Use_YN) &&
                            (searchUserIds.Count == 0 || searchUserIds.Contains(a.User_ID))
                        orderby a.Coupon_Publish_ID descending
                        select new CouponPublishSearchViewModel.CouponPublishSearchDataModel
                        {
                            Coupon_Publish_ID = a.Coupon_Publish_ID,
                            Coupon_ID = a.Coupon_ID,
                            User_ID = a.User_ID,
                            User_Name = "",
                            Phone_Number = "",
                            Use_YN = a.Use_YN,
                            Regist_DateTime = a.Regist_DateTime,
                            Use_DateTime = a.Use_DateTime,
                            Expiration_Date = a.Expiration_Date,
                            Retrieve_DateTime = a.Retrieve_DateTime
                        };

            model.Count = await query.CountAsync();
            model.DataModel = await query.Skip(model.PageFrom).Take(model.PageSize).ToListAsync();

            //회원 정보 일기
            var userIds = model.DataModel.Select(m => m.User_ID).ToList();
            var users = await (from a in _barshopDb.S2_UserInfo_TheCards 
                               where userIds.Contains(a.uid) 
                               select new { a.uid, a.uname, a.hand_phone1, a.hand_phone2, a.hand_phone3 } 
                               ).ToListAsync();
            foreach(var item in model.DataModel)
            {
                var u = users.FirstOrDefault(m => m.uid == item.User_ID);
                if (u == null)
                    item.User_Name = "탈퇴회원";
                else
                {
                    item.User_Name = u.uname;
                    item.Phone_Number = $"{u.hand_phone1}{u.hand_phone2}{u.hand_phone3}";
                }
            }

            model.RouteAction = "PublishList";
            model.RouteController = "Coupon";

            return View(model);
        }

        /// <summary>
        /// 쿠폰 회수
        /// </summary>
        /// <param name="id">Coupon_Publish_ID</param>
        /// <returns></returns>
        public async Task<IActionResult> Retrieve(int id)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            var query = from a in _barunsonDb.TB_Coupon_Publishes
                        where a.Coupon_Publish_ID == id
                        select a;
            var item = await query.FirstOrDefaultAsync();
            if (item != null)
            {
                var now = DateTime.Now;

                item.Use_YN = "N";
                item.Use_DateTime = null;
                item.Retrieve_DateTime = now;
                item.Update_DateTime = now;
                item.Update_IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                item.Update_User_ID = CurrentUserId();

                await _barunsonDb.SaveChangesAsync();
                result.status = true;
                result.message = "";
            }
            else
            {
                result.message = "쿠폰 발행 내역이 없습니다. 회수 할 수 없습니다.";
            }

            return Json(result);
        }

        /// <summary>
        /// 쿠폰 발행
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PublishAdd(CouponPublishAddViewModel model)
        {
            model.DataModel = new List<CouponPublishAddViewModel.CouponPublishAddDataModel>();
            if (!string.IsNullOrEmpty(model.Keyword))
            {
                var query = from u in _barshopDb.S2_UserInfo_TheCards
                            where u.uid.StartsWith(model.Keyword) || u.uname.StartsWith(model.Keyword)
                            orderby u.uname ascending, u.reg_date descending
                            select new 
                            {
                                User_ID = u.uid,
                                User_Name = u.uname,
                                Phone_Number = $"{u.hand_phone1}{u.hand_phone2}{u.hand_phone3}",
                                Wedding_Date = $"{u.wedd_year}-{u.wedd_month}-{u.wedd_day}",
                                Regist_DateTime = u.reg_date
                            };
                var items = await query.ToListAsync();
                foreach(var item in items)
                {
                    DateTime weddDate;
                    
                    model.DataModel.Add(new CouponPublishAddViewModel.CouponPublishAddDataModel
                    {
                        User_ID = item.User_ID,
                        User_Name = item.User_Name,
                        Phone_Number = item.Phone_Number,
                        Wedding_Date = DateTime.TryParse(item.Wedding_Date, out weddDate) ? weddDate : (DateTime?)null ,
                        Regist_DateTime = item.Regist_DateTime
                    });
                }
            }
            return View(model);
        }

        /// <summary>
        /// 쿠폰 발행 저장
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PublishAddSave(CouponPublishAddSaveViewModel model)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            try
            {
                if (model.Coupon_ID > 0 && model.UserIds.Count > 0)
                {
                    var now = DateTime.Now.Date;
                    var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    var currentid = CurrentUserId();

                    DateTime? Expiration_Date = null;
                    var couponItem = await (from a in _barunsonDb.TB_Coupons where a.Coupon_ID == model.Coupon_ID select a).FirstAsync();

                    if (couponItem.Period_Method_Code == "PMC01")  // 날짜 지정
                        Expiration_Date = DateTime.Parse(couponItem.Publish_End_Date);
                    else if (couponItem.Period_Method_Code == "PMC02")  // 발행일로부터
                    {
                        var code = await (from a in _barunsonDb.TB_Common_Codes
                                          where a.Code_Group == "Publish_Period_Code" && a.Code == couponItem.Publish_Period_Code
                                          select a).FirstAsync();

                        Expiration_Date = now.AddDays(int.Parse(code.Code_Name));
                    }

                    var selectedUsers = model.UserIds.Where(m => m.Selected);

                    foreach (var userid in selectedUsers)
                    {
                        _barunsonDb.TB_Coupon_Publishes.Add(new TB_Coupon_Publish
                        {
                            Use_YN = "N",
                            Update_DateTime = DateTime.Now,
                            Regist_DateTime = DateTime.Now,
                            Coupon_ID = model.Coupon_ID,
                            User_ID = userid.Value,
                            Expiration_Date = Expiration_Date?.ToString("yyyy-MM-dd"),
                            Regist_IP = ip,
                            Update_IP = ip,
                            Regist_User_ID = currentid,
                            Update_User_ID = currentid
                        });
                    }

                    await _barunsonDb.SaveChangesAsync();

                    result.status = true;
                    result.message = "";
                }
                else
                {
                    result.message = "쿠폰 내역이 없습니다.";
                }
            }
            catch(Exception e)
            {
                result.message = e.Message;
            }

            return Json(result);
        }
    }
}
