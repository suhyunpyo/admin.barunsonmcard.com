using Barunn.MobileInvitation.Common.Models;
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
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        public OrderController(ILogger<OrderController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
        }
        #region 주문목록

        /// <summary>
        /// 주문목록 검색
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="productCategory"></param>
        /// <param name="productBarnd"></param>
        /// <param name="overlapPurchase"></param>
        /// <param name="memberYn"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="pageFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<(int Count, List<OrderSearchDataModel> Items)> GetOrderListAsync(string keyword, string period, DateTime startDate, DateTime endDate,
            List<string> productCategory, List<string> productBarnd,
            bool overlapPurchase, string memberYn, List<string> paymentStatus, int pageFrom = 0, int? pageSize = null)
        {
            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);
            //총 아이템 수
            int count = 0;
            List<OrderSearchDataModel> items = null;
            var orderQuery = from o in _barunsonDb.TB_Orders
                             join op in _barunsonDb.TB_Order_Products on o.Order_ID equals op.Order_ID
                             join p in _barunsonDb.TB_Products on op.Product_ID equals p.Product_ID
                             join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                             join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                             join d in _barunsonDb.TB_Common_Codes on new { Code = o.Payment_Method_Code, Code_Group = "PAYMENT_METHOD_CODE" } equals new { d.Code, d.Code_Group } into gd
                             from pm in gd.DefaultIfEmpty()
                             join e in _barunsonDb.TB_Common_Codes on new { Code = o.Payment_Status_Code, Code_Group = "PAYMENT_STATUS_CODE" } equals new { e.Code, e.Code_Group } into ge
                             from ps in ge.DefaultIfEmpty()
                             join f in _barunsonDb.TB_Common_Codes on new { Code = p.Product_Brand_Code, Code_Group = "Product_Brand_Code" } equals new { f.Code, f.Code_Group } into gf
                             from pb in gf.DefaultIfEmpty()
                             where ((period == "Order" && o.Regist_DateTime >= startDate && o.Regist_DateTime < endDate) ||
                                 (period == "Pay" && o.Payment_DateTime >= startDate && o.Payment_DateTime < endDate)) &&
                                 o.Payment_Method_Code != null &&
                                 productCategory.Contains(p.Product_Category_Code) &&
                                 productBarnd.Contains(p.Product_Brand_Code) &&
                                 (string.IsNullOrEmpty(keyword) || (o.User_ID.Contains(keyword) || o.Name.Contains(keyword) || o.Email.Contains(keyword) || o.Order_Code.Contains(keyword))) &&
                                 (memberYn == "ALL" || ((memberYn == "G" && o.User_ID == "") || (memberYn == "U" && o.User_ID != ""))) &&
                                 (paymentStatus == null || paymentStatus.Contains(o.Payment_Status_Code))
                             select new OrderSearchDataModel
                             {
                                 Product_Brand_Code = p.Product_Brand_Code,
                                 Product_Brand_Name = pb.Code_Name,
                                 User_Id = o.User_ID,
                                 User_Name = o.Name,
                                 User_Email = o.Email,
                                 Member_Type = (o.User_ID == "") ? "G" : "U",
                                 Order_Path = o.Order_Path,
                                 Payment_Path = o.Payment_Path,
                                 Order_ID = o.Order_ID,
                                 Order_Code = o.Order_Code,
                                 Product_ID = p.Product_ID,
                                 Product_Code = p.Product_Code,
                                 Order_Price = o.Order_Price,
                                 Coupon_Price = o.Coupon_Price,
                                 Payment_Price = o.Payment_Price,
                                 Payment_Method_Code = o.Payment_Method_Code,
                                 Payment_Method_Name = pm.Code_Name,
                                 Regist_DateTime = o.Regist_DateTime,
                                 Payment_Status_Code = o.Payment_Status_Code,
                                 Payment_Status_Name = ps.Code_Name,
                                 Payment_DateTime = o.Payment_DateTime,
                                 Wedding_Date = id.WeddingDate,
                                 Invitation_Url = id.Invitation_URL
                             };
            if (period == "Order")
                orderQuery = orderQuery.OrderByDescending(m => m.Regist_DateTime);
            else if (period == "Pay")
                orderQuery = orderQuery.OrderByDescending(m => m.Payment_DateTime);
            else
                orderQuery = orderQuery.OrderByDescending(m => m.Order_ID);

            if (overlapPurchase) //중복 구매자 필터시 페이징은 결과값에서 실행
            {
                var orderItems = await orderQuery.ToListAsync();
                var dupOrderItems = orderItems.GroupBy(x => new { x.User_Id, x.User_Email })
                     .Where(g => g.Count() > 1)
                     .Select(g => new { Items = g.Select(m => m) })
                     .SelectMany(x => x.Items)
                     .OrderBy(x => x.User_Id)
                     .ThenByDescending(x => x.Order_ID);

                count = dupOrderItems.Count();

                if (pageSize.HasValue)
                {
                    //페이지 수 만큼 데이터 읽기
                    items = dupOrderItems.Skip(pageFrom).Take(pageSize.Value).ToList();
                }
                else
                {
                    items = dupOrderItems.ToList();
                }
            }
            else
            {
                if (pageSize.HasValue)
                {
                    //총 아이템 수
                    count = await orderQuery.CountAsync();
                    //페이지 수 만큼 데이터 읽기
                    items = await orderQuery.Skip(pageFrom).Take(pageSize.Value).ToListAsync();
                }
                else
                {
                    items = await orderQuery.ToListAsync();
                    count = items.Count;
                }
            }


            //All User ID
            var allIds = items.Select(m => string.IsNullOrEmpty(m.User_Id) ? m.User_Email : m.User_Id).Distinct().ToList();

            //청첩장 주문
            var customOrderQuery = from o in _barshopDb.custom_orders
                                   where allIds.Contains(o.member_id) &&
                                   o.status_seq >= 9 &&
                                   o.card_seq > 0
                                   group o by o.member_id into g
                                   select new { Id = g.Key, SaleGubun = g.Max(m => m.sales_Gubun) };
            var customOrderItems = await customOrderQuery.ToListAsync();

            //All Order ID
            var allOrderIds = items.Select(m => m.Order_ID).Distinct().ToList();
            //환불 정보
            var refundQuery = from r in _barunsonDb.TB_Refund_Infos
                              where allOrderIds.Contains(r.Order_ID)
                              orderby r.Refund_ID descending
                              select new { r.Refund_ID, r.Order_ID, r.Refund_Type_Code, r.Refund_Status_Code };
            var refundItems = await refundQuery.ToListAsync();

            foreach (var item in items)
            {
                var saleGubun = customOrderItems.FirstOrDefault(m => m.Id.Equals((string.IsNullOrEmpty(item.User_Id) ? item.User_Email : item.User_Id), StringComparison.InvariantCultureIgnoreCase));
                if (saleGubun != null)
                {
                    item.Sales_Gubun_Code = saleGubun.SaleGubun;
                    item.Sales_Gubun_Name = SalesGubunCodeModel.SalesGubunCodes.FirstOrDefault(m => m.Code == saleGubun.SaleGubun)?.Name;
                }
                //환불 정보 채우기
                var tbRefundInfo = refundItems.Where(m => m.Order_ID == item.Order_ID).FirstOrDefault();
                if (tbRefundInfo != null)
                {
                    item.RefundYn = tbRefundInfo.Refund_Type_Code == "RTC03" && tbRefundInfo.Refund_Status_Code == "RSC01";
                }
            }

            return (count, items);
        }

        public async Task<IActionResult> Index(OrderSearchViewModel model)
        {
            if (model.ProductCategories == null || model.ProductCategories.Count == 0)
            {
                model.ProductCategories = (await GetSelectListsCommonCodesAsync("Product_Category_Code")).ToList();
                model.ProductCategories.ForEach(m => m.Selected = true);
            }

            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");


            var result = await GetOrderListAsync(
                model.Keyword,
                model.Period,
                model.StartDate,
                model.EndDate,
                model.ProductCategories.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.OverlapPurchase,
                model.MemberYn,
                model.PaymentStatuseCodes[model.PaymentStatus],
                model.PageFrom,
                model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "Index";
            model.RouteController = "Order";

            return View(model);
        }

        public async Task<IActionResult> IndexExcel(OrderSearchViewModel model)
        {
            if (model.ProductCategories == null || model.ProductCategories.Count == 0)
            {
                model.ProductCategories = (await GetSelectListsCommonCodesAsync("Product_Category_Code")).ToList();
                model.ProductCategories.ForEach(m => m.Selected = true);
            }

            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");

            var result = await GetOrderListAsync(
                model.Keyword,
                model.Period,
                model.StartDate,
                model.EndDate,
                model.ProductCategories.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.OverlapPurchase,
                model.MemberYn,
                model.PaymentStatuseCodes[model.PaymentStatus]);

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
            workSheet.Cells[rowIndex, colIndex++].Value = "제품 브랜드";
            workSheet.Cells[rowIndex, colIndex++].Value = "청첩장구매";
            workSheet.Cells[rowIndex, colIndex++].Value = "이름";
            workSheet.Cells[rowIndex, colIndex++].Value = "아이디";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "제품코드";

            workSheet.Cells[rowIndex, colIndex++].Value = "구매횟수";

            workSheet.Cells[rowIndex, colIndex++].Value = "주문금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "쿠폰금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제수단";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문일";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제상태";
            workSheet.Cells[rowIndex, colIndex++].Value = "예식일";

            foreach (var item in result.Items)
            {
                var colPayment_Status = item.Payment_Status_Name;
                if (item.Payment_Status_Code == "PSC02")
                    colPayment_Status = item.Payment_DateTime?.ToString("yyyy-MM-dd HH:mm");
                if (item.RefundYn)
                    colPayment_Status = "환불접수";

                //구매회수
                int payCount = 0;
                if (string.IsNullOrEmpty(item.User_Id)) //비회원
                {
                    payCount = _barunsonDb.TB_Orders.Where(m => m.Name == item.User_Name && m.Email == item.User_Email && m.Payment_Status_Code == "PSC02").Count();

                  //  payCount = result.Item2.Where(m => string.IsNullOrEmpty(m.User_Id) && m.User_Email == item.User_Email && m.Payment_Status_Code == "PSC02").Count();
                }
                else
                {
                    payCount = _barunsonDb.TB_Orders.Where(m => m.User_ID == item.User_Id && m.Name == item.User_Name && m.Payment_Status_Code == "PSC02").Count();

                   // payCount = result.Item2.Where(m => m.User_Id == item.User_Id && m.Payment_Status_Code == "PSC02").Count();
                }

                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = rowNum;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Brand_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Sales_Gubun_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.User_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.User_Id;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Order_Path;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Payment_Path;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Order_Code;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Code;
                workSheet.Cells[rowIndex, colIndex++].Value = payCount;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Order_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Coupon_Price.HasValue ? item.Coupon_Price : 0;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Payment_Price.HasValue ? item.Payment_Price : 0;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Payment_Method_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Regist_DateTime?.ToString("yyyy-MM-dd HH:mm");
                workSheet.Cells[rowIndex, colIndex++].Value = colPayment_Status;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Wedding_Date;

                rowNum--;

            }

            rowIndex = rowIndex + 3;
            colIndex = colIndex + 1;


            workSheet.Cells[rowIndex, 1].Value = "결제금액:";
            workSheet.Cells[rowIndex, 1].Style.Font.SetFromFont(new Font("맑은 고딕", 11));
            workSheet.Cells[rowIndex, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;


            workSheet.Cells[rowIndex, 2].Value = result.Item2.Where(m => m.Payment_Price.HasValue && m.Payment_Status_Code == "PSC02").Sum(m => m.Payment_Price)?.ToString("#,##0") + "원";
            workSheet.Cells[rowIndex, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex, 2].Style.Font.SetFromFont(new Font("맑은 고딕", 11));

            //PMC01 신용카드
            workSheet.Cells[rowIndex + 1, 1].Value = "카드:";
            workSheet.Cells[rowIndex + 1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex + 1, 1].Style.Font.SetFromFont(new Font("맑은 고딕", 11));
            workSheet.Cells[rowIndex + 1, 2].Value = result.Item2.Where(m => m.Payment_Price.HasValue && m.Payment_Method_Code == "PMC01" && m.Payment_Status_Code == "PSC02").Sum(m => m.Payment_Price)?.ToString("#,##0") + "원";
            workSheet.Cells[rowIndex + 1, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex + 1, 2].Style.Font.SetFromFont(new Font("맑은 고딕", 11));

            //PMC03 계좌이체
            workSheet.Cells[rowIndex + 2, 1].Value = "계좌이체:";
            workSheet.Cells[rowIndex + 2, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex + 2, 1].Style.Font.SetFromFont(new Font("맑은 고딕", 11));
            workSheet.Cells[rowIndex + 2, 2].Value = result.Item2.Where(m => m.Payment_Price.HasValue && m.Payment_Method_Code == "PMC03" && m.Payment_Status_Code == "PSC02").Sum(m => m.Payment_Price)?.ToString("#,##0") + "원";
            workSheet.Cells[rowIndex + 2, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex + 2, 2].Style.Font.SetFromFont(new Font("맑은 고딕", 11));

            //PMC02 무통장입금(가상계좌)
            workSheet.Cells[rowIndex + 3, 1].Value = "무통장:";
            workSheet.Cells[rowIndex + 3, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex + 3, 1].Style.Font.SetFromFont(new Font("맑은 고딕", 11));
            workSheet.Cells[rowIndex + 3, 2].Value = result.Item2.Where(m => m.Payment_Price.HasValue && m.Payment_Method_Code == "PMC02" && m.Payment_Status_Code == "PSC02").Sum(m => m.Payment_Price)?.ToString("#,##0") + "원";
            workSheet.Cells[rowIndex + 3, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex + 3, 2].Style.Font.SetFromFont(new Font("맑은 고딕", 11));

            workSheet.Cells[rowIndex + 4, 1].Value = "취소금액:";
            workSheet.Cells[rowIndex + 4, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex + 4, 1].Style.Font.SetFromFont(new Font("맑은 고딕", 11));
            workSheet.Cells[rowIndex + 4, 2].Value = result.Item2.Where(m => m.Payment_Price.HasValue && (m.Payment_Status_Code == "PSC03" || m.Payment_Status_Code == "PSC05")).Sum(m => m.Payment_Price)?.ToString("#,##0") + "원";
            workSheet.Cells[rowIndex + 4, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells[rowIndex + 4, 2].Style.Font.SetFromFont(new Font("맑은 고딕", 11));

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Order_List" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }
        #endregion

        #region 주문 취소/환불 검색

        private async Task<(int Count, List<OrderCancelSearchDataModel> Items)> GetOrderCancelListAsync(string keyword, DateTime startDate, DateTime endDate,
            List<string> productBarnds, List<string> refundStatuses, List<string> refundTypes,
            int pageFrom = 0, int? pageSize = null)
        {
            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);

            int searchOrderId = 0;
            if (!string.IsNullOrEmpty(keyword))
                int.TryParse(keyword, out searchOrderId);

            //총 아이템 수
            int count = 0;
            List<OrderCancelSearchDataModel> items = null;
            var orderQuery = from o in _barunsonDb.TB_Orders
                             join op in _barunsonDb.TB_Order_Products on o.Order_ID equals op.Order_ID
                             join p in _barunsonDb.TB_Products on op.Product_ID equals p.Product_ID
                             join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                             join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                             join r in _barunsonDb.TB_Refund_Infos on o.Order_ID equals r.Order_ID
                             join pmc in _barunsonDb.TB_Common_Codes on new { Code = o.Payment_Method_Code, Code_Group = "Payment_Method_Code" } equals new { pmc.Code, pmc.Code_Group }
                             join psc in _barunsonDb.TB_Common_Codes on new { Code = o.Payment_Status_Code, Code_Group = "Payment_Status_Code" } equals new { psc.Code, psc.Code_Group }
                             join pbc in _barunsonDb.TB_Common_Codes on new { Code = p.Product_Brand_Code, Code_Group = "Product_Brand_Code" } equals new { pbc.Code, pbc.Code_Group }
                             join pcc in _barunsonDb.TB_Common_Codes on new { Code = p.Product_Category_Code, Code_Group = "Product_Category_Code" } equals new { pcc.Code, pcc.Code_Group }
                             join rtc in _barunsonDb.TB_Common_Codes on new { Code = r.Refund_Type_Code, Code_Group = "Refund_Type_Code" } equals new { rtc.Code, rtc.Code_Group }
                             join rsc in _barunsonDb.TB_Common_Codes on new { Code = r.Refund_Status_Code, Code_Group = "Refund_Status_Code" } equals new { rsc.Code, rsc.Code_Group }
                             where o.Order_DateTime >= startDate && o.Order_DateTime < endDate &&
                                 productBarnds.Contains(p.Product_Brand_Code) &&
                                 refundStatuses.Contains(r.Refund_Status_Code) &&
                                 refundTypes.Contains(r.Refund_Type_Code) &&
                                 (string.IsNullOrEmpty(keyword) || (o.Order_Code.Contains(keyword) || o.Order_ID == searchOrderId || o.Name.Contains(keyword)))
                             orderby o.Order_DateTime descending
                             select new OrderCancelSearchDataModel
                             {
                                 Product_Brand_Code = p.Product_Brand_Code,
                                 Product_Brand_Name = pbc.Code_Name,
                                 User_Id = o.User_ID,
                                 User_Name = o.Name,
                                 User_Email = o.Email,
                                 Member_Type = (o.User_ID == "") ? "G" : "U",
                                 Order_Path = o.Order_Path,
                                 Payment_Path = o.Payment_Path,
                                 Order_ID = o.Order_ID,
                                 Order_Code = o.Order_Code,
                                 Order_DateTime = o.Order_DateTime,
                                 Product_ID = p.Product_ID,
                                 Product_Code = p.Product_Code,
                                 Order_Price = o.Order_Price,
                                 Coupon_Price = o.Coupon_Price,
                                 Payment_Price = o.Payment_Price,
                                 Payment_Method_Code = o.Payment_Method_Code,
                                 Payment_Method_Name = pmc.Code_Name,
                                 Regist_DateTime = o.Regist_DateTime,
                                 Payment_Status_Code = o.Payment_Status_Code,
                                 Payment_Status_Name = psc.Code_Name,
                                 Payment_DateTime = o.Payment_DateTime,
                                 Wedding_Date = id.WeddingDate,
                                 Invitation_Url = id.Invitation_URL,
                                 Refund_Type_Code = r.Refund_Type_Code,
                                 Refund_Type_Name = rtc.Code_Name,
                                 Refund_Status_Code = r.Refund_Status_Code,
                                 Refund_Status_Name = rsc.Code_Name,
                                 Refund_DateTime = r.Refund_DateTime,
                                 Image_URL = p.Main_Image_URL
                             };

            if (pageSize.HasValue)
            {
                //총 아이템 수
                count = await orderQuery.CountAsync();
                //페이지 수 만큼 데이터 읽기
                items = await orderQuery.Skip(pageFrom).Take(pageSize.Value).ToListAsync();
            }
            else
            {
                items = await orderQuery.ToListAsync();
                count = items.Count;
            }

            return (count, items);
        }

        public async Task<IActionResult> Cancel(OrderCancelSearchViewModel model)
        {
            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }
            if (model.RefundStatuses == null || model.RefundStatuses.Count == 0)
            {
                model.RefundStatuses = (await GetSelectListsCommonCodesAsync("Refund_Status_Code")).ToList();
                model.RefundStatuses.ForEach(m => m.Selected = true);
            }
            if (model.RefundTypes == null || model.RefundTypes.Count == 0)
            {
                model.RefundTypes = (await GetSelectListsCommonCodesAsync("Refund_Type_Code")).ToList();
                model.RefundTypes.ForEach(m => m.Selected = true);
            }
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");

            var result = await GetOrderCancelListAsync(
                model.Keyword,
                model.StartDate,
                model.EndDate,
                model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.RefundStatuses.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.RefundTypes.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.PageFrom,
                model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;

            model.ReturnUrl = Request.GetEncodedPathAndQuery();
            model.RouteAction = "Cancel";
            model.RouteController = "Order";

            return View(model);
        }
        public async Task<IActionResult> CancelExcel(OrderCancelSearchViewModel model)
        {
            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }
            if (model.RefundStatuses == null || model.RefundStatuses.Count == 0)
            {
                model.RefundStatuses = (await GetSelectListsCommonCodesAsync("Refund_Status_Code")).ToList();
                model.RefundStatuses.ForEach(m => m.Selected = true);
            }
            if (model.RefundTypes == null || model.RefundTypes.Count == 0)
            {
                model.RefundTypes = (await GetSelectListsCommonCodesAsync("Refund_Type_Code")).ToList();
                model.RefundTypes.ForEach(m => m.Selected = true);
            }
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");

            var result = await GetOrderCancelListAsync(
               model.Keyword,
               model.StartDate,
               model.EndDate,
               model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList(),
               model.RefundStatuses.Where(m => m.Selected).Select(m => m.Value).ToList(),
               model.RefundTypes.Where(m => m.Selected).Select(m => m.Value).ToList());

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
            workSheet.Cells[rowIndex, colIndex++].Value = "브랜드";
            workSheet.Cells[rowIndex, colIndex++].Value = "이름";
            workSheet.Cells[rowIndex, colIndex++].Value = "아이디";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "제품코드";
            workSheet.Cells[rowIndex, colIndex++].Value = "주문금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "쿠폰금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제금액";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제수단";
            workSheet.Cells[rowIndex, colIndex++].Value = "환불방법";
            workSheet.Cells[rowIndex, colIndex++].Value = "결제일";
            workSheet.Cells[rowIndex, colIndex++].Value = "환불상태";


            foreach (var item in result.Items)
            {
                var colBarunsonmCardUrl = new Uri(model.BarunsonmCardUrl, item.Invitation_Url);

                var colRefund_Status = item.Refund_Status_Name;
                if (item.Refund_Status_Code == "RSC02")  //환불완료
                    colRefund_Status = item.Refund_DateTime?.ToString("yyyy-MM-dd HH:mm");

                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = rowNum;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Brand_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.User_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.User_Id;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Order_Code;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Product_Code;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Order_Price;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Coupon_Price.HasValue ? item.Coupon_Price : 0;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Payment_Price.HasValue ? item.Payment_Price : 0;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Payment_Method_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Refund_Type_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Order_DateTime?.ToString("yyyy-MM-dd HH:mm");
                workSheet.Cells[rowIndex, colIndex++].Value = colRefund_Status;

                rowNum--;

            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Order_List" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }
        #endregion



        #region 회원으로 전환할 비회원 주문건 관련


        /// <summary>
        /// 회원으로 전환할 비회원 주문 검색
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="productCategory"></param>
        /// <param name="productBarnd"></param>
        /// <param name="overlapPurchase"></param>
        /// <param name="memberYn"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="pageFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<(int Count, List<OrderSearchDataModel> Items)> GetOrderNonMemberSearchListAsync(string keyword,
            List<string> productCategory, List<string> productBarnd,
            List<string> paymentStatus, int pageFrom = 0, int? pageSize = null)
        {
           
            //총 아이템 수
            int count = 0;
            List<OrderSearchDataModel> items = null;

            if(!string.IsNullOrEmpty(keyword))
            {
                var orderQuery = from o in _barunsonDb.TB_Orders
                                 join op in _barunsonDb.TB_Order_Products on o.Order_ID equals op.Order_ID
                                 join p in _barunsonDb.TB_Products on op.Product_ID equals p.Product_ID
                                 join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                                 join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                                 join d in _barunsonDb.TB_Common_Codes on new { Code = o.Payment_Method_Code, Code_Group = "PAYMENT_METHOD_CODE" } equals new { d.Code, d.Code_Group } into gd
                                 from pm in gd.DefaultIfEmpty()
                                 join e in _barunsonDb.TB_Common_Codes on new { Code = o.Payment_Status_Code, Code_Group = "PAYMENT_STATUS_CODE" } equals new { e.Code, e.Code_Group } into ge
                                 from ps in ge.DefaultIfEmpty()
                                 join f in _barunsonDb.TB_Common_Codes on new { Code = p.Product_Brand_Code, Code_Group = "Product_Brand_Code" } equals new { f.Code, f.Code_Group } into gf
                                 from pb in gf.DefaultIfEmpty()
                                 where /*(
                                        (period == "Order" && o.Regist_DateTime >= startDate && o.Regist_DateTime < endDate) ||
                                        (period == "Pay" && o.Payment_DateTime >= startDate && o.Payment_DateTime < endDate)
                                 ) && */
                                     /*o.Payment_Method_Code != null && */
                                     productCategory.Contains(p.Product_Category_Code) &&
                                     productBarnd.Contains(p.Product_Brand_Code) &&
                                     //(string.IsNullOrEmpty(keyword) || (o.User_ID.Contains(keyword) || o.Name.Contains(keyword) || o.Email.Contains(keyword) || o.Order_Code.Contains(keyword))) &&
                                     //(memberYn == "ALL" || ((memberYn == "G" && o.User_ID == "") || (memberYn == "U" && o.User_ID != ""))) &&
                                     ((o.User_ID == "" || string.IsNullOrEmpty(o.User_ID)) && o.Order_Code.Contains(keyword)) /*|| o.User_ID == "s4guest"*/ &&
                                     (paymentStatus == null || paymentStatus.Contains(o.Payment_Status_Code))
                                 select new OrderSearchDataModel
                                 {
                                     Product_Brand_Code = p.Product_Brand_Code,
                                     Product_Brand_Name = pb.Code_Name,
                                     User_Id = o.User_ID,
                                     User_Name = o.Name,
                                     User_Email = o.Email,
                                     Member_Type = (o.User_ID == "") ? "G" : "U",
                                     Order_Path = o.Order_Path,
                                     Payment_Path = o.Payment_Path,
                                     Order_ID = o.Order_ID,
                                     Order_Code = o.Order_Code,
                                     Product_ID = p.Product_ID,
                                     Product_Code = p.Product_Code,
                                     Order_Price = o.Order_Price,
                                     Coupon_Price = o.Coupon_Price,
                                     Payment_Price = o.Payment_Price,
                                     Payment_Method_Code = o.Payment_Method_Code,
                                     Payment_Method_Name = pm.Code_Name,
                                     Regist_DateTime = o.Regist_DateTime,
                                     Payment_Status_Code = o.Payment_Status_Code,
                                     Payment_Status_Name = ps.Code_Name,
                                     Payment_DateTime = o.Payment_DateTime,
                                     Wedding_Date = id.WeddingDate,
                                     Invitation_Url = id.Invitation_URL
                                 };
                    orderQuery = orderQuery.OrderByDescending(m => m.Order_ID);

                if (pageSize.HasValue)
                {
                    //총 아이템 수
                    count = await orderQuery.CountAsync();
                    //페이지 수 만큼 데이터 읽기
                    items = await orderQuery.Skip(pageFrom).Take(pageSize.Value).ToListAsync();
                }
                else
                {
                    items = await orderQuery.ToListAsync();
                    count = items.Count;
                }


            }
           
            return (count, items);
        }

        public async Task<IActionResult> Transform(OrderSearchViewModel model)
        {
            if (model.ProductCategories == null || model.ProductCategories.Count == 0)
            {
                model.ProductCategories = (await GetSelectListsCommonCodesAsync("Product_Category_Code")).ToList();
                model.ProductCategories.ForEach(m => m.Selected = true);
            }

            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");


            var result = await GetOrderNonMemberSearchListAsync(
                model.Keyword,
                model.ProductCategories.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.PaymentStatuseCodes[model.PaymentStatus],
                model.PageFrom,
                model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "Transform";
            model.RouteController = "Order";

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> OrderUpdate([FromBody] List<object> data)
        {
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(data[0].ToString());
            string UserId = values["UserId"];
            int OrderId = int.Parse(values["OrderId"].ToString()) ;

            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            
            if (data.Count > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                try
                {
                    var query = from a in _barunsonDb.VW_Users
                                where a.USER_ID.Equals(UserId)
                                select a;
                    var cateItems = await query.ToListAsync();

                    if (cateItems.Count == 0)
                    {
                        result.status = false;
                        result.message = "존재하지 않는 아이디입니다.";
                        // await transaction.RollbackAsync();
                    }
                    else
                    {
                        string admin_id = CurrentUserId();

                        TB_Order order_item;
                        TB_Invitation invitation_item;
                        order_item = await (from a in _barunsonDb.TB_Orders
                                            where a.Order_ID.Equals(OrderId) && string.IsNullOrEmpty(a.User_ID)
                                            select a).FirstOrDefaultAsync();
                        if (order_item == null)
                        {
                            result.status = false;
                            result.message = "주문건이 존재하지 않습니다.";
                            // await transaction.RollbackAsync();
                        }
                        else
                        {
                            order_item.User_ID = UserId;
                            order_item.Update_DateTime = DateTime.Now;
                            order_item.Update_User_ID = admin_id;

                            await _barunsonDb.SaveChangesAsync();


                            invitation_item = await (from a in _barunsonDb.TB_Invitations
                                                     where a.Order_ID.Equals(OrderId)
                                                     select a).FirstOrDefaultAsync();


                            if (invitation_item == null)
                            {
                                result.status = false;
                                result.message = "주문건이 존재하지 않습니다.";

                                //await transaction.RollbackAsync();

                            }
                            else
                            {
                                invitation_item.User_ID = UserId;
                                invitation_item.Update_DateTime = DateTime.Now;
                                invitation_item.Update_User_ID = admin_id;

                                await _barunsonDb.SaveChangesAsync();

                                // await transaction.CommitAsync();

                                result.status = true;
                                result.message = "";
                            }

                        }
                    }

                    if (result.status) await transaction.CommitAsync();
                    else await transaction.RollbackAsync();
                }
                catch (Exception ex)
                {
                    result.status = false; result.message = ex.ToString();// "알수없는 에러가 발생하였습니다.";

                    await transaction.RollbackAsync();
                }

                
            }

            return Json(result);
        }
        #endregion





        #region 환불완료된 주문건을 제작중 상태로 변경(쿠폰 전액 결제 용도)

        public async Task<IActionResult> Reset(OrderSearchViewModel model)
        {
            if (model.ProductCategories == null || model.ProductCategories.Count == 0)
            {
                model.ProductCategories = (await GetSelectListsCommonCodesAsync("Product_Category_Code")).ToList();
                model.ProductCategories.ForEach(m => m.Selected = true);
            }

            if (model.ProductBarnds == null || model.ProductBarnds.Count == 0)
            {
                model.ProductBarnds = (await GetSelectListsCommonCodesAsync("Product_Brand_Code")).ToList();
                model.ProductBarnds.ForEach(m => m.Selected = true);
            }
            model.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");


            var result = await GetOrderRefundMemberSearchListAsync(
                model.Keyword,
                model.ProductCategories.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.ProductBarnds.Where(m => m.Selected).Select(m => m.Value).ToList(),
                model.PaymentStatuseCodes[model.PaymentStatus],
                model.PageFrom,
                model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;
            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "Transform";
            model.RouteController = "Order";

            return View(model);
        }

        /// <summary>
        /// 환불완료된 주문건 리스트 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="productCategory"></param>
        /// <param name="productBarnd"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="pageFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<(int Count, List<OrderSearchDataModel> Items)> GetOrderRefundMemberSearchListAsync(string keyword,
            List<string> productCategory, List<string> productBarnd,
            List<string> paymentStatus, int pageFrom = 0, int? pageSize = null)
        {
            //총 아이템 수
            int count = 0;
            List<OrderSearchDataModel> items = null;

            if (!string.IsNullOrEmpty(keyword))
            {
                var orderQuery = from o in _barunsonDb.TB_Orders
                                 join op in _barunsonDb.TB_Order_Products on o.Order_ID equals op.Order_ID
                                 join p in _barunsonDb.TB_Products on op.Product_ID equals p.Product_ID
                                 join r in _barunsonDb.TB_Refund_Infos on o.Order_ID equals r.Order_ID 
                                 join i in _barunsonDb.TB_Invitations on o.Order_ID equals i.Order_ID
                                 join id in _barunsonDb.TB_Invitation_Details on i.Invitation_ID equals id.Invitation_ID
                                 join d in _barunsonDb.TB_Common_Codes on new { Code = o.Payment_Method_Code, Code_Group = "PAYMENT_METHOD_CODE" } equals new { d.Code, d.Code_Group } into gd
                                 from pm in gd.DefaultIfEmpty()
                                 join e in _barunsonDb.TB_Common_Codes on new { Code = o.Payment_Status_Code, Code_Group = "PAYMENT_STATUS_CODE" } equals new { e.Code, e.Code_Group } into ge
                                 from ps in ge.DefaultIfEmpty()
                                 join f in _barunsonDb.TB_Common_Codes on new { Code = p.Product_Brand_Code, Code_Group = "Product_Brand_Code" } equals new { f.Code, f.Code_Group } into gf
                                 from pb in gf.DefaultIfEmpty()
                                 where /*(
                                        (period == "Order" && o.Regist_DateTime >= startDate && o.Regist_DateTime < endDate) ||
                                        (period == "Pay" && o.Payment_DateTime >= startDate && o.Payment_DateTime < endDate)
                                 ) && */
                                     /*o.Payment_Method_Code != null && */
                                     productCategory.Contains(p.Product_Category_Code) &&
                                     productBarnd.Contains(p.Product_Brand_Code) && o.Order_Code.Contains(keyword) && r.Refund_Status_Code.Equals("RSC02") && o.Payment_Status_Code.Equals("PSC03") &&
                                     //(string.IsNullOrEmpty(keyword) || (o.User_ID.Contains(keyword) || o.Name.Contains(keyword) || o.Email.Contains(keyword) || o.Order_Code.Contains(keyword))) &&
                                     //(memberYn == "ALL" || ((memberYn == "G" && o.User_ID == "") || (memberYn == "U" && o.User_ID != ""))) &&

                                     (paymentStatus == null || paymentStatus.Contains(o.Payment_Status_Code))
                                 select new OrderSearchDataModel
                                 {
                                     Product_Brand_Code = p.Product_Brand_Code,
                                     Product_Brand_Name = pb.Code_Name,
                                     User_Id = o.User_ID,
                                     User_Name = o.Name,
                                     User_Email = o.Email,
                                     Member_Type = (o.User_ID == "") ? "G" : "U",
                                     Order_Path = o.Order_Path,
                                     Payment_Path = o.Payment_Path,
                                     Order_ID = o.Order_ID,
                                     Order_Code = o.Order_Code,
                                     Product_ID = p.Product_ID,
                                     Product_Code = p.Product_Code,
                                     Order_Price = o.Order_Price,
                                     Coupon_Price = o.Coupon_Price,
                                     Payment_Price = o.Payment_Price,
                                     Payment_Method_Code = o.Payment_Method_Code,
                                     Payment_Method_Name = pm.Code_Name,
                                     Regist_DateTime = o.Regist_DateTime,
                                     Payment_Status_Code = o.Payment_Status_Code,
                                     Payment_Status_Name = ps.Code_Name,
                                     Payment_DateTime = o.Payment_DateTime,
                                     Wedding_Date = id.WeddingDate,
                                     Invitation_Url = id.Invitation_URL
                                 };
                orderQuery = orderQuery.OrderByDescending(m => m.Order_ID);

                if (pageSize.HasValue)
                {
                    //총 아이템 수
                    count = await orderQuery.CountAsync();
                    //페이지 수 만큼 데이터 읽기
                    items = await orderQuery.Skip(pageFrom).Take(pageSize.Value).ToListAsync();
                }
                else
                {
                    items = await orderQuery.ToListAsync();
                    count = items.Count;
                }


            }

            return (count, items);
        }



        [HttpPost]
        public async Task<IActionResult> OrderReset([FromBody] List<object> data)
        {
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(data[0].ToString());
            int OrderId = int.Parse(values["OrderId"].ToString());

            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            if (data.Count > 0)
            {
                using var transaction = await _barunsonDb.Database.BeginTransactionAsync();

                try
                {  
                    string admin_id = CurrentUserId();

                    TB_Order order_item;
                    order_item = await (from a in _barunsonDb.TB_Orders
                                        where a.Order_ID.Equals(OrderId)
                                        select a).FirstOrDefaultAsync();

                    order_item.Payment_Method_Code = null;
                    order_item.Coupon_Price = null;
                    order_item.Payment_Price = null;
                    order_item.Payment_Status_Code = "PSC01";
                    order_item.Order_Status_Code = "OSC01";
                    order_item.Update_User_ID = admin_id;
                    order_item.Update_DateTime =  DateTime.Now;
                    order_item.Payment_DateTime = null;
                    order_item.Payment_Path = null;
                    order_item.Order_Path = null;
                    order_item.Deposit_DeadLine_DateTime = null;
                    order_item.Cancel_Time = "00";
                    order_item.Account_Number = null;
                    order_item.Trading_Number = null;
                    order_item.Order_DateTime = null;
                    order_item.Cancel_DateTime = null;
                    order_item.Finance_Auth_Number = null;
                    order_item.Finance_Name = null;
                    order_item.Payer_Name = null;
                    order_item.Card_Installment = null;

                    await _barunsonDb.SaveChangesAsync();
                            
                    result.status = true;
                    result.message = "";
                    
                    if (result.status) await transaction.CommitAsync();
                    else await transaction.RollbackAsync();
                }
                catch (Exception ex)
                {
                    result.status = false; result.message = ex.ToString();// "알수없는 에러가 발생하였습니다.";

                    await transaction.RollbackAsync();
                }


            }

            return Json(result);
        }

        #endregion

    }


}
