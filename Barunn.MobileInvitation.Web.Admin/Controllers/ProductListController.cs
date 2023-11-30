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
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    /// <summary>
    /// 상품목록
    /// </summary>
    [Authorize]
    public class ProductListController : BaseController
    {
        private readonly IStaticContentHelper _staticContent;

        public ProductListController(ILogger<ProductRegistController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb,
            IStaticContentHelper staticContent)
            : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _staticContent = staticContent;
        }

        #region Product list

        /// <summary>
        /// 상품/템플릿 리스트 및 검색
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(ProductListViewModel model)
        {
            model.ProductCategoryCodes = (await GetSelectListsCommonCodesAsync("Product_Category_Code", true, "", "전체")).ToList();
            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }
            
            var result = await GetProductsAsync(
                model.SearchKind,
                model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.SearchViewYn,
                model.SearchTxt,
                model.PageFrom, 
                model.PageSize);

            model.Count = result.Count;
            model.Products = result.Items;

            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "Index";
            model.RouteController = "ProductList";
            
            return View("Index", model);
        }
        public async Task<IActionResult> ListExcel(ProductListViewModel model)
        {
            model.ProductCategoryCodes = (await GetSelectListsCommonCodesAsync("Product_Category_Code", true, "", "전체")).ToList();
            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }

            var result = await GetProductsAsync(
               model.SearchKind,
               model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList(),
               model.SearchViewYn,
               model.SearchTxt);

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "이미지";
            workSheet.Cells[rowIndex, colIndex++].Value = "브랜드";
            workSheet.Cells[rowIndex, colIndex++].Value = "상품명";
            workSheet.Cells[rowIndex, colIndex++].Value = "상품코드";
            workSheet.Cells[rowIndex, colIndex++].Value = "가격";
            workSheet.Cells[rowIndex, colIndex++].Value = "진열상태";
            workSheet.Cells[rowIndex, colIndex++].Value = "최초등록일";
            workSheet.Cells[rowIndex, colIndex++].Value = "최종수정일";

            foreach (var item in result.Items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = (rowIndex - 1);
                workSheet.Cells[rowIndex, colIndex++].Value = item.Main_Image_URL;
                workSheet.Cells[rowIndex, colIndex++].Value = model.ProductBarnds.First(m => m.Value == item.Product_Brand_Code).Text;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Code;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Price;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Display_YN;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Regist_DateTime?.ToShortDateString();
                workSheet.Cells[rowIndex, colIndex++].Value = item.Update_DateTime?.ToShortDateString();
            }
            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductList" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }
        private async Task<(int Count, List<TB_Product> Items)> GetProductsAsync(string searchKind, List<string> barans, string searchViewYn, string searchtxt, int pageFrom = 0, int? pageSize = null)
        {
            var query = from p in _barunsonDb.TB_Products
                        where barans.Contains(p.Product_Brand_Code) &&
                            (string.IsNullOrEmpty(searchKind) || p.Product_Category_Code == searchKind) &&
                            (string.IsNullOrEmpty(searchViewYn) || p.Display_YN == searchViewYn) &&
                            (string.IsNullOrEmpty(searchtxt) || (p.Product_Name.Contains(searchtxt) || p.Product_Code.Contains(searchtxt)))
                        orderby p.Regist_DateTime descending
                        select p;

            //총 아이템 수
            int count = 0;
            List<TB_Product> item = null;
            if (pageSize.HasValue)
            {
                //총 아이템 수
                count = await query.CountAsync();
                //페이지 수 만큼 데이터 읽기
                item = await query.Skip(pageFrom).Take(pageSize.Value).ToListAsync();
            }
            else
            {
                item = await query.ToListAsync();
                count = item.Count;
            }

            return (count, item);
        }
        #endregion

        #region Icon manage
        private async Task<IActionResult> GetIconListAsync()
        {
            var query = from a in _barunsonDb.TB_Icons
                        join b in _barunsonDb.TB_Product_Icons on a.Icon_ID equals b.Icon_ID into g
                        from b in g.DefaultIfEmpty()
                        group b by new { a.Icon_ID, a.Icon_URL } into ag
                        select new IconViewModel { Icon_ID = ag.Key.Icon_ID, Icon_URL = ag.Key.Icon_URL, Selected = false, Products = ag.Count(x => x != null)};

            var items = await query.ToListAsync();
            items.ForEach(m => m.Icon_URL = _staticContent.AbsoluteUri(m.Icon_URL).ToString());

            return View("IconManage", items);
        }

        [HttpGet]
        public async Task<IActionResult> IconManage()
        {
            return await GetIconListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> IconUpload(IFormFile file)
        {
            if (file.Length > 0)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                var filename = Guid.NewGuid().ToString() + ext;
                var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "icon", filename);
                var (status, message) = await _staticContent.UploadFileAsync(file, rPath);

                if (status)
                {
                    var now = DateTime.Now;
                    var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    var currUser = CurrentUserId();

                    var item = new TB_Icon
                    {
                        Icon_URL = message,
                        Regist_IP = ipAddr,
                        Regist_DateTime = now,
                        Regist_User_ID = currUser,
                        Update_IP = ipAddr,
                        Update_DateTime = now,
                        Update_User_ID = currUser
                    };
                    _barunsonDb.TB_Icons.Add(item);
                    var a = await _barunsonDb.SaveChangesAsync();

                }
                else
                {
                    ModelState.AddModelError("", "Icon 등록에 실패 하였습니다.");
                }
            }

            return await GetIconListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> IconDelete(List<IconViewModel> model)
        {
            var ids = model.Where(m => m.Selected).Select(m => m.Icon_ID);
            var delItems = from a in _barunsonDb.TB_Icons
                           where ids.Contains(a.Icon_ID)
                           select a;
            _barunsonDb.RemoveRange(delItems);
            await _barunsonDb.SaveChangesAsync();

            return await GetIconListAsync();
        }
        #endregion

        #region Product Display    
        /// <summary>
        /// 진열 상품 목록 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        private async Task<List<CategoryDisplayViewModel.DisplayItem>> GetCategoryProductsAsync(int cateId)
        {
            var query = from a in _barunsonDb.TB_Products
                        join b in _barunsonDb.TB_Product_Categories on a.Product_ID equals b.Product_ID
                        join c in _barunsonDb.TB_Categories on b.Category_ID equals c.Category_ID
                        where b.Category_ID == cateId &&
                             c.Display_YN == "Y"
                        orderby b.Sort ascending
                        select new CategoryDisplayViewModel.DisplayItem
                        {
                            Product_ID = a.Product_ID,
                            Product_Brand_Code = a.Product_Brand_Code,
                            Category_ID = b.Category_ID,
                            Product_Name = a.Product_Name,
                            Product_Code = a.Product_Code,
                            Price = a.Price,
                            Regist_DateTime = b.Regist_DateTime,
                            Sort = b.Sort,
                            Main_Image_URL = a.Main_Image_URL,
                            DisplayYN = a.Display_YN == "Y"
                        };
            return await query.ToListAsync();
        }

        /// <summary>
        /// 진열 뷰모델 초기화
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task InitCategoryDisplayModel(CategoryDisplayViewModel model)
        {
            model.DisplayItems = await GetCategoryProductsAsync(model.Category);
            var brands = await GetCommonCodeAsync("Product_Brand_Code");

            foreach (var item in model.DisplayItems)
            {
                item.ProductBarndName = brands.First(m => m.Code == item.Product_Brand_Code).Code_Name;
                item.CategoryName = model.Categories.First(m => m.Value == item.Category_ID.ToString()).Text;
                item.Main_ImageAbsoluteUri = string.IsNullOrEmpty(item.Main_Image_URL) ? null : _staticContent.AbsoluteUri(item.Main_Image_URL);
            }
        }

        /// <summary>
        /// 상품 조회 팝업창 
        /// </summary>
        /// <param name="id">category id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> PopupProductSearch(int id, ProductListViewModel model)
        {
            ViewBag.id = id;
            model.ProductCategoryCodes = (await GetSelectListsCommonCodesAsync("Product_Category_Code", true, "", "전체")).ToList();
            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }
            var barans = model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList();

            var inProductIds = from a in _barunsonDb.TB_Categories
                               join b in _barunsonDb.TB_Product_Categories on a.Category_ID equals b.Category_ID
                               where a.Category_ID == id
                               select b.Product_ID;

            var query = from p in _barunsonDb.TB_Products
                        where !inProductIds.Contains(p.Product_ID) && 
                            barans.Contains(p.Product_Brand_Code) &&
                            (string.IsNullOrEmpty(model.SearchKind) || p.Product_Category_Code == model.SearchKind) &&
                            (string.IsNullOrEmpty(model.SearchViewYn) || p.Display_YN == model.SearchViewYn) &&
                            (string.IsNullOrEmpty(model.SearchTxt) || (p.Product_Name.Contains(model.SearchTxt) || p.Product_Code.Contains(model.SearchTxt)))
                        orderby p.Regist_DateTime descending
                        select p;
            
            model.Count = await query.CountAsync();
            model.Products = await query.Skip(model.PageFrom).Take(model.PageSize).ToListAsync();
            
            model.RouteAction = "PopupProductSearch";
            model.RouteController = "ProductList";
            return PartialView("_PopupProductSearch", model);
        }

        /// <summary>
        /// 진열 상품 추가
        /// </summary>
        /// <param name="id">category id</param>
        /// <param name="ids">상품 id 목록</param>
        /// <returns></returns>
        public async Task<IActionResult> CategoryDisplayAdd(int id, [FromBody] List<int> ids)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            if (ids.Count > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                var query = from a in _barunsonDb.TB_Product_Categories
                            where a.Category_ID == id
                            orderby a.Sort descending
                            select a.Sort;

                var maxSort = await query.FirstOrDefaultAsync();
                if (!maxSort.HasValue)
                    maxSort = 1;
                else
                    maxSort++;

                var now = DateTime.Now.Date;
                var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currentid = CurrentUserId();

                foreach (var pid in ids)
                {
                    var item = new TB_Product_Category
                    {
                        Category_ID = id,
                        Product_ID = pid,
                        Sort = 1,           // maxSort.Value, 추가시 최상단에 위치,, 하단위치면 주석제거
                        Update_DateTime = now,
                        Update_IP = ip,
                        Update_User_ID = currentid,
                        Regist_DateTime = now,
                        Regist_IP = ip,
                        Regist_User_ID = currentid,
                    };

                    _barunsonDb.TB_Product_Categories.Add(item);

                    maxSort++;
                }
                await _barunsonDb.SaveChangesAsync();
                await transaction.CommitAsync();

                result.status = true;
                result.message = "";
            }
            return Json(result);
        }

        /// <summary>
        /// 진열상품 제거
        /// </summary>
        /// <param name="id">category id</param>
        /// <param name="ids">상품 Id 목록</param>
        /// <returns></returns>
        public async Task<IActionResult> CategoryDisplayDelete(int id, [FromBody] List<int> ids)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            if (ids.Count > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                var query = from a in _barunsonDb.TB_Product_Categories
                            where a.Category_ID == id
                            orderby a.Sort ascending
                            select a;

                var items = await query.ToListAsync();

                var now = DateTime.Now.Date;
                var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currentid = CurrentUserId();

                var delItems = items.Where(m => ids.Contains(m.Product_ID));
                _barunsonDb.TB_Product_Categories.RemoveRange(delItems);

                // 정열 - 자동 번호 부여가 필요할 경우 아래 주석 제거
                //var updateItems = items.Where(m => !ids.Contains(m.Product_ID)).OrderBy(m => m.Sort);
                //var newsort = 1;
                //foreach (var item in updateItems)
                //{
                //    item.Sort = newsort;
                //    item.Update_DateTime = now;
                //    item.Update_IP = ip;
                //    item.Update_User_ID = currentid;

                //    newsort++;
                //}

                await _barunsonDb.SaveChangesAsync();
                await transaction.CommitAsync();

                result.status = true;
                result.message = "";
            }
            return Json(result);
        }

        /// <summary>
        /// 진열상품 정열 업데이트
        /// </summary>
        /// <param name="id">category id</param>
        /// <param name="prods">상품 id, sort</param>
        /// <returns></returns>
        public async Task<IActionResult> CategoryDisplayUpdate(int id, [FromBody] Dictionary<string, string> prods)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            if (prods.Count > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                var query = from a in _barunsonDb.TB_Product_Categories
                            where a.Category_ID == id
                            orderby a.Sort descending
                            select a;
                var items = await query.ToListAsync();

                var now = DateTime.Now.Date;
                var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currentid = CurrentUserId();

                foreach (var pid in prods)
                {
                    var item = items.FirstOrDefault(m => m.Product_ID == int.Parse(pid.Key));
                    if (item != null)
                    {
                        item.Sort = int.Parse(pid.Value);
                        item.Update_DateTime = now;
                        item.Update_IP = ip;
                        item.Update_User_ID = currentid;
                    }
                }
                await _barunsonDb.SaveChangesAsync();
                await transaction.CommitAsync();

                result.status = true;
                result.message = "";
            }
            return Json(result);
        }

        #region Main

        /// <summary>
        /// 메인 진열 목록
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> MainDisplayList(CategoryDisplayViewModel model)
        {
            model.Categories = (await GetSelectListsMainCategoryAsync()).ToList();
            if (model.Category == 0)
                model.Category = int.Parse(model.Categories.First().Value);

            await InitCategoryDisplayModel(model);
            
            ViewBag.ReturnUrl = Request.GetEncodedPathAndQuery();

            return View(model);
        }

        /// <summary>
        /// 메인 진열 목록 엑셀 출력
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> MainDisplayExcel(int id)
        {
            var items = await GetCategoryProductsAsync(id);
            var brands = await GetCommonCodeAsync("Product_Brand_Code");
            var mainCates = await GetSelectListsMainCategoryAsync();

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "진열순서";
            workSheet.Cells[rowIndex, colIndex++].Value = "브랜드";
            workSheet.Cells[rowIndex, colIndex++].Value = "대분류";
            workSheet.Cells[rowIndex, colIndex++].Value = "상품명";
            workSheet.Cells[rowIndex, colIndex++].Value = "제품코드";
            workSheet.Cells[rowIndex, colIndex++].Value = "가격";
            workSheet.Cells[rowIndex, colIndex++].Value = "진열일자";
            workSheet.Cells[rowIndex, colIndex++].Value = "진열상태";

            foreach (var item in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = item.Sort;
                workSheet.Cells[rowIndex, colIndex++].Value = brands.First(m => m.Code == item.Product_Brand_Code).Code_Name; ;
                workSheet.Cells[rowIndex, colIndex++].Value = mainCates.First(m => m.Value == item.Category_ID.ToString()).Text;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Code;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Price;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Regist_DateTime?.ToShortDateString();
                workSheet.Cells[rowIndex, colIndex++].Value = item.DisplayYN;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MainDisplay" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }
        
        #endregion

        #region Category

        /// <summary>
        /// 카테고리 진열 목록
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> CateDisplayList(CategoryDisplayViewModel model)
        {
            model.Categories = new List<SelectListItem>();
            var parentCategories = await GetSelectListsCategoryAsync(null);
            foreach (var preantCategory in parentCategories)
            {
                model.Categories.Add(new SelectListItem { Value = preantCategory.Value, Text = preantCategory.Text });
                var childCategories = await GetSelectListsCategoryAsync(int.Parse(preantCategory.Value));
                foreach (var childCategory in childCategories)
                {
                    model.Categories.Add(new SelectListItem { Value = childCategory.Value, Text = $"{preantCategory.Text} > {childCategory.Text}" });
                }
            }
            if (model.Category == 0)
                model.Category = int.Parse(model.Categories.First().Value);

            await InitCategoryDisplayModel(model);

            ViewBag.ReturnUrl = Request.GetEncodedPathAndQuery();

            return View(model);
        }

        /// <summary>
        ///  카테고리 진열 목록 엑셀 출력
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> CateDisplayExcel(int id)
        {
            var items = await GetCategoryProductsAsync(id);
            var brands = await GetCommonCodeAsync("Product_Brand_Code");
            var Cates = await (from r in _barunsonDb.TB_Categories
                               where r.Category_Type_Code == "CTC02"
                               select new { r.Category_ID, r.Parent_Category_ID, r.Category_Name }
                                   ).ToListAsync();

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "진열순서";
            workSheet.Cells[rowIndex, colIndex++].Value = "브랜드";
            workSheet.Cells[rowIndex, colIndex++].Value = "대분류카테고리";
            workSheet.Cells[rowIndex, colIndex++].Value = "중분류카테고리";
            workSheet.Cells[rowIndex, colIndex++].Value = "상품명";
            workSheet.Cells[rowIndex, colIndex++].Value = "제품코드";
            workSheet.Cells[rowIndex, colIndex++].Value = "가격";
            workSheet.Cells[rowIndex, colIndex++].Value = "진열일자";
            workSheet.Cells[rowIndex, colIndex++].Value = "진열상태";

            foreach (var item in items)
            {
                rowIndex++;
                colIndex = 1;

                var cate = Cates.First(m => m.Category_ID == item.Category_ID);
                var pcate = Cates.FirstOrDefault(m => m.Category_ID == cate.Parent_Category_ID);

                workSheet.Cells[rowIndex, colIndex++].Value = item.Sort;
                workSheet.Cells[rowIndex, colIndex++].Value = brands.First(m => m.Code == item.Product_Brand_Code).Code_Name; ;
                workSheet.Cells[rowIndex, colIndex++].Value = pcate == null ? cate.Category_Name : pcate.Category_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = cate.Parent_Category_ID == null ? "" : cate.Category_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Code;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Price;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Regist_DateTime?.ToShortDateString();
                workSheet.Cells[rowIndex, colIndex++].Value = item.DisplayYN;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CategoryDisplay" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }

        #endregion

        #endregion
    }
}
