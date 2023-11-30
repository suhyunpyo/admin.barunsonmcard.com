using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Dac.Contexts;
using Barunn.MobileInvitation.Dac.Models.Barunson;
using Barunn.MobileInvitation.Web.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    [Authorize]
    public class StatisticsController : BaseController
    {
        #region 생성자
        public StatisticsController(ILogger<StatisticsController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
        }
        #endregion

        #region 내부 함수
        
        /// <summary>
        /// 전체 현황 - 일별 통계
        /// </summary>
        /// <param name="yyyymm">년월(202101)</param>
        /// <returns></returns>
        private async Task<List<TB_Total_Statistic_Day>> GetTotalStatisticDaysAsync(DateTime date)
        {
            var query = from r in _barunsonDb.TB_Total_Statistic_Days
                        where r.Date.StartsWith(date.ToString("yyyyMM"))
                        orderby r.Date descending
                        select r;

            return await query.ToListAsync();
        }

        /// <summary>
        /// 전체 현황 - 월별 통계
        /// </summary>
        /// <param name="yyyy">년(2021)</param>
        /// <returns></returns>
        private async Task<List<TB_Total_Statistic_Month>> GetTotalStatisticMonthsAsync(DateTime date)
        {
            var query = from r in _barunsonDb.TB_Total_Statistic_Months
                        where r.Date.StartsWith(date.ToString("yyyy"))
                        orderby r.Date descending
                        select r;

            return await query.ToListAsync();
        }

        /// <summary>
        /// 매출 현황 - 일별
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task<List<TB_Sales_Statistic_Day>> GetSalesStatisticDaysAsync(DateTime date)
        {
            var query = from r in _barunsonDb.TB_Sales_Statistic_Days
                        where r.Date.StartsWith(date.ToString("yyyyMM"))
                        orderby r.Date descending
                        select r;

            return await query.ToListAsync();
        }
        /// <summary>
        /// 매출 현황 - 월별
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task<List<TB_Sales_Statistic_Month>> GetSalesStatisticMonthsAsync(DateTime date)
        {
            var query = from r in _barunsonDb.TB_Sales_Statistic_Months
                        where r.Date.StartsWith(date.ToString("yyyy"))
                        orderby r.Date descending
                        select r;

            return await query.ToListAsync();
        }

        /// <summary>
        /// 구매 패턴
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private async Task<StatisticsOrderPattern> GetStatisticsOrderPatternAsync(DateTime startDate, DateTime endDate)
        {
            var orderPattern = new StatisticsOrderPattern();

            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);

            //MCard, Multi Order query
            var orderQuery = from o in _barunsonDb.TB_Orders
                             where o.Order_DateTime > startDate && o.Order_DateTime < endDate
                             group o by o.User_ID into og
                             select new { User_ID = og.Key, OrderCount = og.Count() };
            var orderItem = await orderQuery.ToListAsync();
            var userIds = orderItem.Select(m => m.User_ID).ToList();

            var weddingCardQuery = from a in _barshopDb.custom_orders
                                   where a.status_seq > 9 && userIds.Contains(a.member_id)
                                   group a by a.member_id into g
                                   select new { User_ID = g.Key };
            var weddingCardItem = await weddingCardQuery.ToListAsync();

            int McardOrder1Count, McardOrder2Count, MultiOrder1Count, MultiOrder2Count;
            McardOrder1Count = McardOrder2Count = MultiOrder1Count = MultiOrder2Count = 0;

            foreach(var item in orderItem)
            {
                var weddingCard = weddingCardItem.Exists(m => m.User_ID.Equals(item.User_ID, StringComparison.InvariantCultureIgnoreCase));

                if (weddingCard && item.OrderCount > 1)
                    MultiOrder2Count++;
                else if (weddingCard && item.OrderCount == 1)
                    MultiOrder1Count++;
                else if (!weddingCard && item.OrderCount > 1)
                    McardOrder2Count++;
                else if (!weddingCard && item.OrderCount == 1)
                    McardOrder1Count++;
            }
            orderPattern.McardOrder1Count = McardOrder1Count;
            orderPattern.McardOrder2Count = McardOrder2Count;
            orderPattern.MultiOrder1Count = MultiOrder1Count;
            orderPattern.MultiOrder2Count = MultiOrder2Count;
            

            //OrderUserCount
            var query2 = from o in _barshopDb.custom_orders
                         where o.status_seq == 15 && o.src_packing_date > startDate && o.src_packing_date < endDate
                         group o by o.member_id into og
                         select og.Key;

            orderPattern.OrderUserCount = await query2.CountAsync();

            //join_count
            var query3 = from o in _barshopDb.S2_UserInfo_TheCards
                         where o.reg_date > startDate && o.reg_date < endDate
                         group o by o.uid into og
                         select og.Key;
            orderPattern.JoinCount = await query3.CountAsync();

            return orderPattern;
        }

        /// <summary>
        /// 결제 수단 - 일별
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task<List<TB_Payment_Status_Day>> GetPaymentStatusDayAsync(DateTime date)
        {
            var query = from r in _barunsonDb.TB_Payment_Status_Days
                        where r.Date.StartsWith(date.ToString("yyyyMM"))
                        orderby r.Date descending
                        select r;

            return await query.ToListAsync();
        }
        /// <summary>
        /// 결제 수단 - 월별
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task<List<TB_Payment_Status_Month>> GetPaymentStatusMonthAsync(DateTime date)
        {
            var query = from r in _barunsonDb.TB_Payment_Status_Months
                        where r.Date.StartsWith(date.ToString("yyyy"))
                        orderby r.Date descending
                        select r;

            return await query.ToListAsync();
        }

        /// <summary>
        /// 제품 통계
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="barans"></param>
        /// <param name="searchKeyword"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        private async Task<List<StatisticsOrderProduct>> GetStatisticsOrderProductsAsync(DateTime startDate, DateTime endDate, List<string> barans, string searchKeyword, string orderby)
        {
            var list = new List<StatisticsOrderProduct>();

            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);

            if (barans == null || barans.Count == 0)
            {
                barans.AddRange(new string[]
                {
                    "PBC01",
                    "PBC02",
                    "PBC03",
                    "PBC04"
                });
            }

            var query = from o in _barunsonDb.TB_Orders
                        join op in _barunsonDb.TB_Order_Products
                            on o.Order_ID equals op.Order_ID
                        join p in _barunsonDb.TB_Products
                            on op.Product_ID equals p.Product_ID
                        where o.Order_DateTime > startDate &&
                              o.Order_DateTime < endDate &&
                              o.Payment_Status_Code == "PSC02" &&
                              barans.Contains(p.Product_Brand_Code)
                        group o by new { p.Product_Brand_Code, p.Product_Code, p.Product_Name } into g
                        select new
                        {
                            g.Key.Product_Brand_Code,
                            g.Key.Product_Code,
                            g.Key.Product_Name,
                            PayCount = g.Sum(m => (m.Payment_Price > 0) ? 1 : 0),
                            FreeCount = g.Sum(m => (m.Payment_Price == null || m.Payment_Price == 0) ? 1 : 0),
                            TotalCount = g.Count()
                        } into r
                        join cd in _barunsonDb.TB_Common_Codes
                            on r.Product_Brand_Code equals cd.Code
                        where cd.Code_Group == "Product_Brand_Code"
                        select new StatisticsOrderProduct
                        {
                            ProductBrandCode = r.Product_Brand_Code,
                            ProductBrandName = cd.Code_Name,
                            ProductCode = r.Product_Code,
                            ProductName = r.Product_Name,
                            PayCount = r.PayCount,
                            FreeCount = r.FreeCount,
                            TotalCount = r.TotalCount
                        };
            if (!string.IsNullOrEmpty(searchKeyword))
                query = query.Where(m => m.ProductName.Contains(searchKeyword) || m.ProductCode.Contains(searchKeyword));

            switch (orderby)
            {
                case "brand_asc":
                    query = query.OrderBy(m => m.ProductBrandCode);
                    break;
                case "brand_desc":
                    query = query.OrderByDescending(m => m.ProductBrandCode);
                    break;
                case "code_asc":
                    query = query.OrderBy(m => m.ProductCode);
                    break;
                case "code_desc":
                    query = query.OrderByDescending(m => m.ProductCode);
                    break;
                case "free_asc":
                    query = query.OrderBy(m => m.FreeCount);
                    break;
                case "free_desc":
                    query = query.OrderByDescending(m => m.FreeCount);
                    break;
                case "pay_asc":
                    query = query.OrderBy(m => m.PayCount);
                    break;
                case "pay_desc":
                    query = query.OrderByDescending(m => m.PayCount);
                    break;
                case "total_asc":
                    query = query.OrderBy(m => m.TotalCount);
                    break;
                case "total_desc":
                    query = query.OrderByDescending(m => m.TotalCount);
                    break;
                default:
                    break;
            }

            return await query.ToListAsync();
        }

        #endregion

        #region 전체 현황

        /// <summary>
        ///  전체 현황 - 일별 통계
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TotalDailyAsync(int? year, int? month)
        {
            var targetDate = GetTargetDate(year, month);

            var model = new StatisticsViewModel<TB_Total_Statistic_Day>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                Items = await GetTotalStatisticDaysAsync(targetDate),
                Total = new TB_Total_Statistic_Day()
            };
            model.Total.Free_Order_Count = model.Items.Sum(m => m.Free_Order_Count);
            model.Total.Charge_Order_Count = model.Items.Sum(m => m.Charge_Order_Count);
            model.Total.Cancel_Count = model.Items.Sum(m => m.Cancel_Count);
            model.Total.Payment_Price = model.Items.Sum(m => m.Payment_Price);
            model.Total.Cancel_Refund_Price = model.Items.Sum(m => m.Cancel_Refund_Price);
            model.Total.Profit_Price = model.Items.Sum(m => m.Profit_Price);
            model.Total.Memberjoin_Count = model.Items.Sum(m => m.Memberjoin_Count);

            return View(model);
        }

        /// <summary>
        ///  전체 현황 - 일별 통계 엑셀
        /// </summary>
        /// <param name="year">년</param>
        /// <param name="month">월</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TotalDailyExcelAsync(int? year, int? month)
        {
            var targetDate = GetTargetDate(year, month);

            var items = await GetTotalStatisticDaysAsync(targetDate);

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "날짜";
            workSheet.Cells[rowIndex, colIndex++].Value = "무료주문수";
            workSheet.Cells[rowIndex, colIndex++].Value = "유료주문수";
            workSheet.Cells[rowIndex, colIndex++].Value = "취소/환불주문수";
            workSheet.Cells[rowIndex, colIndex++].Value = "매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "취소/환불액";
            workSheet.Cells[rowIndex, colIndex++].Value = "순매출";
            workSheet.Cells[rowIndex, colIndex++].Value = "회원가입수";


            foreach (var data in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Cancel_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Payment_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Cancel_Refund_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Profit_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Memberjoin_Count;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TotalDaily_" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }

        /// <summary>
        /// 전체 현황 - 월별 통계
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TotalMonthlyAsync(int? year)
        {
            var targetDate = GetTargetDate(year);

            var model = new StatisticsViewModel<TB_Total_Statistic_Month>
            {
                SelectedYear = targetDate.Year,
                Items = await GetTotalStatisticMonthsAsync(targetDate),
                Total = new TB_Total_Statistic_Month()
            };
            model.Total.Free_Order_Count = model.Items.Sum(m => m.Free_Order_Count);
            model.Total.Charge_Order_Count = model.Items.Sum(m => m.Charge_Order_Count);
            model.Total.Cancel_Count = model.Items.Sum(m => m.Cancel_Count);
            model.Total.Payment_Price = model.Items.Sum(m => m.Payment_Price);
            model.Total.Cancel_Refund_Price = model.Items.Sum(m => m.Cancel_Refund_Price);
            model.Total.Profit_Price = model.Items.Sum(m => m.Profit_Price);
            model.Total.Memberjoin_Count = model.Items.Sum(m => m.Memberjoin_Count);

            return View(model);
        }
        /// <summary>
        /// 전체 현황 - 월별 통계 엑셀
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TotalMonthlyExcelAsync(int? year)
        {
            var targetDate = GetTargetDate(year);

            var items = await GetTotalStatisticMonthsAsync(targetDate);

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "날짜";
            workSheet.Cells[rowIndex, colIndex++].Value = "무료주문수";
            workSheet.Cells[rowIndex, colIndex++].Value = "유료주문수";
            workSheet.Cells[rowIndex, colIndex++].Value = "취소/환불주문수";
            workSheet.Cells[rowIndex, colIndex++].Value = "매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "취소/환불액";
            workSheet.Cells[rowIndex, colIndex++].Value = "순매출";
            workSheet.Cells[rowIndex, colIndex++].Value = "회원가입수";


            foreach (var data in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Cancel_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Payment_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Cancel_Refund_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Profit_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Memberjoin_Count;
            }
            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TotalMonthly_" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }
        #endregion

        #region 매출통계
        /// <summary>
        /// 매출 통계 - 일별
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesDailyAsync(int? year, int? month)
        {
            var targetDate = GetTargetDate(year, month);

            var model = new StatisticsViewModel<TB_Sales_Statistic_Day>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                Items = await GetSalesStatisticDaysAsync(targetDate),
                Total = new TB_Sales_Statistic_Day()
            };
            model.Total.Barunn_Charge_Order_Count = model.Items.Sum(m => m.Barunn_Charge_Order_Count);
            model.Total.Barunn_Free_Order_Count = model.Items.Sum(m => m.Barunn_Free_Order_Count);
            model.Total.Barunn_Sales_Price = model.Items.Sum(m => m.Barunn_Sales_Price);
            model.Total.Bhands_Charge_Order_Count = model.Items.Sum(m => m.Bhands_Charge_Order_Count);
            model.Total.Bhands_Free_Order_Count = model.Items.Sum(m => m.Bhands_Free_Order_Count);
            model.Total.Bhands_Sales_Price = model.Items.Sum(m => m.Bhands_Sales_Price);
            model.Total.Thecard_Charge_Order_Count = model.Items.Sum(m => m.Thecard_Charge_Order_Count);
            model.Total.Thecard_Free_Order_Count = model.Items.Sum(m => m.Thecard_Free_Order_Count);
            model.Total.Thecard_Sales_Price = model.Items.Sum(m => m.Thecard_Sales_Price);
            model.Total.Premier_Charge_Order_Count = model.Items.Sum(m => m.Premier_Charge_Order_Count);
            model.Total.Premier_Free_Order_Count = model.Items.Sum(m => m.Premier_Free_Order_Count);
            model.Total.Premier_Sales_Price = model.Items.Sum(m => m.Premier_Sales_Price);
            model.Total.Total_Charge_Order_Count = model.Items.Sum(m => m.Total_Charge_Order_Count);
            model.Total.Total_Free_Order_Count = model.Items.Sum(m => m.Total_Free_Order_Count);
            model.Total.Total_Sales_Price = model.Items.Sum(m => m.Total_Sales_Price);

            return View(model);
        }
        /// <summary>
        /// 매출 통계 - 일별 엑셀
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesDailyExcelAsync(int? year, int? month)
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

            workSheet.Cells[rowIndex, colIndex++].Value = "날짜";
            workSheet.Cells[rowIndex, colIndex++].Value = "바른손-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "바른손-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "바른손-매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "비핸즈-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "비핸즈-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "비핸즈-매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "더카드-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "더카드-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "더카드-매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "프리미어페이퍼-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "프리미어페이퍼-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "프리미어페이퍼-매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "전체-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "전체-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "전체-매출액";


            foreach (TB_Sales_Statistic_Day data in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Barunn_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Barunn_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Barunn_Sales_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Bhands_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Bhands_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Bhands_Sales_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Thecard_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Thecard_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Thecard_Sales_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Premier_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Premier_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Premier_Sales_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Total_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Total_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Total_Sales_Price;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesDaily_" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }

        /// <summary>
        /// 매출 통계 - 월별
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesMonthlyAsync(int? year)
        {
            var targetDate = GetTargetDate(year);

            var model = new StatisticsViewModel<TB_Sales_Statistic_Month>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                Items = await GetSalesStatisticMonthsAsync(targetDate),
                Total = new TB_Sales_Statistic_Month()
            };
            model.Total.Barunn_Charge_Order_Count = model.Items.Sum(m => m.Barunn_Charge_Order_Count);
            model.Total.Barunn_Free_Order_Count = model.Items.Sum(m => m.Barunn_Free_Order_Count);
            model.Total.Barunn_Sales_Price = model.Items.Sum(m => m.Barunn_Sales_Price);
            model.Total.Bhands_Charge_Order_Count = model.Items.Sum(m => m.Bhands_Charge_Order_Count);
            model.Total.Bhands_Free_Order_Count = model.Items.Sum(m => m.Bhands_Free_Order_Count);
            model.Total.Bhands_Sales_Price = model.Items.Sum(m => m.Bhands_Sales_Price);
            model.Total.Thecard_Charge_Order_Count = model.Items.Sum(m => m.Thecard_Charge_Order_Count);
            model.Total.Thecard_Free_Order_Count = model.Items.Sum(m => m.Thecard_Free_Order_Count);
            model.Total.Thecard_Sales_Price = model.Items.Sum(m => m.Thecard_Sales_Price);
            model.Total.Premier_Charge_Order_Count = model.Items.Sum(m => m.Premier_Charge_Order_Count);
            model.Total.Premier_Free_Order_Count = model.Items.Sum(m => m.Premier_Free_Order_Count);
            model.Total.Premier_Sales_Price = model.Items.Sum(m => m.Premier_Sales_Price);
            model.Total.Total_Charge_Order_Count = model.Items.Sum(m => m.Total_Charge_Order_Count);
            model.Total.Total_Free_Order_Count = model.Items.Sum(m => m.Total_Free_Order_Count);
            model.Total.Total_Sales_Price = model.Items.Sum(m => m.Total_Sales_Price);

            return View(model);
        }

        /// <summary>
        /// 매출 통계 - 월별 엑셀
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesMonthlyExcelAsync(int? year)
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

            workSheet.Cells[rowIndex, colIndex++].Value = "날짜";
            workSheet.Cells[rowIndex, colIndex++].Value = "바른손-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "바른손-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "바른손-매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "비핸즈-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "비핸즈-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "비핸즈-매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "더카드-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "더카드-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "더카드-매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "프리미어페이퍼-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "프리미어페이퍼-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "프리미어페이퍼-매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "전체-유료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "전체-무료수";
            workSheet.Cells[rowIndex, colIndex++].Value = "전체-매출액";


            foreach (var data in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Barunn_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Barunn_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Barunn_Sales_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Bhands_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Bhands_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Bhands_Sales_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Thecard_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Thecard_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Thecard_Sales_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Premier_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Premier_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Premier_Sales_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Total_Charge_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Total_Free_Order_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Total_Sales_Price;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesMonthly_" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }
        #endregion

        #region 구매 패턴

        /// <summary>
        /// 구매 패턴
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public async Task<IActionResult> PurchasePatternAsync(StatisticsPurchaseViewModel model)
        {
            if (model == null)
                model = new StatisticsPurchaseViewModel();

            model.Item = await GetStatisticsOrderPatternAsync(model.StartDate, model.EndDate);

            return View(model);
        }
        #endregion

        #region 결제 수단
        /// <summary>
        /// 결제 수단 - 일별
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PayTypeDailyAsync(int? year, int? month)
        {
            var targetDate = GetTargetDate(year, month);

            var model = new StatisticsViewModel<TB_Payment_Status_Day>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                Items = await GetPaymentStatusDayAsync(targetDate),
                Total = new TB_Payment_Status_Day()
            };
            model.Total.Card_Payment_Price = model.Items.Sum(m => m.Card_Payment_Price);
            model.Total.Account_Transfer_Price = model.Items.Sum(m => m.Account_Transfer_Price);
            model.Total.Virtual_Account_Price = model.Items.Sum(m => m.Virtual_Account_Price);
            model.Total.Etc_Price = model.Items.Sum(m => m.Etc_Price);
            model.Total.Total_Price = model.Items.Sum(m => m.Total_Price);
            model.Total.Cancel_Refund_Price = model.Items.Sum(m => m.Cancel_Refund_Price);
            model.Total.Profit_Price = model.Items.Sum(m => m.Profit_Price);

            return View(model);
        }

        /// <summary>
        /// 결제 수단 - 일별 엑셀
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public async Task<IActionResult> PayTypeDailyExcelAsync(int? year, int? month)
        {
            var targetDate = GetTargetDate(year, month);
            var items = await GetPaymentStatusDayAsync(targetDate);

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "날짜";
            workSheet.Cells[rowIndex, colIndex++].Value = "카드결제";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌이체";
            workSheet.Cells[rowIndex, colIndex++].Value = "무통장";
            workSheet.Cells[rowIndex, colIndex++].Value = "기타";
            workSheet.Cells[rowIndex, colIndex++].Value = "전체";
            workSheet.Cells[rowIndex, colIndex++].Value = "취소/환불";
            workSheet.Cells[rowIndex, colIndex++].Value = "순매출";


            foreach (var data in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Card_Payment_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Account_Transfer_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Virtual_Account_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Etc_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Total_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Cancel_Refund_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Profit_Price;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PayTypeDaily_" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }

        /// <summary>
        /// 결제 수단 - 월별
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<IActionResult> PayTypeMonthlyAsync(int? year)
        {
            var targetDate = GetTargetDate(year);
            var model = new StatisticsViewModel<TB_Payment_Status_Month>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                Items = await GetPaymentStatusMonthAsync(targetDate),
                Total = new TB_Payment_Status_Month()
            };
            model.Total.Card_Payment_Price = model.Items.Sum(m => m.Card_Payment_Price);
            model.Total.Account_Transfer_Price = model.Items.Sum(m => m.Account_Transfer_Price);
            model.Total.Virtual_Account_Price = model.Items.Sum(m => m.Virtual_Account_Price);
            model.Total.Etc_Price = model.Items.Sum(m => m.Etc_Price);
            model.Total.Total_Price = model.Items.Sum(m => m.Total_Price);
            model.Total.Cancel_Refund_Price = model.Items.Sum(m => m.Cancel_Refund_Price);
            model.Total.Profit_Price = model.Items.Sum(m => m.Profit_Price);

            return View(model);
        }

        /// <summary>
        ///  결제 수단 - 월별 엑셀
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<IActionResult> PayTypeMonthlyExcelAsync(int? year)
        {
            var targetDate = GetTargetDate(year);
            var Items = await GetPaymentStatusMonthAsync(targetDate);

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "날짜";
            workSheet.Cells[rowIndex, colIndex++].Value = "카드결제";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌이체";
            workSheet.Cells[rowIndex, colIndex++].Value = "무통장";
            workSheet.Cells[rowIndex, colIndex++].Value = "기타";
            workSheet.Cells[rowIndex, colIndex++].Value = "전체";
            workSheet.Cells[rowIndex, colIndex++].Value = "취소/환불";
            workSheet.Cells[rowIndex, colIndex++].Value = "순매출";

            foreach (var data in Items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Card_Payment_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Account_Transfer_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Virtual_Account_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Etc_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Total_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Cancel_Refund_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = data.Profit_Price;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PayTypeMonthly_" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }
        #endregion

        #region 제품통계

        /// <summary>
        /// 제품 통계
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public async Task<IActionResult> ProductsAsync(StatisticsProductsViewModel model)
        {
            if (model == null)
                model = new StatisticsProductsViewModel();

            //브랜드 코드
            if (model.Barans == null || model.Barans.Count == 0)
            {
                var brands = await GetCommonCodeAsync("Product_Brand_Code");

                model.Barans = brands.Select(x =>
                 new SelectListItem
                 {
                     Text = x.Code_Name,
                     Value = x.Code,
                     Selected = true
                 })
                .ToList();
            }

            model.Items = await GetStatisticsOrderProductsAsync(
                model.StartDate, model.EndDate,
                model.Barans.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.Keyword, model.SelectedOrderby
                );

            return View(model);
        }

        public async Task<IActionResult> ProductsExcelAsync(StatisticsProductsViewModel model)
        {
            //브랜드 코드
            if (model.Barans == null || model.Barans.Count == 0)
            {
                var brands = await GetCommonCodeAsync("Product_Brand_Code");

                model.Barans = brands.Select(x =>
                 new SelectListItem
                 {
                     Text = x.Code_Name,
                     Value = x.Code,
                     Selected = true
                 })
                .ToList();
            }
            var list = await GetStatisticsOrderProductsAsync(
                model.StartDate, model.EndDate,
                model.Barans.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.Keyword, model.SelectedOrderby
                );

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "브랜드";
            workSheet.Cells[rowIndex, colIndex++].Value = "무료건수";
            workSheet.Cells[rowIndex, colIndex++].Value = "유료건수";
            workSheet.Cells[rowIndex, colIndex++].Value = "건수합계";


            foreach (var data in list)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.ProductBrandName;
                workSheet.Cells[rowIndex, colIndex++].Value = data.ProductCode;
                workSheet.Cells[rowIndex, colIndex++].Value = data.FreeCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.PayCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.TotalCount;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products_" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }
        #endregion

        #region 제작통계
        /// <summary>
        /// 제작통계
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProduceAsync(int? year)
        {
            var targetDate = GetTargetDate(year);
            var model = new StatisticsProduceViewModel<StatisticsOrderProduce>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                Items = await GetStatisticsProduce2Async(targetDate),
                Total = new StatisticsOrderProduce()
            };
            model.Total.CustomOrderCount = model.Items.Sum(m => m.CustomOrderCount);
            model.Total.MCardWithPaperCount = model.Items.Sum(m => m.MCardWithPaperCount);
            model.Total.MCardWithoutPaperCount = model.Items.Sum(m => m.MCardWithoutPaperCount);
            model.Total.KaKaoPayConfCount = model.Items.Sum(m => m.KaKaoPayConfCount);
            model.Total.KaKaoPayCancelCount = model.Items.Sum(m => m.KaKaoPayCancelCount);
            model.Total.RemitConfCount = model.Items.Sum(m => m.RemitConfCount);
            model.Total.RemitCancelCount = model.Items.Sum(m => m.RemitCancelCount);
            model.Total.AccountTotalAmount = model.Items.Sum(m => m.AccountTotalAmount);
            model.Total.FlowerConfCount = model.Items.Sum(m => m.FlowerConfCount);
            model.Total.FlowerCancelCount = model.Items.Sum(m => m.FlowerCancelCount);
            model.Total.FlowerTotalAmount = model.Items.Sum(m => m.FlowerTotalAmount);

            return View(model);
        }

        public async Task<IActionResult> ProduceExcelAsync(int? year)
        {
            var targetDate = GetTargetDate(year);
            var Items = await GetStatisticsProduce2Async(targetDate);

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

          
            workSheet.Cells[rowIndex, colIndex, rowIndex + 1, colIndex].Merge = true;
            workSheet.Cells[rowIndex, colIndex++].Value = "일자";

            workSheet.Cells[rowIndex, colIndex, rowIndex + 1, colIndex].Merge = true;
            workSheet.Cells[rowIndex, colIndex++].Value = "종이 청첩장 제작";

            workSheet.Cells[rowIndex, colIndex, rowIndex, colIndex + 2].Merge = true;
            workSheet.Cells[rowIndex, colIndex].Value = "모바일 청첩장 제작";

            workSheet.Cells[rowIndex, colIndex + 3, rowIndex, colIndex + 6].Merge = true;
            workSheet.Cells[rowIndex, colIndex + 3 ].Value = "계좌 설정";
            workSheet.Cells[rowIndex, colIndex + 7].Value = "카카오페이";

            workSheet.Cells[rowIndex, colIndex + 8, rowIndex, colIndex + 10].Merge = true;
            workSheet.Cells[rowIndex, colIndex + 8].Value = "화환설정";

            rowIndex++;

            workSheet.Cells[rowIndex, colIndex++].Value = "청첩장제작(O)";
            workSheet.Cells[rowIndex, colIndex++].Value = "청첩장제작(X)";
            workSheet.Cells[rowIndex, colIndex++].Value = "계";
            workSheet.Cells[rowIndex, colIndex++].Value = "카카오페이 설정";
            workSheet.Cells[rowIndex, colIndex++].Value = "카카오페이 취소";
            workSheet.Cells[rowIndex, colIndex++].Value = "일반송금 설정";
            workSheet.Cells[rowIndex, colIndex++].Value = "일반송금 취소";
            workSheet.Cells[rowIndex, colIndex++].Value = "거래액";
            workSheet.Cells[rowIndex, colIndex++].Value = "선물 설정";
            workSheet.Cells[rowIndex, colIndex++].Value = "선물 취소";
            workSheet.Cells[rowIndex, colIndex++].Value = "거래액";


            foreach (var data in Items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = data.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = data.CustomOrderCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.MCardWithPaperCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.MCardWithoutPaperCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.MCardTotalCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.KaKaoPayConfCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.KaKaoPayCancelCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.RemitConfCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.RemitCancelCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.AccountTotalAmount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.FlowerConfCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.FlowerCancelCount;
                workSheet.Cells[rowIndex, colIndex++].Value = data.FlowerTotalAmount;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Produce_" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }

        private async Task<List<StatisticsOrderProduce>> GetStatisticsProduce2Async(DateTime date)
        {
            var results = new List<StatisticsOrderProduce>();
            var now = DateTime.Now.Date;
            var startDate = new DateTime(date.Year, 1, 1);
            var dateRange = new Dictionary<DateTime, DateTime>();
            //월 생성
            do
            {
                var nextdate = startDate.AddMonths(1);
                dateRange.Add(startDate, nextdate);
                startDate = nextdate;
            }
            while (startDate < now);

            var ordertype = new string[] { "1", "6", "7" };

            foreach (var workMonth in dateRange.OrderByDescending(m => m.Key))
            {
                var model = new StatisticsOrderProduce { Date = workMonth.Key.ToString("yyyy-MM") };

                //종이 청첩장 수
                model.CustomOrderCount = await (from m in _barshopDb.custom_orders
                                                where m.src_packing_date >= workMonth.Key && m.src_packing_date < workMonth.Value
                                                    && m.up_order_seq == null && m.status_seq == 15
                                                select m.order_seq
                                               ).CountAsync();
                //모초 제작수
                var musers = await (from m in _barunsonDb.TB_Orders
                                    join i in _barunsonDb.TB_Invitations on m.Order_ID equals i.Order_ID
                                    join d in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals d.Invitation_ID
                                    where m.Payment_DateTime >= workMonth.Key && m.Payment_DateTime < workMonth.Value
                                        && m.Payment_Status_Code == "PSC02"
                                    select new { m.Order_ID, m.User_ID, m.Email, 
                                        d.Conf_KaKaoPay_YN, d.MoneyGift_Remit_Use_YN,
                                        d.Conf_Remit_YN, d.MoneyAccount_Div_Use_YN, d.MoneyAccount_Remit_Use_YN,
                                        d.Flower_gift_YN
                                    }).ToListAsync();

                var distinctUsers = musers.Where(x => !string.IsNullOrWhiteSpace(x.Email)).Select(m => m.Email).Distinct();

                //모초 중 종이청첩장 제작
                var existsUserQ = from m in _barshopDb.custom_orders
                                  where distinctUsers.Contains(m.order_email) && m.src_packing_date != null
                                  select new { m.order_seq, m.member_id, m.order_email, m.up_order_seq, m.status_seq, m.src_packing_date, m.order_type};
                var existsUser = await existsUserQ.ToListAsync();

                var filterUser = existsUser
                    .Where(m => m.up_order_seq == null && m.status_seq == 15 && ordertype.Contains(m.order_type))
                    .Select(m => m);

                model.MCardWithPaperCount = (from a in musers
                                             join b in filterUser on new { id = a.User_ID, mail = a.Email } equals new { id = b.member_id, mail = b.order_email }
                                             select a).Count();
                model.MCardWithoutPaperCount = musers.Count() - model.MCardWithPaperCount;

                if (workMonth.Key >= new DateTime(2022, 4, 1))
                {
                    //카카오페이 설정: Conf_KaKaoPay_YN = 'Y' && MoneyGift_Remit_Use_YN = 'Y'
                    model.KaKaoPayConfCount = musers.Where(x => x.Conf_KaKaoPay_YN == "Y" && x.MoneyGift_Remit_Use_YN == "Y").Count();
                    //카카오페이 취소 설정: Conf_KaKaoPay_YN = 'Y' && MoneyGift_Remit_Use_YN = 'N'
                    model.KaKaoPayCancelCount = musers.Where(x => x.Conf_KaKaoPay_YN == "Y" && x.MoneyGift_Remit_Use_YN == "N").Count();
                    //일반송금 사용설정 수: Conf_Remit_YN = 'Y' AND  (MoneyAccount_Div_Use_YN = 'Y' OR MoneyAccount_Remit_Use_YN = 'Y')
                    model.RemitConfCount = musers.Where(x => x.Conf_Remit_YN == "Y" && (x.MoneyAccount_Div_Use_YN == "Y" || x.MoneyAccount_Remit_Use_YN == "Y")).Count();
                    //일반송금 일반송금 사용 설정 후 취소한 수: Conf_Remit_YN = 'Y' AND  (C.MoneyAccount_Div_Use_YN = 'N' AND C.MoneyAccount_Remit_Use_YN = 'N')
                    model.RemitCancelCount = musers.Where(x => x.Conf_Remit_YN == "Y" && (x.MoneyAccount_Div_Use_YN == "N" && x.MoneyAccount_Remit_Use_YN == "N")).Count();


                    //화환선물 사용 설정 수 : Flower_gift_YN = 'Y' 
                    model.FlowerConfCount = musers.Where(x => x.Flower_gift_YN == "Y").Count();
                    //화환선물 사용 설정 후 취소한 수
                    model.FlowerCancelCount = musers.Where(x => x.Flower_gift_YN == "C").Count();

                }

                model.AccountTotalAmount = await (from m in _barunsonDb.TB_Remit_Statistics_Monthlies
                                                  where m.Date == workMonth.Key.ToString("yyyMM")
                                                  select m.Remit_Price
                                  ).FirstOrDefaultAsync() ?? 0;

                model.FlowerTotalAmount = await (from m in _barunsonDb.TB_Order_PartnerShip
                                                 where m.P_OrderDate >= workMonth.Key && m.P_OrderDate < workMonth.Value 
                                                 && m.P_Id == "flasystem" && m.Payment_Status_Code == "PSC02"
                                                 select m.Payment_Price).SumAsync();

                results.Add(model);
            }

            return results;
        }
        #endregion


    }
}
