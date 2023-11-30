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
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    /// <summary>
    /// 축의금 관리
    /// </summary>
    [Authorize]
    public class KakaoRemitController : BaseController
    {
        private readonly KakaoBankConfig _kakaoBankConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        #region 생성자
        public KakaoRemitController(ILogger<KakaoRemitController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb, 
            KakaoBankConfig kakaoBankConfig, IHttpClientFactory httpClientFactory)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _httpClientFactory = httpClientFactory;
            _kakaoBankConfig = kakaoBankConfig;
        }
        #endregion

        #region 내부 함수
        private static readonly Dictionary<string, string> KPStatusCode = new Dictionary<string, string>
        {
            {"READY","RC001" },
            {"OPEN_PAYMENT","RC002" },
            {"SELECT_METHOD","RC002" },
            {"AUTH_PASSWORD","RC003" },
            {"PAYMENT_IN_PROGRESS","RC004" },
            {"FAIL_AUTH_PASSWORD","RC102" },
            {"SUCCESS_PAYMENT","RC005" },
            {"FAIL_PAYMENT","RC100" },
            {"QUIT_PAYMENT","RC101" }
        };

        /// <summary>
        /// 축의금 현황 - 일별 통계
        /// </summary>
        /// <param name="yyyymm">년월(202101)</param>
        /// <returns></returns>
        private async Task<List<TB_Remit_Statistics_Daily>> GetTotalStatisticDaysAsync(DateTime date)
        {
            var query = from r in _barunsonDb.TB_Remit_Statistics_Dailies
                        where r.Date.StartsWith(date.ToString("yyyyMM"))
                        orderby r.Date descending
                        select r;

            return await query.ToListAsync();
        }
        /// <summary>
        /// 축의금 현황 - 월별 통계
        /// </summary>
        /// <param name="yyyy">년(2021)</param>
        /// <returns></returns>
        private async Task<List<TB_Remit_Statistics_Monthly>> GetTotalStatisticMonthsAsync(DateTime date)
        {
            var query = from r in _barunsonDb.TB_Remit_Statistics_Monthlies
                        where r.Date.StartsWith(date.ToString("yyyy"))
                        orderby r.Date descending
                        select r;

            return await query.ToListAsync();
        }

        /// <summary>
        /// 축의금 주문별 검색
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="period"></param>
        /// <param name="calculateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<(int Count, int TotalRemitCount, int TotalPrice, List<KakaoRemitOrderDataModel> Items)> GetOrderListAsync(string keyword, string period, string calculateType, DateTime startDate, DateTime endDate,
            int pageFrom = 0, int? pageSize = null)
        {
            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);
            //총 아이템 수
            int count = 0;
            int totalRemitCount = 0;
            int totalPrice = 0;
            var items = new List<KakaoRemitOrderDataModel>();


            //주문데이터 읽기
            var orderQuery = from o in _barunsonDb.TB_Orders
                             join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                             join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                             join it in _barunsonDb.TB_Invitation_Taxes on i.Invitation_ID equals it.Invitation_ID
                             join t in _barunsonDb.TB_Taxes on it.Tax_ID equals t.Tax_ID
                             where (
                                   (period == "Order" && o.Order_DateTime >= startDate && o.Order_DateTime < endDate) ||
                                   (period == "Wedding" && id.WeddingDate.CompareTo(startDate.ToString("yyyy-MM-dd")) >= 0 && id.WeddingDate.CompareTo(endDate.ToString("yyyy-MM-dd")) < 0)
                                  ) &&
                                  (string.IsNullOrEmpty(keyword) || (o.User_ID.Contains(keyword) || o.Name.Contains(keyword) || o.Email.Contains(keyword) || o.Order_Code.Contains(keyword)))
                             select new
                             {
                                 i.Invitation_ID,
                                 o.User_ID,
                                 o.Name,
                                 o.Order_ID,
                                 o.Order_Code,
                                 o.Order_DateTime,
                                 id.WeddingDate,
                                 id.WeddingHour,
                                 id.WeddingMin,
                                 id.Time_Type_Code,
                                 id.Invitation_URL,
                                 t.Tax,
                             };

            var goupQuery = from r in _barunsonDb.TB_Remits
                            from o in orderQuery.Where(m => m.Invitation_ID == r.Invitation_ID)
                            from c in _barunsonDb.TB_Calculates.Where(m => m.Remit_ID == r.Remit_ID && m.Calculate_Type_Code == "CTC02" && m.Status_Code == "200").DefaultIfEmpty()
                            from a in _barunsonDb.TB_Accounts.Where(m => m.Invitation_ID == r.Invitation_ID)
                                    .GroupBy(g => g.Invitation_ID)
                                    .Select(m => new { Count = m.Count(), AccountDate = m.Min(x => x.Regist_DateTime) })
                            where r.Result_Code == "RC005" &&
                                (
                                    string.IsNullOrEmpty(calculateType) ||
                                    (calculateType == "remit" && c != null) ||
                                    (calculateType == "remain" && c == null) 
                                ) 
                            group new { r, c, a } by new
                            {
                                 o.Invitation_ID,
                                 o.User_ID,
                                 o.Name,
                                 o.Order_ID,
                                 o.Order_Code,
                                 o.Order_DateTime,
                                 o.WeddingDate,
                                 o.WeddingHour,
                                 o.WeddingMin,
                                 o.Time_Type_Code,
                                 o.Invitation_URL,
                                 o.Tax
                             } into g
                             select new
                             {
                                 InvitationID = g.Key.Invitation_ID,
                                 UserId = g.Key.User_ID,
                                 UserName = g.Key.Name, 
                                 OrderID = g.Key.Order_ID,
                                 OrderCode = g.Key.Order_Code,
                                 OrderDate = g.Key.Order_DateTime,
                                 WeddingDate = g.Key.WeddingDate,
                                 WeddingHour = g.Key.WeddingHour,
                                 WeddingMin = g.Key.WeddingMin,
                                 Time_Type_Code = g.Key.Time_Type_Code,
                                 Invitation_URL = g.Key.Invitation_URL,
                                 Tax = g.Key.Tax * g.Count(),
                                 AccountCount = g.Max(m => m.a.Count),
                                 AccountDate = g.Max(m => m.a.AccountDate),
                                 RemitCount = g.Count(),
                                 TotalPrice = g.Sum(x => x.r.Total_Price) ?? 0,
                                 RemitPrice = g.Sum(x => x.c.Remit_Price) ?? 0
                             };

            if (period == "Order")
                goupQuery = goupQuery.OrderByDescending(m => m.OrderDate);
            else if (period == "Wedding")
                goupQuery = goupQuery.OrderByDescending(m => m.WeddingDate).ThenByDescending(m => m.WeddingHour);

            var queryItems = await goupQuery.ToListAsync();

            foreach (var item in queryItems)
            {
                var data = new KakaoRemitOrderDataModel
                {
                    OrderID = item.OrderID,
                    OrderCode = item.OrderCode,
                    InvitationID = item.InvitationID,
                    UserId = item.UserId,
                    UserName = item.UserName,
                    AccountCount = item.AccountCount,
                    RemitCount = item.RemitCount,
                    TotalPrice = item.TotalPrice,
                    RemitTax = item.Tax,
                    RemitPrice = item.RemitPrice,
                    RemainPrice = item.TotalPrice - item.Tax - item.RemitPrice,
                    RegistDate = item.OrderDate,
                    WeddingDate = GetWeddingDate(item.WeddingDate, item.WeddingHour, item.WeddingMin, item.Time_Type_Code),
                    AccountDate = item.AccountDate,
                    Invitation_Url = item.Invitation_URL
                };
                items.Add(data);
            }
                        
            count = items.Count;
            totalRemitCount = items.Sum(m => m.RemitCount);
            totalPrice = items.Sum(m => m.TotalPrice);

            if (pageSize.HasValue)
            {
                items = items.Skip(pageFrom).Take(pageSize.Value).ToList();
            }
            return (count, totalRemitCount, totalPrice, items);
        }

        /// <summary>
        /// 건별
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="period"></param>
        /// <param name="calculateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<(int Count, int TotalRemitCount, int TotalPrice, List<KakaoRemitListDataModel> Items)> GetRemitListAsync(string keyword, string period, string calculateType, DateTime startDate, DateTime endDate,
           int pageFrom = 0, int? pageSize = null)
        {
            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);
            //총 아이템 수
            int count = 0;
            int totalRemitCount = 0;
            int totalPrice = 0;
            var items = new List<KakaoRemitListDataModel>();

            var atcList = await GetCommonCodeAsync("Account_Type_Code");
            var bankList = await GetCommonCodeAsync("Bank_Code");
            //주문데이터 읽기
            var orderQuery = from o in _barunsonDb.TB_Orders
                             join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                             join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                             join it in _barunsonDb.TB_Invitation_Taxes on i.Invitation_ID equals it.Invitation_ID
                             join t in _barunsonDb.TB_Taxes on it.Tax_ID equals t.Tax_ID
                             join r in _barunsonDb.TB_Remits on i.Invitation_ID equals r.Invitation_ID
                             join a in _barunsonDb.TB_Accounts on r.Account_ID equals a.Account_ID
                             from c in _barunsonDb.TB_Calculates.Where(m => m.Remit_ID == r.Remit_ID && m.Calculate_Type_Code == "CTC02" && m.Status_Code == "200").DefaultIfEmpty()
                             where r.Result_Code == "RC005" && 
                                  (
                                   (period == "Order" && o.Order_DateTime >= startDate && o.Order_DateTime < endDate) ||
                                   (period == "Remit" && r.Complete_DateTime >= startDate && r.Complete_DateTime < endDate) ||
                                   (period == "Wedding" && id.WeddingDate.CompareTo(startDate.ToString("yyyy-MM-dd")) >= 0 && id.WeddingDate.CompareTo(endDate.ToString("yyyy-MM-dd")) < 0)
                                  ) &&
                                  (string.IsNullOrEmpty(keyword) || (o.User_ID.Contains(keyword) || o.Name.Contains(keyword) || o.Email.Contains(keyword) || o.Order_Code.Contains(keyword))) &&
                                  (
                                    string.IsNullOrEmpty(calculateType) ||
                                    (calculateType == "remit" && c != null) ||
                                    (calculateType == "remain" && c == null)
                                  )
                             orderby r.Complete_DateTime descending
                             select new
                             {
                                 i.Invitation_ID,
                                 o.User_ID,
                                 o.Name,
                                 o.Order_ID,
                                 o.Order_Code,
                                 o.Order_DateTime,
                                 id.WeddingDate,
                                 id.WeddingHour,
                                 id.WeddingMin,
                                 id.Time_Type_Code,
                                 id.Invitation_URL,
                                 t.Tax,
                                 a.Account_Type_Code,
                                 a.Depositor_Name,
                                 a.Bank_Code,
                                 a.Account_Number,
                                 r.Remit_ID,
                                 r.Remitter_Name,
                                 r.Total_Price,
                                 Remit_Price = c.Remit_Price ?? 0,
                                 AccountDate = a.Regist_DateTime,
                                 Complete_Date = r.Complete_DateTime,
                                 Calculate_Date = c.Calculate_DateTime
                             };

            var queryItems = await orderQuery.ToListAsync();

            foreach (var item in queryItems)
            {
                var data = new KakaoRemitListDataModel
                {
                    OrderID = item.Order_ID,
                    OrderCode = item.Order_Code,
                    InvitationID = item.Invitation_ID,
                    RemitID = item.Remit_ID,
                    UserId = item.User_ID,
                    UserName = item.Name,
                    AccountTypeName = atcList.FirstOrDefault(m => m.Code == item.Account_Type_Code)?.Code_Name,
                    BankName = bankList.FirstOrDefault(m => m.Code == item.Bank_Code)?.Code_Name,
                    DepositorName = item.Depositor_Name,
                    AccountNumber = AccountMask(item.Account_Number),
                    RemitterName = item.Remitter_Name,
                    TotalPrice = item.Total_Price ?? 0,
                    RemitTax = item.Tax,
                    RemitPrice = item.Remit_Price,
                    WeddingDate = GetWeddingDate(item.WeddingDate, item.WeddingHour, item.WeddingMin, item.Time_Type_Code),
                    CompleteDateTime = item.Complete_Date,
                    CalculateDateTime = item.Calculate_Date,
                    AccountDate = item.AccountDate,
                    Invitation_Url = item.Invitation_URL
                };
                items.Add(data);
            }

            count = items.Count;
            totalRemitCount = items.Count;
            totalPrice = items.Sum(m => m.TotalPrice);

            if (pageSize.HasValue)
            {
                items = items.Skip(pageFrom).Take(pageSize.Value).ToList();
            }
            return (count, totalRemitCount, totalPrice, items);
        }

        private async Task<SelectList> GetAccountTypeList(string prodCateCode, string allText = "")
        {
            var atcList = await GetCommonCodeAsync("Account_Type_Code");
            var acts = new List<SelectListItem>();
            if (prodCateCode == "PCC01")
            {
                acts = atcList.Where(x => x.Extra_Code == "1" || x.Extra_Code == "2")
                    .Select(m => new SelectListItem { Text = m.Code_Name, Value = m.Code }).ToList();
            }
            else if (prodCateCode == "PCC03")
            {
                acts = atcList.Where(x => x.Extra_Code == "3")
                    .Select(m => new SelectListItem { Text = m.Code_Name, Value = m.Code }).ToList();
            }
            if (!string.IsNullOrEmpty(allText))
            {
                acts.Insert(0, new SelectListItem { Text = allText, Value = "" });
            }
            
            return new SelectList(acts, "Value", "Text");
        }

        /// <summary>
        /// 계좌번호 마스킹 처리
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string AccountMask(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= 5)
                return value;
            else
                return value.Substring(0, 3) +
                       new string('*', value.Length - 4) +
                       value.Substring(value.Length - 1);

        }
        #endregion

        #region KakaoApi Call
        /// <summary>
        /// 송금 관련 API 호출
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="apiUri"></param>
        /// <param name="postBody"></param>
        /// <returns></returns>
        private async Task<(HttpStatusCode StatusCode, R Data)> PostKakaoApiAsync<T,R>(Uri apiUri, T postBody)
        {
            var result = default(R);
            var statusCode = HttpStatusCode.InternalServerError;
            var httpClient = _httpClientFactory.CreateClient();
            var bodystr = JsonSerializer.Serialize<T>(postBody);
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = apiUri;
                request.Content = new StringContent(bodystr, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                statusCode = response.StatusCode;

                var responsestr = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<R>(responsestr);
            }
            return (statusCode, result);
        }
        #endregion

        #region 매출현황
        /// <summary>
        ///  매출현황 - 일별 통계
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TotalDailyAsync(int? year, int? month)
        {
            var targetDate = GetTargetDate(year, month);

            var itmes = await GetTotalStatisticDaysAsync(targetDate);
            var model = new StatisticsViewModel<TB_Remit_Statistics_Daily>
            {
                SelectedYear = targetDate.Year,
                SelectedMonth = targetDate.Month,
                Items = itmes.Where(m => !m.Date.EndsWith("00")).OrderByDescending(o => o.Date).ToList(),
                Total = itmes.Where(m => m.Date.EndsWith("00")).SingleOrDefault()
            };

            return View(model);
        }
        /// <summary>
        ///  매출현황 - 일별 통계 엑셀
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

            workSheet.Cells[rowIndex, colIndex++].Value = "일자";
            workSheet.Cells[rowIndex, colIndex++].Value = "거래액";
            workSheet.Cells[rowIndex, colIndex++].Value = "매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "송금 수수료";
            workSheet.Cells[rowIndex, colIndex++].Value = "정산 수수료";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌 조회 수수료";
            workSheet.Cells[rowIndex, colIndex++].Value = "순매출";
            workSheet.Cells[rowIndex, colIndex++].Value = "이용자수";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌수";
            workSheet.Cells[rowIndex, colIndex++].Value = "송금건수";


            foreach (var item in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = item.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Remit_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Remit_Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Calculate_Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Hits_Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Tax - item.Remit_Tax - item.Calculate_Tax - item.Hits_Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.User_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Account_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Remit_Count;
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Daily_" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }

        /// <summary>
        /// 매출현황 - 월별 통계
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TotalMonthlyAsync(int? year)
        {
            var targetDate = GetTargetDate(year);
            var itmes = await GetTotalStatisticMonthsAsync(targetDate);
            var model = new StatisticsViewModel<TB_Remit_Statistics_Monthly>
            {
                SelectedYear = targetDate.Year,
                Items = itmes.Where(m => !m.Date.EndsWith("00")).OrderByDescending(o => o.Date).ToList(),
                Total = itmes.Where(m => m.Date.EndsWith("00")).SingleOrDefault()
            };
            return View(model);
        }
        /// <summary>
        /// 매출현황 - 월별 통계 엑셀
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

            workSheet.Cells[rowIndex, colIndex++].Value = "일자";
            workSheet.Cells[rowIndex, colIndex++].Value = "거래액";
            workSheet.Cells[rowIndex, colIndex++].Value = "매출액";
            workSheet.Cells[rowIndex, colIndex++].Value = "송금 수수료";
            workSheet.Cells[rowIndex, colIndex++].Value = "정산 수수료";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌 조회 수수료";
            workSheet.Cells[rowIndex, colIndex++].Value = "순매출";
            workSheet.Cells[rowIndex, colIndex++].Value = "이용자수";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌수";
            workSheet.Cells[rowIndex, colIndex++].Value = "송금건수";


            foreach (var item in items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = item.Date;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Remit_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Remit_Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Calculate_Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Hits_Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Tax - item.Remit_Tax - item.Calculate_Tax - item.Hits_Tax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.User_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Account_Count;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Remit_Count;
            }
            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Monthly_" + DateTime.Now.ToShortTimeString() + ".xlsx");

        }
        #endregion

        #region 주문관리
        /// <summary>
        /// 주문별
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> OrderList(KakaoRemitOrderSearchViewModel<KakaoRemitOrderDataModel> model)
        {
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");

            var result = await GetOrderListAsync(
               model.Keyword,
               model.Period,
               model.CalculateType,
               model.StartDate,
               model.EndDate,
               model.PageFrom,
               model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.RemitCount = result.TotalRemitCount;
            model.TotalPrice = result.TotalPrice;
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "OrderList";
            model.RouteController = "KakaoRemit";

            return View(model);
        }
        public async Task<IActionResult> OrderListExcel(KakaoRemitOrderSearchViewModel<KakaoRemitOrderDataModel> model)
        {
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");

            var result = await GetOrderListAsync(
               model.Keyword,
               model.Period,
               model.CalculateType,
               model.StartDate,
               model.EndDate);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.RemitCount = result.TotalRemitCount;
            model.TotalPrice = result.TotalPrice;

            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "이름";
            workSheet.Cells[rowIndex, colIndex++].Value = "사용자ID";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌 수";
            workSheet.Cells[rowIndex, colIndex++].Value = "송금건수";
            workSheet.Cells[rowIndex, colIndex++].Value = "송금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "수수료";
            workSheet.Cells[rowIndex, colIndex++].Value = "정산완료";
            workSheet.Cells[rowIndex, colIndex++].Value = "미정산";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문일";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌설정일";
            workSheet.Cells[rowIndex, colIndex++].Value = "예식일";

            foreach (var item in result.Items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderCode;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserId;
                workSheet.Cells[rowIndex, colIndex++].Value = item.AccountCount;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RemitCount;
                workSheet.Cells[rowIndex, colIndex++].Value = item.TotalPrice;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RemitTax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RemitPrice;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RemainPrice;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RegistDate?.ToString("yyyy-MM-dd HH:mm:ss");
                workSheet.Cells[rowIndex, colIndex++].Value = item.AccountDate?.ToString("yyyy-MM-dd HH:mm:ss"); 
                workSheet.Cells[rowIndex, colIndex++].Value = item.WeddingDate?.ToString("yyyy-MM-dd HH:mm:ss"); 
            }
            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "User_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }
        #endregion

        #region AccountList
        /// <summary>
        /// 계좌 정보
        /// </summary>
        /// <param name="id">초대장 InvitationID</param>
        /// <returns></returns>
        public async Task<IActionResult> AccountListPartial(int id)
        {
            var model = new KakaoRemitViewModel<KakaoAccountDataModel>
            {
                InvitationID = id,
                UserName = "",
                DataModel = new List<KakaoAccountDataModel>()
            };
            //상품정보 읽기
            var orderQ = from i in _barunsonDb.TB_Invitations
                           join o in _barunsonDb.TB_Orders on i.Order_ID equals o.Order_ID
                           join op in _barunsonDb.TB_Order_Products on o.Order_ID equals op.Order_ID
                           join p in _barunsonDb.TB_Products on op.Product_ID equals p.Product_ID
                           where i.Invitation_ID == id
                           select new { p.Product_Category_Code, o.Name };
            var order = await orderQ.FirstAsync();
            model.UserName = order.Name;
            model.AccountTypeList = await GetAccountTypeList(order.Product_Category_Code, null);

            model.BankList = new SelectList(await GetSelectListsCommonCodesAsync("Bank_Code"), "Value", "Text");
            
            var accountQ = from a in _barunsonDb.TB_Accounts
                           where a.Invitation_ID == id
                           orderby a.Sort
                           select new KakaoAccountDataModel
                           {
                               AccountId = a.Account_ID,
                               AccountNumber = a.Account_Number,
                               AccountTypeCode = a.Account_Type_Code,
                               BankCode = a.Bank_Code,
                               DepositorName = a.Depositor_Name,
                               Sort = a.Sort ?? 0
                           };
            model.DataModel = await accountQ.ToListAsync();

            return View(model);
        }

        /// <summary>
        /// 계좌 수정 저장
        /// </summary>
        /// <param name="id">초대장 InvitationID</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> AccountModify(int id, KakaoAccountDataModel model)
        {
            var result = new JsonReusltStatusModel { status = false, message = "계좌 저장에 실패 하였습니다." };
            try
            {
                if (string.IsNullOrEmpty(model.AccountTypeCode) || string.IsNullOrEmpty(model.BankCode) || 
                    string.IsNullOrEmpty(model.DepositorName) || string.IsNullOrEmpty(model.AccountNumber))
                {
                    result.message = "입력된 정보를 다시 확인 해주세요. 모든 항목을 입력하셔야 합니다.";
                    return Json(result);
                }

                var accoutQ = from a in _barunsonDb.TB_Accounts
                              where a.Invitation_ID == id && a.Account_ID == model.AccountId
                              select a;
                var account = await accoutQ.FirstOrDefaultAsync();
                if (account != null)
                {
                    var Now = DateTime.Now;

                    #region 계좌 유효성 검사

                    //최종 API 성공 여부
                    var isSuccess = false;

                    //고유 번호 생성
                    var uniqueNum = await GetTelegramNoAsync(Now.ToString("yyyyMMdd"));

                    #region 계좌 조회 기록 데이터 생성

                    var InquireHit = new TB_Depositor_Hit
                    {
                        User_ID = CurrentUserId(),  //관리자 ID
                        Request_DateTime = Now,
                        Request_Date = Now.ToString("yyyyMMdd"),
                        Bank_Code = model.BankCode,
                        Account_Number = model.AccountNumber,
                        Unique_Number = uniqueNum
                    };
                    _barunsonDb.TB_Depositor_Hits.Add(InquireHit);
                    await _barunsonDb.SaveChangesAsync();

                    #endregion

                    #region 계좌번호 확인 API

                    #endregion
                    var InquireDepositor = new KP_FirmInquireDepositor
                    {
                        BankCode = model.BankCode,
                        Account = model.AccountNumber,
                        ApiKey = _kakaoBankConfig.BankingApiKey,
                        OrgCode = _kakaoBankConfig.OrgCode,
                        TelegramNo = uniqueNum
                    };
                    var apiUri = new Uri(_kakaoBankConfig.BankingHost, _kakaoBankConfig.InquireDepositorUri);
                    var response = await PostKakaoApiAsync<KP_FirmInquireDepositor, KP_FirmInquireDepositorResult>(apiUri, InquireDepositor);
                    var responseData = response.Data;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        InquireHit.Status_Code = responseData.Status.ToString();
                        InquireHit.Trading_Number = responseData.NatvTrNo;
                        InquireHit.Depositor = responseData.Depositor;
                        InquireHit.Hits_Depositor = model.DepositorName;
                        InquireHit.Request_Result_DateTime = responseData.RequestAt;

                        if (model.DepositorName.StartsWith(responseData.Depositor, StringComparison.InvariantCultureIgnoreCase))
                            isSuccess = true;
                        else
                            result.message = "계좌정보가 맞지 않습니다.(예금주)";
                    }
                    else
                    {
                        InquireHit.Error_Code = responseData.ErrorCode;
                        InquireHit.Error_Message = responseData.ErrorMessage;
                        result.message = $"계좌정보 확인중 오류가 발생하였습니다.{(responseData.ErrorMessage)}";
                    }
                    await _barunsonDb.SaveChangesAsync();
                    #endregion

                    if (isSuccess)
                    {
                        account.Account_Type_Code = model.AccountTypeCode;
                        account.Bank_Code = model.BankCode;
                        account.Depositor_Name = model.DepositorName;
                        account.Account_Number = model.AccountNumber;

                        await _barunsonDb.SaveChangesAsync();
                        result.status = true;
                        result.message = "정상적으로 저장 하였습니다.";
                        result.url = Url.Action("AccountListPartial", "KakaoRemit", new { id = id });
                    }

                }
                else
                {
                    result.message = "수정할 계좌 정보를 읽을 수 었습니다.";
                }
            }
            catch(Exception ex)
            {
                result.message = ex.Message;
            }
            return Json(result);
        }
        public async Task<IActionResult> AccountChangeSort(int id, [FromBody]List<int> accountId)
        {
            var result = new JsonReusltStatusModel { status = false, message = "계좌 저장에 실패 하였습니다." };
            try
            {
                var accoutQ = from a in _barunsonDb.TB_Accounts
                              where a.Invitation_ID == id
                              select a;
                var accounts = await accoutQ.ToListAsync();
                if (accounts != null && accounts.Count > 0)
                {
                    for(int i = 0; i < accountId.Count; i++)
                    {
                        var account = accounts.FirstOrDefault(m => m.Account_ID == accountId[i]);
                        if (account != null)
                            account.Sort = i;
                    }
                    
                    await _barunsonDb.SaveChangesAsync();
                    result.status = true;
                    result.message = "정상적으로 저장 하였습니다.";
                    result.url = Url.Action("AccountListPartial", "KakaoRemit", new { id = id });
                }
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return Json(result);
        }
        
        #endregion

        #region 송금 내역
        /// <summary>
        /// 송금 내역 
        /// </summary>
        /// <param name="id">초대장 InvitationID </param>
        /// <returns></returns>
        public async Task<IActionResult> RemitDtailPartial(int id)
        {
            var model = new KakaoRemitViewModel<KakaoRemitDataModel>
            {
                InvitationID = id,
                UserName = "",
                DataModel = new List<KakaoRemitDataModel>()
            };

            var query = from r in _barunsonDb.TB_Remits
                        join a in _barunsonDb.TB_Accounts on r.Account_ID equals a.Account_ID
                        join i in _barunsonDb.TB_Invitations on r.Invitation_ID equals i.Order_ID
                        join it in _barunsonDb.TB_Invitation_Taxes on i.Invitation_ID equals it.Invitation_ID
                        join t in _barunsonDb.TB_Taxes on it.Tax_ID equals t.Tax_ID
                        from AccountTypeCode in _barunsonDb.TB_Common_Codes.Where(c => a.Account_Type_Code == c.Code && c.Code_Group == "Account_Type_Code").DefaultIfEmpty()
                        from BankCode in _barunsonDb.TB_Common_Codes.Where(c => a.Bank_Code == c.Code && c.Code_Group == "Bank_Code").DefaultIfEmpty()
                        from c in _barunsonDb.TB_Calculates.Where(m => m.Remit_ID == r.Remit_ID && m.Calculate_Type_Code == "CTC02" && m.Status_Code == "200").DefaultIfEmpty()
                        where r.Invitation_ID == id && r.Result_Code == "RC005"
                        orderby r.Complete_DateTime descending
                        select new
                        {
                            r.Remit_ID,
                            AccountTypeName = AccountTypeCode.Code_Name,
                            a.Depositor_Name,
                            BankName = BankCode.Code_Name,
                            a.Account_Number,
                            r.Remitter_Name,
                            r.Total_Price,
                            t.Tax,
                            c.Remit_Price,
                            r.Complete_DateTime,
                            c.Calculate_DateTime,
                            i.User_ID
                        };
            var items = await query.ToListAsync();
            if (items!= null && items.Count > 0)
            {
                model.UserName = await _barunsonDb.VW_Users.Where(m => m.USER_ID == items.First().User_ID).Select(m => m.NAME).FirstOrDefaultAsync();

                foreach (var item in items)
                {
                    model.DataModel.Add(new KakaoRemitDataModel
                    {
                        RemitID = item.Remit_ID,
                        AccountTypeName = item.AccountTypeName,
                        DepositorName = item.Depositor_Name,
                        BankName = item.BankName,
                        AccountNumber = AccountMask(item.Account_Number),
                        RemitterName = item.Remitter_Name,
                        TotalPrice = item.Total_Price ?? 0,
                        RemitTax = item.Tax,
                        RemitPrice = item.Remit_Price ?? 0,
                        CompleteDateTime = item.Complete_DateTime,
                        CalculateDateTime = item.Calculate_DateTime,

                    });
                }
            }
            return View(model);
        }

        /// <summary>
        /// 송금확인
        /// </summary>
        /// <param name="id">RemitID</param>
        /// <returns></returns>
        public async Task<IActionResult> CheckComplete(int id)
        {
            var reuslt = new JsonReusltStatusModel { status = false, message = "송금 정보 확인중 오류가 발생하였습니다. (코드 10)" };
            try
            {
                var remit = await (from a in _barunsonDb.TB_Remits where a.Remit_ID == id select a).FirstOrDefaultAsync();
                if (remit != null)
                {
                    var Now = DateTime.Now;
                    var Obj = new KP_TransferStatus
                    {
                        ApiKey = _kakaoBankConfig.MainApiKey,
                        Cid = _kakaoBankConfig.MainCid,
                        Tid = remit.Transaction_ID
                    };

                    var apiUri = new Uri(_kakaoBankConfig.MainHost, _kakaoBankConfig.StatusUri);
                    var response = await PostKakaoApiAsync<KP_TransferStatus, KP_StatusResult>(apiUri, Obj);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        DateTime dt = DateTime.ParseExact(response.Data.approved_at, "yyyyMMddHHmmss", null);
                        remit.Send_Status = response.Data.send_status;
                        if (KPStatusCode.ContainsKey(response.Data.send_status))
                        {
                            remit.Complete_DateTime = dt;
                            remit.Complete_Date = response.Data.approved_at.Substring(0, 8);
                            remit.Result_Code = KPStatusCode[response.Data.send_status];

                            reuslt.status = true;
                            reuslt.message = "송금이 확인 되었습니다.";
                        }
                        else
                        {
                            remit.Result_Code = "RC100";
                        }
                        await _barunsonDb.SaveChangesAsync();
                        
                    }
                    else
                    {
                        reuslt.message = $"송금 정보 확인중 오류가 발생하였습니다.({response.StatusCode})";
                    }
                }
                else
                {
                    reuslt.message = "송금 정보가 없습니다.";
                }
            }
            catch { }

            return Json(reuslt);
        }
        /// <summary>
        /// 정산
        /// </summary>
        /// <param name="id">RemitID</param>
        /// <returns></returns>
        public async Task<IActionResult> CheckCalculate(int id)
        {
            var reuslt = new JsonReusltStatusModel { status = false, message = "정산 중 오류가 발생하였습니다. (코드 10)" };
            try
            {
                var calcitems = await (from a in _barunsonDb.TB_Calculates
                                       where a.Remit_ID == id
                                       orderby a.Calculate_ID descending
                                       select a).ToListAsync();
                if (calcitems != null && !calcitems.Any(m => m.Status_Code == "200"))
                {
                    var Now = DateTime.Now;

                    var calcitem = calcitems.FirstOrDefault();
                  
                    KP_FirmTransferResult FirmTransferResult = null;
                    TB_Calculate calculate = null;
                    // 이체처리 결과 조회에서 이체가 안된경우 재정산
                    if (calcitem != null && (calcitem.Error_Code == "VTIM" || calcitem.Error_Code == "0011"))
                    {
                        calculate = calcitem;
                        var check = new KP_Check
                        {
                            ApiKey = _kakaoBankConfig.BankingApiKey,
                            OrgCode = _kakaoBankConfig.OrgCode,
                            OrgTelegramNo = calcitem.Unique_Number.Value.ToString(),
                            TrDt = calcitem.Request_Date
                        };

                        var apiUri = new Uri(_kakaoBankConfig.BankingHost, _kakaoBankConfig.TransferCheckUri);
                        var response = await PostKakaoApiAsync<KP_Check, KP_FirmTransferResult>(apiUri, check);
                        FirmTransferResult = response.Data;
                    }
                    else
                    {
                        //고유 번호 생성
                        var uniqueNum = await GetTelegramNoAsync(Now.ToString("yyyyMMdd"));
                        //수신 공동 입금 정보(더즌과 계약된 전용 계좌)
                        var accountSetting = await (from a in _barunsonDb.TB_Account_Settings orderby a.Account_Setting_ID descending select a).FirstAsync();
                        var remit = await (from a in _barunsonDb.TB_Remits where a.Remit_ID == calcitem.Remit_ID select a).FirstAsync();
                        var tax = await (from a in _barunsonDb.TB_Taxes
                                         join b in _barunsonDb.TB_Invitation_Taxes on a.Tax_ID equals b.Tax_ID
                                         where b.Invitation_ID == remit.Invitation_ID 
                                         select a.Tax).FirstAsync();
                        var account = await (from a in _barunsonDb.TB_Accounts where a.Account_ID == remit.Account_ID select a).FirstAsync();

                        var FirmTransfer = new KP_FirmTransfer
                        {
                            ApiKey = _kakaoBankConfig.BankingApiKey,
                            OrgCode = _kakaoBankConfig.OrgCode,
                            TelegramNo = uniqueNum,
                            RvAccountCntn = remit.Remitter_Name + "입금",
                            Amount = (int)(remit.Total_Price - tax), // 수수료 빼고 입금
                            RvBankCode = account.Bank_Code,
                            RvAccount = account.Account_Number
                        };
                        calculate = new TB_Calculate
                        {
                            Unique_Number = uniqueNum,
                            Calculate_Type_Code = "CTC02",
                            Request_Date = Now.ToString("yyyyMMdd"),
                            Calculate_DateTime = Now,
                            Status_Code = "100",
                            Remit_ID = remit.Remit_ID,
                            Remit_Price = FirmTransfer.Amount,
                            Remit_Content = FirmTransfer.RvAccountCntn,
                            Remit_Bank_Code = FirmTransfer.RvBankCode,
                            Remit_Account_Number = FirmTransfer.RvAccount
                        };
                        _barunsonDb.TB_Calculates.Add(calculate);

                        var apiUri = new Uri(_kakaoBankConfig.BankingHost, _kakaoBankConfig.TransferUri);
                        var response = await PostKakaoApiAsync<KP_FirmTransfer, KP_FirmTransferResult>(apiUri, FirmTransfer);
                        FirmTransferResult = response.Data;
                    }

                    if (FirmTransferResult.Status == 200)
                    {
                        // 정상 처리 저장
                        calculate.Status_Code = FirmTransferResult.Status.ToString();
                        calculate.Trading_Number = FirmTransferResult.NatvTrNo;
                        calculate.Request_DateTime = FirmTransferResult.RequestAt;
                        calculate.Remit_Price = FirmTransferResult.Amount;

                        reuslt.status = true;
                        reuslt.message = "정산 되었습니다.";
                    }
                    else
                    {
                        // 통신 이상 정보 저장
                        calculate.Status_Code = FirmTransferResult.Status.ToString();
                        calculate.Error_Code = FirmTransferResult.ErrorCode;
                        calculate.Error_Message = FirmTransferResult.ErrorMessage;

                        reuslt.message = FirmTransferResult.ErrorMessage;
                    }
                    await _barunsonDb.SaveChangesAsync();
                    
                }
                else
                {
                    reuslt.message = "정산이 완료 됬거나 정산할 정보가 없습니다.";
                }
            }
            catch { }

            return Json(reuslt);
        }
        #endregion

        #region 건별

        /// <summary>
        /// 건별
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemitList(KakaoRemitOrderSearchViewModel<KakaoRemitListDataModel> model)
        {
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");
            model.Periods = new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = "주문일", Value = "Order" },
                        new SelectListItem { Text = "송금일", Value = "Remit" },
                        new SelectListItem { Text = "예식일", Value = "Wedding" }
                    }, "Value", "Text");


            var result = await GetRemitListAsync(
               model.Keyword,
               model.Period,
               model.CalculateType,
               model.StartDate,
               model.EndDate,
               model.PageFrom,
               model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.RemitCount = result.TotalRemitCount;
            model.TotalPrice = result.TotalPrice;
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "RemitList";
            model.RouteController = "KakaoRemit";

            return View(model);
        }

        public async Task<IActionResult> RemitListExcel(KakaoRemitOrderSearchViewModel<KakaoRemitListDataModel> model)
        {
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");
            
            var result = await GetRemitListAsync(
               model.Keyword,
               model.Period,
               model.CalculateType,
               model.StartDate,
               model.EndDate,
               model.PageFrom,
               model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.RemitCount = result.TotalRemitCount;
            model.TotalPrice = result.TotalPrice;


            int rowIndex = 1;
            int colIndex = 1;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "이름";
            workSheet.Cells[rowIndex, colIndex++].Value = "사용자ID";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "구분";
            workSheet.Cells[rowIndex, colIndex++].Value = "예금주";
            workSheet.Cells[rowIndex, colIndex++].Value = "은행";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌";
            workSheet.Cells[rowIndex, colIndex++].Value = "송금자";
            workSheet.Cells[rowIndex, colIndex++].Value = "송금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "수수료";
            workSheet.Cells[rowIndex, colIndex++].Value = "정산완료";
            workSheet.Cells[rowIndex, colIndex++].Value = "미정산";
            workSheet.Cells[rowIndex, colIndex++].Value = "예식일";
            workSheet.Cells[rowIndex, colIndex++].Value = "계좌설정일";
            workSheet.Cells[rowIndex, colIndex++].Value = "하객송금일";
            workSheet.Cells[rowIndex, colIndex++].Value = "정산완료일";

            foreach (var item in result.Items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = item.UserName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.UserId;
                workSheet.Cells[rowIndex, colIndex++].Value = item.OrderCode;
                workSheet.Cells[rowIndex, colIndex++].Value = item.AccountTypeName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.DepositorName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.BankName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.AccountNumber;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RemitterName;
                workSheet.Cells[rowIndex, colIndex++].Value = item.TotalPrice;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RemitTax;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RemitPrice;
                workSheet.Cells[rowIndex, colIndex++].Value = item.RemainPrice;
                workSheet.Cells[rowIndex, colIndex++].Value = item.WeddingDate?.ToString("yyyy-MM-dd HH:mm:ss");
                workSheet.Cells[rowIndex, colIndex++].Value = item.AccountDate?.ToString("yyyy-MM-dd HH:mm:ss");
                workSheet.Cells[rowIndex, colIndex++].Value = item.CompleteDateTime?.ToString("yyyy-MM-dd HH:mm:ss");
                workSheet.Cells[rowIndex, colIndex++].Value = item.CalculateDateTime?.ToString("yyyy-MM-dd HH:mm:ss");


            }
            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Remit_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }

        #endregion

        #region 수수료
        /// <summary>
        /// 수수료 변경 히스토리
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SettingTax()
        {
            var query = from t in _barunsonDb.TB_Taxes
                        join u in _barunsonDb.TB_Invitation_Admins on t.Regist_User_ID equals u.admin_id into d
                        from u in d.DefaultIfEmpty()
                        orderby t.Tax_ID descending
                        select new KakaoRemitTaxDataModel
                        {
                            TaxId = t.Tax_ID,
                            Tax = t.Tax,
                            PreviousTax = t.Previous_Tax,
                            RegistDateTime = t.Regist_DateTime,
                            RegistUserID = t.Regist_User_ID,
                            RegistUserName = u.admin_name ?? t.Regist_User_ID
                        };
            var model = await query.ToListAsync();

            return View(model);

        }
        /// <summary>
        /// 수수료 저장
        /// </summary>
        /// <param name="Tax"></param>
        /// <returns></returns>
        public async Task<IActionResult> SaveTax(int Tax)
        {
            var prvTax = await _barunsonDb.TB_Taxes.OrderByDescending(t => t.Tax_ID).FirstOrDefaultAsync();
            int prvTaxVal = 0;
            if (prvTax != null)
            {
                prvTaxVal = prvTax.Tax;
            }
            TB_Tax tB_Tax = new()
            {
                Tax = Tax,
                Previous_Tax = prvTaxVal,
                Regist_DateTime = DateTime.Now,
                Regist_User_ID = CurrentUserId()
            };
            _barunsonDb.TB_Taxes.Add(tB_Tax);
            await _barunsonDb.SaveChangesAsync();

            return RedirectToAction("SettingTax");
        }
        #endregion


        #region 잔액 확인
        public async Task<IActionResult> CheckBalance()
        {
            var result = new JsonReusltStatusModel { status = false, message = "잔액 확인 중 오류가 발생하였습니다. (코드 10)" };
            try
            {
                var Now = DateTime.Now;
                //고유 번호 생성
                var uniqueNum = await GetTelegramNoAsync(Now.ToString("yyyyMMdd"));
                //수신 공동 입금 정보(더즌과 계약된 전용 계좌)
                var accountSetting = await (from a in _barunsonDb.TB_Account_Settings orderby a.Account_Setting_ID descending select a).FirstAsync();

                KP_BalanceCheck balanceCheck = new()
                {
                    ApiKey = _kakaoBankConfig.BankingApiKey,
                    OrgCode = _kakaoBankConfig.OrgCode,
                    TelegramNo = uniqueNum,
                    DrwAccount = accountSetting.Kakao_Account_Number,
                    DrwBankCode = accountSetting.Kakao_Bank_Code
                };
                
                var apiUri = new Uri(_kakaoBankConfig.BankingHost, _kakaoBankConfig.BalanceCheckUri);

                var response = await PostKakaoApiAsync<KP_BalanceCheck, KP_BalanceCheckResult>(apiUri, balanceCheck);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result.status = true;
                    result.message = $"잔액: {response.Data.BalanceAmount.ToString("#,##0")}원";
                }
            }
            catch
            { }
            return Json(result);
        }

        #endregion
    }
}
