using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Dac.Contexts;
using Barunn.MobileInvitation.Dac.Models.Barunson;
using Barunn.MobileInvitation.Web.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    [Authorize]
    public class FlagiftController : BaseController
    {
        private const string PID = "flasystem";

        #region 생성자
        public FlagiftController(ILogger<StatisticsController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
        }
        #endregion

        #region 매출 현황

        /// <summary>
        /// 매출 통계 - 일별
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesDaily(int? year, int? month)
        {
            var targetDate = GetTargetDate(year, month);

            var model = new StatisticsViewModel<FlagiftSaleStatistic>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                StartYear = 2022,
                Items = await GetSalesStatisticDaysAsync(targetDate),
                Total = new FlagiftSaleStatistic()
            };
            model.Total.SetCount = model.Items.Sum(m => m.SetCount);
            model.Total.SetCancelCount = model.Items.Sum(m => m.SetCancelCount);
            model.Total.GiftCount = model.Items.Sum(m => m.GiftCount);
            model.Total.GiftCancelCount = model.Items.Sum(m => m.GiftCancelCount);
            model.Total.GiftAmount = model.Items.Sum(m => m.GiftAmount);


            return View(model);
        }
        /// <summary>
        /// 매출 통계 - 일별 엑셀
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesDailyExcel(int? year, int? month)
        {
            var targetDate = GetTargetDate(year, month);
            var items = await GetSalesStatisticDaysAsync(targetDate);

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "일자";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 설정-선물 설정";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 설정-선물 취소";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 선물-선물 건수";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 선물-취소/환불";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 선물-결제금액";

            foreach (var data in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.SetCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.SetCancelCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.GiftCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.GiftCancelCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.GiftAmount;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FlowerSalesDaily_" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }
        /// <summary>
        /// 매출 통계 - 월별
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesMonthly(int? year)
        {
            var targetDate = GetTargetDate(year);

            var model = new StatisticsViewModel<FlagiftSaleStatistic>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                StartYear = 2022,
                Items = await GetSalesStatisticMonthsAsync(targetDate),
                Total = new FlagiftSaleStatistic()
            };
            model.Total.SetCount = model.Items.Sum(m => m.SetCount);
            model.Total.SetCancelCount = model.Items.Sum(m => m.SetCancelCount);
            model.Total.GiftCount = model.Items.Sum(m => m.GiftCount);
            model.Total.GiftCancelCount = model.Items.Sum(m => m.GiftCancelCount);
            model.Total.GiftAmount = model.Items.Sum(m => m.GiftAmount);

            return View(model);
        }
        /// <summary>
        /// 매출 통계 - 월별 엑셀
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesMonthlyExcel(int? year)
        {
            var targetDate = GetTargetDate(year);
            var items = await GetSalesStatisticMonthsAsync(targetDate);

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "일자";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 설정-선물 설정";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 설정-선물 취소";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 선물-선물 건수";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 선물-취소/환불";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환 선물-결제금액";

            foreach (var data in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.SetCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.SetCancelCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.GiftCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.GiftCancelCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.GiftAmount;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FlowerSalesMonthly_" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }
        #endregion

        #region 주문관리
        /// <summary>
        /// 주문별
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> OrderList(FlaOrderSearchViewModel<FlaOrderSearchDataModel> model)
        {
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");

            var result = await GetOrderListAsync(
               model.Keyword,
               model.Period,
               model.StartDate,
               model.EndDate,
               model.PageFrom,
               model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "OrderList";
            model.RouteController = "Flagift";

            return View(model);
        }
        public async Task<IActionResult> OrderListExcel(FlaOrderSearchViewModel<FlaOrderSearchDataModel> model)
        {
            var result = await GetOrderListAsync(
               model.Keyword,
               model.Period,
               model.StartDate,
               model.EndDate);

            int rowIndex = 1;
            int colIndex = 1;
            int rowNum = result.Count;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "No";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "이름";
            workSheet.Cells[rowIndex, colIndex++].Value = "아이디";
            workSheet.Cells[rowIndex, colIndex++].Value = "선물건수";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "청첩장 주문일시";
            workSheet.Cells[rowIndex, colIndex++].Value = "예식일";

            foreach (var item in result.Items)
            {
                rowIndex++;
                colIndex = 1;
                workSheet.Cells[rowIndex, colIndex++].Value = rowNum;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderCode;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserId;
                workSheet.Cells[rowIndex, colIndex++].Value = item.FlaGiftCount;
                workSheet.Cells[rowIndex, colIndex++].Value = item.FlaGiftAmount;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderDate?.ToString("yyyy-MM-dd HH:mm");
                workSheet.Cells[rowIndex, colIndex++].Value = item.WeddingDate?.ToString("yyyy-MM-dd HH:mm");

                rowNum--;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrderList" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }
        /// <summary>
        /// 주문별 상세 화환 주문 목록
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> FlaOrderListPartial(FlaOrderItemsViewModel model)
        {
            model.RouteAction = "FlaOrderListPartial";
            model.RouteController = "Flagift";
            var orderQuery = from o in _barunsonDb.TB_Orders where o.Order_ID == model.OrderId select new { o.Name };
            var orderItem = await orderQuery.FirstOrDefaultAsync();
            if (orderItem != null)
            {
                model.UserName = orderItem.Name;
                model.DataModel = new List<FlaOrderItemDataModel>();

                var fquery = from m in _barunsonDb.TB_Order_PartnerShip
                             where m.Order_ID == model.OrderId && m.P_Id == PID && m.Payment_Status_Code == "PSC02"
                             orderby m.P_OrderDate descending
                             select m;

                //총 아이템 수
                model.Count = await fquery.CountAsync();
                //페이지 수 만큼 데이터 읽기
                var query = fquery.Skip(model.PageFrom).Take(model.PageSize);
                var queryItems = await query.ToListAsync();

                foreach(var item in queryItems)
                {
                    var jsonData = JObject.Parse(item.P_ExtendData);
                    var data = new FlaOrderItemDataModel
                    {
                        OrderCode = item.P_OrderCode,
                        OrderTitle = (string)jsonData["deli_title"],
                        OrderName = item.P_Order_Name,
                        ProductName = item.P_ProductName,
                        SalePrice = item.Payment_Price,
                        OrderDate = item.P_OrderDate,
                        DeliveryAddress = (string)jsonData["deli_addr"],
                        OrderStateName = item.P_OrderState
                    };
                    DateTime deliTime;
                    if (DateTime.TryParse((string)jsonData["deli_time"], out deliTime))
                        data.DeliveryDate = deliTime;

                    model.DataModel.Add(data);
                }

            }
            return View("FlaOrderListPartial", model);
        }

        /// <summary>
        /// 건별
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> FlaOrderList(FlaOrderSearchViewModel<FlaOrderDataModel> model)
        {
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");
            model.Periods =  new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "화환주문일", Value = "Order" },
                        new SelectListItem { Text = "예식일", Value = "Wedding" }
                    }, "Value", "Text");

            var result = await GetFlaOrderListAsync(
              model.Keyword,
              model.Period,
              model.StartDate,
              model.EndDate,
              model.PageFrom,
              model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "FlaOrderList";
            model.RouteController = "Flagift";

            return View(model);
        }
        public async Task<IActionResult> FlaOrderListExcel(FlaOrderSearchViewModel<FlaOrderDataModel> model)
        {
            var result = await GetFlaOrderListAsync(
              model.Keyword,
              model.Period,
              model.StartDate,
              model.EndDate);

            int rowIndex = 1;
            int colIndex = 1;
            int rowNum = result.Count;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "No";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "이름";
            workSheet.Cells[rowIndex, colIndex++].Value = "아이디";
            workSheet.Cells[rowIndex, colIndex++].Value = "구분";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문자";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "상품";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "배송상태";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환주문일시";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제일";
            workSheet.Cells[rowIndex, colIndex++].Value = "예식일시";

            foreach (var item in result.Items)
            {
                rowIndex++;
                colIndex = 1;
                workSheet.Cells[rowIndex, colIndex++].Value = rowNum;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderCode;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserId;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderTitle;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.POrderCode;
                workSheet.Cells[rowIndex, colIndex++].Value = item.ProductName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.SalePrice;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderStateName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderDate.ToString("yyyy-MM-dd HH:mm");
                workSheet.Cells[rowIndex, colIndex++].Value = item.PPaymentDate?.ToString("yyyy-MM-dd");
                workSheet.Cells[rowIndex, colIndex++].Value = item.WeddingDate?.ToString("yyyy-MM-dd HH:mm");

                rowNum--;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FlaOrderList" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }
        /// <summary>
        /// 최소 환불
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> FlaRefundList(FlaOrderSearchViewModel<FlaRefundDataModel> model)
        {
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");
            model.Periods = new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "화환주문일", Value = "Order" },
                        new SelectListItem { Text = "최소/환불일", Value = "Refund" }
                    }, "Value", "Text");

            var result = await GetFlaRefundListAsync(
              model.Keyword,
              model.Period,
              model.StartDate,
              model.EndDate,
              model.PageFrom,
              model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "FlaRefundList";
            model.RouteController = "Flagift";

            return View(model);
        }
        public async Task<IActionResult> FlaRefundListExcel(FlaOrderSearchViewModel<FlaRefundDataModel> model)
        {
            var result = await GetFlaRefundListAsync(
              model.Keyword,
              model.Period,
              model.StartDate,
              model.EndDate);

            int rowIndex = 1;
            int colIndex = 1;
            int rowNum = result.Count;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "No";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "이름";
            workSheet.Cells[rowIndex, colIndex++].Value = "아이디";
            workSheet.Cells[rowIndex, colIndex++].Value = "구분";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문자";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "상품";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제수단";
            workSheet.Cells[rowIndex, colIndex++].Value = "환불방법";
            workSheet.Cells[rowIndex, colIndex++].Value = "화환주문일시";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제일";
            workSheet.Cells[rowIndex, colIndex++].Value = "취소/환불일시";

            foreach (var item in result.Items)
            {
                rowIndex++;
                colIndex = 1;
                workSheet.Cells[rowIndex, colIndex++].Value = rowNum;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderCode;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserId;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderTitle;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.POrderCode;
                workSheet.Cells[rowIndex, colIndex++].Value = item.ProductName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.SalePrice;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Payment_Method_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Payment_Status_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderDate.ToString("yyyy-MM-dd HH:mm");
                workSheet.Cells[rowIndex, colIndex++].Value = item.PPaymentDate?.ToString("yyyy-MM-dd");
                workSheet.Cells[rowIndex, colIndex++].Value = item.RefundDate?.ToString("yyyy-MM-dd HH:mm");

                rowNum--;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FlaRefundList" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }
        #endregion

        #region 화환 배너 관리
        public async Task<IActionResult> BannerManageList(FlaBannerManageSearchViewModel model)
        {
            var query = from m in _barunsonDb.TB_FlaBannerManage
                        where model.Allowed == "ALL" || (m.Allowed == (model.Allowed == "True"))
                        orderby m.Id descending
                        select m;
            var items = await query.ToListAsync();

            //데이터 양이 많을 수 없기 때문에 페이지를 위한 코드 없이 전체 테이블 처리
            var dataModel = new List<FlaBannerManageDataModel>();
            foreach (var item in items)
            {
                var keywords = JsonSerializer.Deserialize<List<string>>(item.Keywords);

                //검색 키워드 포함 여부 검사
                if (!string.IsNullOrWhiteSpace(model.Keyword) &&
                    !item.GroupName.Contains(model.Keyword, StringComparison.InvariantCultureIgnoreCase) &&
                    !keywords.Any(m => m.Contains(model.Keyword, StringComparison.InvariantCultureIgnoreCase)))
                {
                    continue;
                }

                dataModel.Add(new FlaBannerManageDataModel
                {
                    Id = item.Id,
                    GroupName = item.GroupName,
                    Allowed = model.Alloweds.Single(m => m.Value == item.Allowed.ToString()).Text,
                    Keywords = string.Join(",",keywords),
                    RegistDateTime = item.RegistDateTime,
                    UpdateDateTime = item.UpdateDateTime,
                    RegistUserName = item.RegistUserName
                });
            }
            model.Count = dataModel.Count;
            model.DataModel = dataModel.Skip(model.PageFrom).Take(model.PageSize).ToList();
            model.RouteAction = "BannerManageList";
            model.RouteController = "Flagift";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> BannerManageEdit(int Id)
        {
            var model = new FlaBannerManageEditModel();
            
            if (Id > 0)
            {
                var query = from m in _barunsonDb.TB_FlaBannerManage
                            where m.Id == Id
                            select m;
                var item = await query.FirstOrDefaultAsync();
                if (item != null)
                {
                    model.Id = item.Id;
                    model.GroupName = item.GroupName;
                    model.Allowed = item.Allowed;
                    model.Keywords = (!string.IsNullOrEmpty(item.Keywords)) ?
                        JsonSerializer.Deserialize<List<string>>(item.Keywords) : new List<string>();
                }
            }

            if (model.Keywords.Count == 0)
            {
                model.Keywords.Add("");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> BannerManageSave(FlaBannerManageEditModel model)
        {
            TB_FlaBannerManage item;
            string? msg = null;

            if (ModelState.IsValid)
            {
                var keywordCount = model.Keywords.Where(m => !string.IsNullOrWhiteSpace(m)).Count();

                if (string.IsNullOrWhiteSpace(model.GroupName))
                    msg = "키워드 그룹명을 입력하세요.";
                else if (keywordCount == 0)
                    msg = "연관 키워드는 한개 이상 입력해야 합니다.";
                else
                {
                    if (model.Id > 0)
                    {
                        var query = from m in _barunsonDb.TB_FlaBannerManage
                                    where m.Id == model.Id
                                    select m;
                        item = await query.FirstOrDefaultAsync();

                    }
                    else
                    {
                        item = new TB_FlaBannerManage();
                        item.RegistDateTime = DateTime.Now;

                        _barunsonDb.TB_FlaBannerManage.Add(item);
                    }
                    if (item != null)
                    {
                        item.GroupName = model.GroupName;
                        item.Allowed = model.Allowed;
                        item.Keywords = JsonSerializer.Serialize<List<string>>(model.Keywords.Where(m => !string.IsNullOrWhiteSpace(m)).Distinct().ToList());
                        item.UpdateDateTime = DateTime.Now;
                        item.RegistUserId = CurrentUserId();
                        item.RegistUserName = User.Identity.Name;

                        await _barunsonDb.SaveChangesAsync();

                        model.Id = item.Id;
                        return RedirectToAction("BannerManageList");
                    }
                    else
                    {
                        msg = "저장실패, 데이터를 찾을 수 없습니다!";
                    }
                }
            }
            else
            {
                msg = "저장실패, 입력된 정보를 다시 확인해주세요!";
            }
            model.Message = msg;

            return View("BannerManageEdit", model);
        }

        public async Task<IActionResult> BannerManageDelete(int Id)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            if (Id > 0)
            {
                var query = from m in _barunsonDb.TB_FlaBannerManage
                            where m.Id == Id
                            select m;
                var item = await query.FirstOrDefaultAsync();
                if (item != null)
                {
                    _barunsonDb.TB_FlaBannerManage.Remove(item);
                    await _barunsonDb.SaveChangesAsync();
                }

                result.status = true;
                result.message = "";

            }
            return Json(result);
        }
        #endregion

        #region 내부 함수
        /// <summary>
        /// 매출 현황 - 일별
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task<List<FlagiftSaleStatistic>> GetSalesStatisticDaysAsync(DateTime date)
        {
            var result = new List<FlagiftSaleStatistic>();
            var startDate = new DateTime(date.Year, date.Month, 1);
            var enddate = startDate.AddMonths(1);
            if (enddate > DateTime.Now)
                enddate = DateTime.Now.Date;

            var dateRange = new Dictionary<DateTime, DateTime>();
            //월 생성
            while (startDate <= enddate) 
            {
                var nextdate = startDate.AddDays(1);
                dateRange.Add(startDate, nextdate);
                startDate = nextdate;
            }
            
            foreach (var workMonth in dateRange.OrderByDescending(m => m.Key))
            {
                var item = new FlagiftSaleStatistic
                {
                    Date = workMonth.Key.ToString("yyyy-MM-dd (ddd)")
                };
                var bquery = from m in _barunsonDb.TB_Orders
                            join i in _barunsonDb.TB_Invitations on m.Order_ID equals i.Order_ID
                            join d in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals d.Invitation_ID
                            where m.Payment_DateTime >= workMonth.Key && m.Payment_DateTime < workMonth.Value
                                && m.Payment_Status_Code == "PSC02"
                            group d by 1 into g
                            select new
                            {
                                SetCount = g.Sum(x => x.Flower_gift_YN == "Y" ? 1 : 0),
                                SetCancelCount = g.Sum(x => x.Flower_gift_YN == "C" ? 1 : 0)
                            };
                var barunItems = await bquery.FirstOrDefaultAsync();
                if (barunItems != null)
                {
                    item.SetCount = barunItems.SetCount;
                    item.SetCancelCount = barunItems.SetCancelCount;
                }
                var fquery = from m in _barunsonDb.TB_Order_PartnerShip
                             where m.P_OrderDate >= workMonth.Key && m.P_OrderDate < workMonth.Value
                                 && m.P_Id == PID
                             group m by 1 into g
                             select new
                             {
                                 GiftCount = g.Sum(x => x.Payment_Status_Code == "PSC02" ? 1 : 0),
                                 GiftCancelCount = g.Sum(x => x.Payment_Status_Code == "PSC03" ? 1 : 0),
                                 GiftAmount = g.Sum(x => x.Payment_Status_Code == "PSC02" ? x.Payment_Price : 0)
                             };
                var fItems = await fquery.FirstOrDefaultAsync();
                if (fItems != null)
                {
                    item.GiftCount = fItems.GiftCount;
                    item.GiftCancelCount = fItems.GiftCancelCount;
                    item.GiftAmount = fItems.GiftAmount;
                }
                result.Add(item);
            }
            return result.OrderByDescending(m => m.Date).ToList();
        }

        /// <summary>
        /// 매출 현황 - 월
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task<List<FlagiftSaleStatistic>> GetSalesStatisticMonthsAsync(DateTime date)
        {
            var result = new List<FlagiftSaleStatistic>();
            var startDate = new DateTime(date.Year, 1, 1);
            var enddate = startDate.AddYears(1);
            if (enddate > DateTime.Now)
                enddate = DateTime.Now.Date;

            var dateRange = new Dictionary<DateTime, DateTime>();
            //월 생성
            while (startDate < enddate)
            {
                var nextdate = startDate.AddMonths(1);
                dateRange.Add(startDate, nextdate);
                startDate = nextdate;
            }

            foreach (var workMonth in dateRange.OrderByDescending(m => m.Key))
            {
                var item = new FlagiftSaleStatistic
                {
                    Date = workMonth.Key.ToString("yyyy-MM")
                };
                var bquery = from m in _barunsonDb.TB_Orders
                             join i in _barunsonDb.TB_Invitations on m.Order_ID equals i.Order_ID
                             join d in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals d.Invitation_ID
                             where m.Payment_DateTime >= workMonth.Key && m.Payment_DateTime < workMonth.Value
                                 && m.Payment_Status_Code == "PSC02"
                             group d by 1 into g
                             select new
                             {
                                 SetCount = g.Sum(x => x.Flower_gift_YN == "Y" ? 1 : 0),
                                 SetCancelCount = g.Sum(x => x.Flower_gift_YN == "C" ? 1 : 0)
                             };
                var barunItems = await bquery.FirstOrDefaultAsync();
                if (barunItems != null) 
                {
                    item.SetCount = barunItems.SetCount;
                    item.SetCancelCount = barunItems.SetCancelCount;
                }
                var fquery = from m in _barunsonDb.TB_Order_PartnerShip
                             where m.P_OrderDate >= workMonth.Key && m.P_OrderDate < workMonth.Value
                                 && m.P_Id == PID
                             group m by 1 into g
                             select new
                             {
                                 GiftCount = g.Sum(x => x.Payment_Status_Code == "PSC02" ? 1 : 0),
                                 GiftCancelCount = g.Sum(x => x.Payment_Status_Code == "PSC03" ? 1 : 0),
                                 GiftAmount = g.Sum(x => x.Payment_Status_Code == "PSC02" ? x.Payment_Price : 0)
                             };
                var fItems = await fquery.FirstOrDefaultAsync();
                if (fItems != null)
                {
                    item.GiftCount = fItems.GiftCount;
                    item.GiftCancelCount = fItems.GiftCancelCount;
                    item.GiftAmount = fItems.GiftAmount;
                }
                
                result.Add(item);
            }
            return result.OrderByDescending(m => m.Date).ToList();
        }

        /// <summary>
        /// 주문별 검색
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<(int Count, List<FlaOrderSearchDataModel> Items)> GetOrderListAsync(string keyword, string period, DateTime startDate, DateTime endDate,
            int pageFrom = 0, int? pageSize = null)
        {
            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);
            //총 아이템 수
            int count = 0;
            List<FlaOrderSearchDataModel> items = null;

            var orderQuery = from o in _barunsonDb.TB_Orders
                             join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                             join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                             where ((period == "Order" && o.Order_DateTime >= startDate && o.Order_DateTime < endDate) ||
                                 (period == "Wedding" && id.WeddingDate.CompareTo(startDate.ToString("yyyy-MM-dd")) >= 0 && 
                                  id.WeddingDate.CompareTo(endDate.ToString("yyyy-MM-dd")) < 0)) &&
                                 id.Flower_gift_YN == "Y" &&
                                 (string.IsNullOrEmpty(keyword) || (o.User_ID.Contains(keyword) || o.Name.Contains(keyword) || o.Email.Contains(keyword) || o.Order_Code.Contains(keyword))) 
                             select new 
                             {
                                 UserId = o.User_ID,
                                 UserName = o.Name,
                                 UserEmail = o.Email,
                                 OrderID = o.Order_ID,
                                 o.Order_Code,
                                 OrderDate = o.Order_DateTime,
                                 WeddingDate = id.WeddingDate,
                                 id.WeddingHour,
                                 id.WeddingMin,
                                 id.Time_Type_Code,
                                 id.Invitation_URL,
                             };
            if (period == "Order")
                orderQuery = orderQuery.OrderByDescending(m => m.OrderDate);
            else if (period == "Wedding")
                orderQuery = orderQuery.OrderByDescending(m => m.WeddingDate).ThenByDescending(m => m.WeddingHour);
            else
                orderQuery = orderQuery.OrderByDescending(m => m.OrderID);

            if (pageSize.HasValue)
            {
                //총 아이템 수
                count = await orderQuery.CountAsync();
                //페이지 수 만큼 데이터 읽기
                orderQuery = orderQuery.Skip(pageFrom).Take(pageSize.Value);
                
            }
            var queryItems = await orderQuery.ToListAsync();
            items = queryItems.Select(x =>
                        new FlaOrderSearchDataModel
                        {
                            MemberType = (x.UserId == "") ? "G" : "U",
                            UserId = x.UserId,
                            UserName = x.UserName,
                            UserEmail = x.UserEmail,
                            OrderID = x.OrderID,
                            OrderCode = x.Order_Code,
                            OrderDate = x.OrderDate,
                            WeddingDate = GetWeddingDate(x.WeddingDate, x.WeddingHour, x.WeddingMin, x.Time_Type_Code),
                            Invitation_Url = x.Invitation_URL
                        }
                    ).ToList();
            if (count == 0)
                count = items.Count;

            //화환 통계
            foreach(var item in items)
            {
                var fquery = from m in _barunsonDb.TB_Order_PartnerShip
                           where m.Order_ID == item.OrderID && m.P_Id == PID
                           group m by 1 into g
                           select new
                           {
                               GiftCount = g.Sum(x => x.Payment_Status_Code == "PSC02" ? 1 : 0),
                               GiftAmount = g.Sum(x => x.Payment_Status_Code == "PSC02" ? x.Payment_Price : 0)
                           };
                var fItems = await fquery.FirstOrDefaultAsync();
                if (fItems != null)
                {
                    item.FlaGiftCount = fItems.GiftCount;
                    item.FlaGiftAmount = fItems.GiftAmount;
                }
            }
            return (count, items);

        }

        /// <summary>
        /// 건별 검색
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<(int Count, List<FlaOrderDataModel> Items)> GetFlaOrderListAsync(string keyword, string period, DateTime startDate, DateTime endDate,
            int pageFrom = 0, int? pageSize = null)
        {
            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);
            //총 아이템 수
            int count = 0;
            List<FlaOrderDataModel> items = new List<FlaOrderDataModel>();

            var orderQuery = from f in _barunsonDb.TB_Order_PartnerShip
                             join o in _barunsonDb.TB_Orders on f.Order_ID equals o.Order_ID
                             join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                             join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                             where f.P_Id == PID && f.Payment_Status_Code == "PSC02" &&
                                 ((period == "Order" && f.P_OrderDate >= startDate && f.P_OrderDate < endDate) ||
                                  (period == "Wedding" && id.WeddingDate.CompareTo(startDate.ToString("yyyy-MM-dd")) >= 0 &&
                                 id.WeddingDate.CompareTo(endDate.ToString("yyyy-MM-dd")) < 0)) &&
                                 (string.IsNullOrEmpty(keyword) || (o.User_ID.Contains(keyword) || f.P_Order_Name.Contains(keyword) || o.Email.Contains(keyword) || o.Order_Code.Contains(keyword)))
                             select new
                             {
                                 UserId = o.User_ID,
                                 UserName = o.Name,
                                 UserEmail = o.Email,
                                 OrderID = o.Order_ID,
                                 o.Order_Code,
                                 WeddingDate = id.WeddingDate,
                                 id.WeddingHour,
                                 id.WeddingMin,
                                 id.Time_Type_Code,
                                 id.Invitation_URL,
                                 
                                 OrderName = f.P_Order_Name,
                                 ProductName = f.P_ProductName,
                                 SalePrice = f.Payment_Price,
                                 OrderDate = f.P_OrderDate,
                                 OrderState = f.P_OrderState,
                                 f.P_ExtendData,
                                 f.P_OrderCode,
                                 f.Payment_DateTime
                             };
            if (period == "Order")
                orderQuery = orderQuery.OrderByDescending(m => m.OrderDate);
            else if (period == "Wedding")
                orderQuery = orderQuery.OrderByDescending(m => m.WeddingDate).ThenByDescending(m => m.WeddingHour);
            else
                orderQuery = orderQuery.OrderByDescending(m => m.OrderID);

            if (pageSize.HasValue)
            {
                //총 아이템 수
                count = await orderQuery.CountAsync();
                //페이지 수 만큼 데이터 읽기
                orderQuery = orderQuery.Skip(pageFrom).Take(pageSize.Value);

            }
            var queryItems = await orderQuery.ToListAsync();
            foreach (var item in queryItems)
            {
                var jsonData = JObject.Parse(item.P_ExtendData);
                var data = new FlaOrderDataModel
                {
                    MemberType = (item.UserId == "") ? "G" : "U",
                    UserId = item.UserId,
                    UserName = item.UserName,
                    UserEmail = item.UserEmail,
                    OrderID = item.OrderID,
                    OrderCode = item.Order_Code,
                    OrderDate = item.OrderDate,
                    WeddingDate = GetWeddingDate(item.WeddingDate, item.WeddingHour, item.WeddingMin, item.Time_Type_Code),
                    Invitation_Url = item.Invitation_URL,
                    OrderTitle = (string)jsonData["deli_title"],
                    OrderName = item.OrderName,
                    ProductName = item.ProductName,
                    SalePrice = item.SalePrice,
                    OrderStateName = item.OrderState,
                    POrderCode = item.P_OrderCode,
                    PPaymentDate = item.Payment_DateTime
                };
                DateTime deliTime;
                if (DateTime.TryParse((string)jsonData["deli_time"], out deliTime))
                    data.DeliveryDate = deliTime;

                items.Add(data);
            }
                
            if (count == 0)
                count = items.Count;

            
            return (count, items);

        }

        /// <summary>
        /// 취소 환불 검색
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<(int Count, List<FlaRefundDataModel> Items)> GetFlaRefundListAsync(string keyword, string period, DateTime startDate, DateTime endDate,
            int pageFrom = 0, int? pageSize = null)
        {
            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);
            //총 아이템 수
            int count = 0;
            var items = new List<FlaRefundDataModel>();

            var orderQuery = from f in _barunsonDb.TB_Order_PartnerShip
                             join o in _barunsonDb.TB_Orders on f.Order_ID equals o.Order_ID
                             join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                             join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                             where f.P_Id == PID && f.Payment_Status_Code == "PSC03" &&
                                 ((period == "Order" && f.P_OrderDate >= startDate && f.P_OrderDate < endDate) ||
                                  (period == "Refund" && f.Refund_DateTime >= startDate && f.Refund_DateTime < endDate)) &&
                                 (string.IsNullOrEmpty(keyword) || (o.User_ID.Contains(keyword) || f.P_Order_Name.Contains(keyword) || o.Email.Contains(keyword) || o.Order_Code.Contains(keyword)))
                             select new
                             {
                                 UserId = o.User_ID,
                                 UserName = o.Name,
                                 UserEmail = o.Email,
                                 OrderID = o.Order_ID,
                                 o.Order_Code,
                                 WeddingDate = id.WeddingDate,
                                 id.WeddingHour,
                                 id.WeddingMin,
                                 id.Time_Type_Code,
                                 id.Invitation_URL,

                                 OrderName = f.P_Order_Name,
                                 ProductName = f.P_ProductName,
                                 SalePrice = f.Payment_Price,
                                 OrderDate = f.P_OrderDate,
                                 RefundDate = f.Refund_DateTime,
                                 f.Payment_Method_Code,
                                 f.Payment_Status_Code,
                                 f.P_ExtendData,
                                 f.P_OrderCode,
                                 f.Payment_DateTime
                             };
            if (period == "Order")
                orderQuery = orderQuery.OrderByDescending(m => m.OrderDate);
            else if (period == "Refund")
                orderQuery = orderQuery.OrderByDescending(m => m.RefundDate);
            else
                orderQuery = orderQuery.OrderByDescending(m => m.OrderID);

            if (pageSize.HasValue)
            {
                //총 아이템 수
                count = await orderQuery.CountAsync();
                //페이지 수 만큼 데이터 읽기
                orderQuery = orderQuery.Skip(pageFrom).Take(pageSize.Value);

            }
            var queryItems = await orderQuery.ToListAsync();

            var Payment_Status_Codes = await GetCommonCodeAsync("Payment_Status_Code");
            var Payment_Method_Codes = await GetCommonCodeAsync("Payment_Method_Code");
            foreach (var item in queryItems)
            {
                var jsonData = JObject.Parse(item.P_ExtendData);
                var data = new FlaRefundDataModel
                {
                    MemberType = (item.UserId == "") ? "G" : "U",
                    UserId = item.UserId,
                    UserName = item.UserName,
                    UserEmail = item.UserEmail,
                    OrderID = item.OrderID,
                    OrderCode = item.Order_Code,
                    OrderDate = item.OrderDate,
                    WeddingDate = GetWeddingDate(item.WeddingDate, item.WeddingHour, item.WeddingMin, item.Time_Type_Code),
                    Invitation_Url = item.Invitation_URL,
                    OrderTitle = (string)jsonData["deli_title"],
                    OrderName = item.OrderName,
                    ProductName = item.ProductName,
                    SalePrice = item.SalePrice,
                    RefundDate = item.RefundDate,
                    Payment_Status_Code = item.Payment_Status_Code,
                    Payment_Status_Name = Payment_Status_Codes.FirstOrDefault(x => x.Code == item.Payment_Status_Code)?.Code_Name,
                    Payment_Method_Code = item.Payment_Method_Code,
                    Payment_Method_Name = Payment_Method_Codes.FirstOrDefault(x => x.Code == item.Payment_Method_Code)?.Code_Name,
                    POrderCode = item.P_OrderCode,
                    PPaymentDate = item.Payment_DateTime

                };

                items.Add(data);
            }

            if (count == 0)
                count = items.Count;


            return (count, items);

        }
        
        #endregion
    }
}
