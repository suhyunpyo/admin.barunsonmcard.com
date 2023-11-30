using Barunn.MobileInvitation.Common.Helpers;
using Barunn.MobileInvitation.Common.Models;
using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Dac.Contexts;
using Barunn.MobileInvitation.Dac.Models.BarShop;
using Barunn.MobileInvitation.Dac.Models.Barunson;
using Barunn.MobileInvitation.Web.Admin.Models;
using Barunn.MobileInvitation.Web.Admin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {
        private readonly ITossPaymentService _tossPay;

        public MemberController(ILogger<MemberController> logger, IOptions<BarunnConfig> barunnConfig,   BarunsonContext barunsonDb, BarShopContext barshopDb, ITossPaymentService tossPaymentService)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _tossPay = tossPaymentService;

        }

        #region Amin page Login

        [HttpGet]
        [AllowAnonymous] // 인증되지 않은 사용자도 접근 가능
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string ADMIN_ID, string ADMIN_pwd, string returnUrl = null)
        {
            ViewBag.Message = "아이디 또는 비밀번호가 일치하지 않습니다.";

            try
            {
                var account = await _barshopDb.S2_AdminLists
                                .FromSqlInterpolated($"select * from S2_AdminList where admin_id = {ADMIN_ID} and  PWDCOMPARE({ADMIN_pwd}, password) = 1 and access_flag = 1")
                                .SingleOrDefaultAsync();

                if (account != null)
                {
                    //로그인 기록
                    var loginInfo = new Admin_LoginIpInfo
                    {
                        referer_url = $"{Request.Scheme}://{Request.Host}",
                        ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                        user_agent = Request.Headers["User-Agent"].ToString(),
                        uid = account.admin_id,
                        uname = account.admin_name,
                        umail = account.admin_mail,
                        regdate = DateTime.Now
                    };
                    _barshopDb.Admin_LoginIpInfos.Add(loginInfo);
                    await _barshopDb.SaveChangesAsync();

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Sid, account.admin_id),
                        new Claim(ClaimTypes.NameIdentifier, account.admin_id),
                        new Claim(ClaimTypes.Name, account.admin_name),
                        new Claim(ClaimTypes.Email, account.admin_mail ?? ""),
                        new Claim(ClaimTypes.Role, account.admin_level.ToString())
                    };
                    var ci = new ClaimsIdentity(claims, _barunnConfig.AuthCookie.DefaultScheme);

                    var authenticationProperties = new AuthenticationProperties()
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                        IssuedUtc = DateTimeOffset.UtcNow,
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(new ClaimsPrincipal(ci), authenticationProperties);

                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("TotalDaily", "Statistics");
                    else
                        return LocalRedirect(returnUrl);
                }
            }
            catch(Exception ex)  
            {
                ViewBag.Message = ex.ToString();

            }

            return View();
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(_barunnConfig.AuthCookie.DefaultScheme);

            return Redirect("/");
        }

        #endregion

        #region 회원 관리

        /// <summary>
        /// 어드민 - 회원관리 - 회원목록
        /// </summary>
        /// <param name="startDate">가입일?주문일</param>
        /// <param name="endDate">가입일?주문일</param>
        /// <param name="memberType">전체/회원/비회원</param>
        /// <param name="searchtxt">이름또는ID</param>
        /// <returns></returns>
        private async Task<(int Count, List<AdmimOrderMember> Items)> GetUserListAsync(DateTime startDate, DateTime endDate, string memberType, string searchtxt, int pageFrom = 0, int? pageSize = null)
        {
            //End Date는 날짜 시간으로 하루 더하여 조건 검색
            endDate = endDate.AddDays(1);

            //주문 테이블 
            var orderQuery = from o in _barunsonDb.TB_Orders
                             join b in _barunsonDb.TB_Invitations on o.Order_ID equals b.Order_ID
                             join c in _barunsonDb.TB_Invitation_Details on b.Invitation_ID equals c.Invitation_ID
                             where o.Regist_DateTime > startDate &&
                                   o.Regist_DateTime < endDate &&
                                   o.Email != "" &&
                                   (string.IsNullOrEmpty(searchtxt) || (o.User_ID.Contains(searchtxt) || o.Name.Contains(searchtxt) || o.Email.Contains(searchtxt))) &&
                                   (memberType == "ALL" || ((memberType == "G" && o.User_ID == "") || (memberType == "U" && o.User_ID != "")))
                             group new { o, c } by new { o.User_ID, o.Name, o.Email } into g
                             orderby g.Max(m => m.o.Order_ID) descending
                             select new AdmimOrderMember
                             {
                                 Member_Type = (g.Key.User_ID == "") ? "G" : "U",
                                 User_Id = (g.Key.User_ID == "") ? g.Key.Email : g.Key.User_ID,
                                 User_Name = g.Key.Name,
                                 Regist_Datetime = g.Max(m => m.o.Regist_DateTime),
                                 Order_ID = g.Max(m => m.o.Order_ID),
                                 Wedding_Date = g.Max(m => m.c.WeddingDate)
                             };
            //총 아이템 수
            int count = 0;
            List<AdmimOrderMember> orderItems = null;
            if (pageSize.HasValue)
            {
                //총 아이템 수
                count = await orderQuery.CountAsync();
                //페이지 수 만큼 데이터 읽기
                orderItems = await orderQuery.Skip(pageFrom).Take(pageSize.Value).ToListAsync();
            }
            else
            {
                orderItems = await orderQuery.ToListAsync();
                count = orderItems.Count;
            }
            //All User ID
            var allIds = orderItems.Select(m => m.User_Id).ToList();
            //Member ID 
            var memberIds = orderItems
                            .Where(m => m.Member_Type == "U")
                            .Select(m => m.User_Id)
                            .ToList();
            //Non Member Emails
            var nonMemberIds = orderItems
                                .Where(m => m.Member_Type == "G")
                                .Select(m => m.User_Id)
                                .ToList();

                           
            //회원 테이블 
            var memberQuery = from u in _barshopDb.S2_UserInfo_TheCards
                              where memberIds.Contains(u.uid)
                              select new
                              {
                                  u.uid,
                                  u.uname,
                                  u.umail,
                                  u.REFERER_SALES_GUBUN,
                                  u.reg_date
                              };
            var memberItems = await memberQuery.ToListAsync();

            //청첩장 주문
            var customOrderQuery = from o in _barshopDb.custom_orders
                                   where allIds.Contains(o.member_id) &&
                                   o.status_seq > 9 &&
                                   o.card_seq > 0
                                   group o by o.member_id into g
                                   select g.Key;
            var customOrderItems = await customOrderQuery.ToListAsync();
            //멤버 모초 유,무료 구입여부
            var meberBmByeQuery = from o in _barunsonDb.TB_Orders
                             where memberIds.Contains(o.User_ID)
                             group o by o.User_ID  into g
                             select new
                             {
                                 User_ID = g.Key,
                                 BMBuy = g.Sum(m => (m.Payment_Status_Code == "PSC02" && m.Payment_Price > 0) ? 1 : 0),
                                 BMFree = g.Sum(m => (m.Payment_Status_Code == "PSC02" && (m.Payment_Price == null || m.Payment_Price == 0)) ? 1 : 0),
                             };
            var meberBmByeItems = await meberBmByeQuery.ToListAsync();

            //비멤버 모초 유,무료 구입여부
            var nonMeberBmByeQuery = from o in _barunsonDb.TB_Orders
                                  where nonMemberIds.Contains(o.Email)
                                  group o by o.Email into g
                                  select new
                                  {
                                      User_ID = g.Key,
                                      BMBuy = g.Sum(m => (m.Payment_Status_Code == "PSC02" && m.Payment_Price > 0) ? 1 : 0),
                                      BMFree = g.Sum(m => (m.Payment_Status_Code == "PSC02" && (m.Payment_Price == null || m.Payment_Price == 0)) ? 1 : 0),
                                  };
            var nonMeberBmByeItems = await nonMeberBmByeQuery.ToListAsync();

            foreach (var item in orderItems)
            {
                if (item.Member_Type == "U")  //회원
                {
                    var member = memberItems.FirstOrDefault(m => m.uid == item.User_Id);
                    if (member != null)
                    {
                        if (!string.IsNullOrEmpty(member.uname) && member.uname != item.User_Name)
                            item.User_Name = member.uname;

                        item.Join_Type = SalesGubunCodeModel.SalesGubunCodes.FirstOrDefault(m => m.Code == member.REFERER_SALES_GUBUN)?.ShortName;

                        item.Regist_Datetime = member.reg_date;
                    }
                    else
                    {
                        //탈퇴 회원
                        item.User_Name += "(탈퇴)";
                    }
                    //모초 유,무료 구입여부
                    item.BMBuy = meberBmByeItems.SingleOrDefault(m => m.User_ID == item.User_Id)?.BMBuy > 0;
                    item.BMFree = meberBmByeItems.SingleOrDefault(m => m.User_ID == item.User_Id)?.BMFree > 0;
                }
                else
                {
                    item.Join_Type = "비회원";

                    //모초 유,무료 구입여부
                    item.BMBuy = nonMeberBmByeItems.SingleOrDefault(m => m.User_ID == item.User_Id)?.BMBuy > 0;
                    item.BMFree = nonMeberBmByeItems.SingleOrDefault(m => m.User_ID == item.User_Id)?.BMFree > 0;
                }
                //청첩장 주문
                item.InvitationBuy = customOrderItems.Exists(m => m == item.User_Id);
                
            }

            return (count, orderItems);
        }

        /// <summary>
        /// 구매 회원 조회
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> IndexAsync(MemberViewModel<List<AdmimOrderMember>> model)
        {
            var result = await GetUserListAsync(model.StartDate.Date, model.EndDate.Date, model.SeachMemberYn, model.Searchtxt, model.PageFrom, model.PageSize);

            model.Count = result.Count;
            model.DataModel = result.Items;

            model.ReturnUrl = Request.GetEncodedPathAndQuery();

            model.RouteAction = "Index";
            model.RouteController = "Member";
            model.RouteData = new Dictionary<string, string> {
                {"StartDate", model.StartDate.ToString("yyyy-MM-dd")},
                {"EndDate", model.EndDate.ToString("yyyy-MM-dd")},
                {"SeachMemberYn", model.SeachMemberYn},
                {"Searchtxt", model.Searchtxt},
                {"TermType", model.TermType.ToString()},
            };

            return View("Index", model);
        }

        /// <summary>
        /// 회원 조회 엑셀 출력
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="SeachMemberYn"></param>
        /// <param name="Searchtxt"></param>
        /// <returns></returns>
        public async Task<IActionResult> IndexExcelAsync(DateTime StartDate, DateTime EndDate, string SeachMemberYn, string Searchtxt)
        {
            int rowIndex = 1;
            int colIndex = 1;

            var result = await GetUserListAsync(StartDate.Date, EndDate.Date, SeachMemberYn, Searchtxt);

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Row(1).Height = 25;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.SetFromFont(new Font("맑은 고딕", 10));

            workSheet.Cells[rowIndex, colIndex++].Value = "번호";
            workSheet.Cells[rowIndex, colIndex++].Value = "이름";
            workSheet.Cells[rowIndex, colIndex++].Value = "ID";
            workSheet.Cells[rowIndex, colIndex++].Value = "청첩장구매";
            workSheet.Cells[rowIndex, colIndex++].Value = "모초무료제작";
            workSheet.Cells[rowIndex, colIndex++].Value = "모초유료제작";
            workSheet.Cells[rowIndex, colIndex++].Value = "가입구분";
            workSheet.Cells[rowIndex, colIndex++].Value = "예식일자";
            workSheet.Cells[rowIndex, colIndex++].Value = "회원가입";

            foreach (var item in result.Items)
            {
                rowIndex++;
                colIndex = 1;

                workSheet.Cells[rowIndex, colIndex++].Value = (rowIndex - 1);
                workSheet.Cells[rowIndex, colIndex++].Value = item.User_Name;
                workSheet.Cells[rowIndex, colIndex++].Value = item.User_Id;
                workSheet.Cells[rowIndex, colIndex++].Value = item.InvitationBuy ? "○" : "";
                workSheet.Cells[rowIndex, colIndex++].Value = item.BMFree ? "○" : "";
                workSheet.Cells[rowIndex, colIndex++].Value = item.BMBuy ? "○" : "";
                workSheet.Cells[rowIndex, colIndex++].Value = item.Join_Type;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Wedding_Date;
                workSheet.Cells[rowIndex, colIndex++].Value = item.Regist_Datetime?.ToString("yyyy-MM-dd");
            }

            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MemberList" + DateTime.Now.ToShortTimeString() + ".xlsx");
        }

        /// <summary>
        /// 회원상세
        /// </summary>
        /// <param name="memberType">U : 회원 / G : 비회원 </param>
        /// <param name="user_Id">회원: ID, 비회원: email</param>
        /// <returns></returns>
        private async Task<AdminOrderMemberDetail> GetUserDetailAsyn(string memberType, string user_Id)
        {
            var result = new AdminOrderMemberDetail();

            result.BarunsonmCardUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");

            #region 기본정보
            if (memberType == "U")   //회원 정보, 회원 데이터의 전화번호번호등 부정확함. 주문으로 대체필요.
            {
                var basicQuery = from u in _barshopDb.S2_UserInfo_TheCards
                                 where u.uid == user_Id
                                 select new { u.uid, u.uname, u.phone1, u.phone2, u.phone3, u.umail, u.reg_date };

                var basicInfo = await basicQuery.FirstOrDefaultAsync();
                if (basicInfo != null)
                {
                    result.User_Name = basicInfo.uname;
                    result.User_Id = basicInfo.uid;
                    result.User_Hp = basicInfo.phone1 + basicInfo.phone2 + basicInfo.phone3;
                    result.User_Email = basicInfo.umail;
                    result.Regist_Datetime = basicInfo.reg_date;
                }
                else
                {
                    result.User_Name = "탈퇴회원";
                    result.User_Id = user_Id;
                    result.User_Hp = "";
                    result.User_Email = "";
                    result.Regist_Datetime = null;

                }
            }
            else //비회원
            {
                var basicQuery = from o in _barunsonDb.TB_Orders
                                 where o.Email == user_Id
                                 orderby o.Order_ID descending
                                 select new { o.Email, o.Name, o.CellPhone_Number };
                var basicInfo = await basicQuery.FirstAsync();

                result.User_Name = basicInfo.Name;
                result.User_Id = basicInfo.Email;
                result.User_Hp = basicInfo.CellPhone_Number;
                result.User_Email = basicInfo.Email;
                result.Regist_Datetime = null;
            }
            #endregion

            #region 2. 주문정보(주문번호, 상품코드, 주문금액, 쿠폰사용금액, 결재금액, 결재수단, 결재일자, 결제취소, 모초 바로가기 URL) and 4. 결재 환불 정보 

            var reFundTypeCode = await (from m in _barunsonDb.TB_Common_Codes where m.Code_Group == "REFUND_TYPE_CODE" || m.Code_Group == "REFUND_STATUS_CODE" select m).ToListAsync();

            result.OrderList = new List<AdminOrderMemberDetail.OrderInfo>();
            result.PayAndRefundInfoList = new List<AdminOrderMemberDetail.PayAndRefundInfo>();

            var orderQuery = from a in _barunsonDb.TB_Orders
                             join b in _barunsonDb.TB_Order_Products on a.Order_ID equals b.Order_ID
                             join c in _barunsonDb.TB_Products on b.Product_ID equals c.Product_ID
                             join g in _barunsonDb.TB_Invitations on a.Order_ID equals g.Order_ID
                             join h in _barunsonDb.TB_Invitation_Details on g.Invitation_ID equals h.Invitation_ID
                             join d in _barunsonDb.TB_Common_Codes on new { Code = a.Payment_Method_Code, Code_Group = "PAYMENT_METHOD_CODE" } equals new { d.Code, d.Code_Group } into gd
                             from paymentMethod in gd.DefaultIfEmpty()
                             join e in _barunsonDb.TB_Common_Codes on new { Code = a.Payment_Status_Code, Code_Group = "PAYMENT_STATUS_CODE" } equals new { e.Code, e.Code_Group } into ge
                             from paymentStatus in ge.DefaultIfEmpty()
                             where (memberType == "U" && a.User_ID == user_Id) || (memberType == "G" && a.Email == user_Id)
                             orderby a.Regist_DateTime descending
                             select new
                             {
                                 Order_Code = a.Order_Code,
                                 Order_ID = a.Order_ID,
                                 Product_ID = c.Product_ID,
                                 Product_Code = c.Product_Code,
                                 Order_Price = a.Order_Price,
                                 Payment_Price = a.Payment_Price,
                                 Payment_Method_Code = a.Payment_Method_Code,
                                 Payment_Method_Name = paymentMethod.Code_Name,
                                 Payment_Status_Code = a.Payment_Status_Code,
                                 Payment_Status_Name = paymentStatus.Code_Name,
                                 Regist_DateTime = a.Regist_DateTime,
                                 Order_DateTime = a.Order_DateTime,
                                 Cancel_DateTime = a.Cancel_DateTime,
                                 Invitation_Url = h.Invitation_URL,
                                 Wedding_Date = h.WeddingDate,
                                 Finance_Name = a.Finance_Name,
                                 Account_Number = a.Account_Number,
                                 Payment_DateTime = a.Payment_DateTime,
                                 User_Name = a.Name,
                                 User_Hp = a.CellPhone_Number,
                                 User_Email = a.Email
                             };

            var orderList = await orderQuery.ToListAsync();
            // 회원 데이터의 전화번호번호등 부정확하여 최근 주문 정보로 전화번호, Email 수정 및 예식일 업데이트
            var lastOrder = orderList.FirstOrDefault();
            if (lastOrder != null)
            {
                result.User_Name = lastOrder.User_Name;
                result.User_Hp = lastOrder.User_Hp;
                result.User_Email = lastOrder.User_Email;
                result.Wedding_Date = lastOrder.Wedding_Date;
                result.LastOrderID = lastOrder.Order_ID;
            }
            //All Order ID
            var allOrderIds = orderList.Select(m => m.Order_ID).Distinct().ToList();
            //환불 정보
            var refundQuery = from r in _barunsonDb.TB_Refund_Infos
                              where allOrderIds.Contains(r.Order_ID)
                              orderby r.Refund_ID descending
                              select r;
            var refundItems = await refundQuery.ToListAsync();

            foreach (var item in orderList)
            {
                //Payment_Method_Code가 Null 일경우 주문 진행 되지 않은 것으로 보임.. 화면 표시에서 제거
                if (string.IsNullOrEmpty(item.Payment_Method_Code))
                    continue;

                //주문 정보 채우기
                var orderItem = new AdminOrderMemberDetail.OrderInfo
                {
                    Order_Code = item.Order_Code,
                    Order_ID = item.Order_ID,
                    Product_ID = item.Product_ID,
                    Product_Code = item.Product_Code,
                    Order_Price = item.Order_Price,
                    Payment_Price = item.Payment_Price,
                    Payment_Method_Code = item.Payment_Method_Code,
                    Payment_Method_Name = item.Payment_Method_Name,
                    Payment_Status_Code = item.Payment_Status_Code,
                    Payment_Status_Name = item.Payment_Status_Name,
                    Regist_DateTime = item.Regist_DateTime,
                    Order_DateTime = item.Order_DateTime,
                    Cancel_DateTime = item.Cancel_DateTime,
                    Invitation_Url = item.Invitation_Url,
                    Wedding_Date = item.Wedding_Date,
                    Coupon_Use_Price = await (from m in _barunsonDb.TB_Order_Coupon_Uses where m.Order_ID == item.Order_ID select m.Discount_Price).SumAsync(),
                    RefundYn = false
                };
                //결제 정보 채우기
                var payItem = new AdminOrderMemberDetail.PayAndRefundInfo
                {
                    Order_Id = item.Order_ID,
                    Order_Code = item.Order_Code,
                    Payment_Price = item.Payment_Price,
                    Payment_Method_Code = item.Payment_Method_Code,
                    Payment_Method_Name = item.Payment_Method_Name,
                    Payment_Status_Code = item.Payment_Status_Code,
                    Payment_Status_Name = item.Payment_Status_Name,
                    Finance_Name = item.Finance_Name,
                    Account_Number = item.Account_Number,
                    Regist_Datetime = item.Regist_DateTime,
                    Payment_Datetime = item.Payment_DateTime,
                    Invitation_Url = item.Invitation_Url,
                };
                //환불 정보 채우기
                var tbRefundInfo = refundItems.Where(m => m.Order_ID == item.Order_ID).FirstOrDefault();
                if (tbRefundInfo != null)
                {
                    payItem.Refund_Price = tbRefundInfo.Refund_Price;
                    payItem.Refund_Datetime = tbRefundInfo.Refund_DateTime;
                    payItem.Refund_Type_Code = tbRefundInfo.Refund_Type_Code;
                    payItem.Refund_Type_Name = reFundTypeCode.FirstOrDefault(m => m.Code == payItem.Refund_Type_Code && m.Code_Group == "REFUND_TYPE_CODE")?.Code_Name;
                    payItem.Refund_Status_Code = tbRefundInfo.Refund_Status_Code;
                    payItem.Refund_Status_Name = reFundTypeCode.FirstOrDefault(m => m.Code == payItem.Refund_Status_Code && m.Code_Group == "REFUND_STATUS_CODE")?.Code_Name;

                    //무통장환불접수여부
                    orderItem.RefundYn = tbRefundInfo.Refund_Type_Code == "RTC03" && tbRefundInfo.Refund_Status_Code == "RSC01";
                }

                result.OrderList.Add(orderItem);
                result.PayAndRefundInfoList.Add(payItem);


            }

            #endregion

            #region 3. 쿠폰 정보
            var couponQuery = from a in _barunsonDb.TB_Coupon_Publishes
                              join b in _barunsonDb.TB_Coupons on a.Coupon_ID equals b.Coupon_ID
                              where a.User_ID == result.User_Id
                              orderby a.Regist_DateTime descending
                              select new AdminOrderMemberDetail.CouponInfo
                              {
                                  Coupon_Name = b.Coupon_Name,
                                  Coupon_Id = a.Coupon_ID,
                                  Use_Yn = a.Use_YN,
                                  Discount_Price = b.Discount_Price,
                                  Discount_Rate = b.Discount_Rate,
                                  Discount_Method_Code = b.Discount_Method_Code,
                                  Use_DateTime = a.Use_DateTime,
                                  Regist_Datetime = a.Regist_DateTime,
                                  Retrieve_Datetime = a.Retrieve_DateTime,
                                  Expiration_Date = a.Expiration_Date,
                                  Coupon_Publish_Id = a.Coupon_Publish_ID
                              };
            result.CouponList = await couponQuery.ToListAsync();

            #endregion

            #region 5. 고객 상담
            var userQnAQuery = from m in _barshopDb.S2_UserQnAs
                               where m.member_id == result.User_Id && m.company_seq == 8070 && m.sales_gubun == "BM"
                               orderby m.reg_dt descending
                               select new AdminOrderMemberDetail.UserQnA
                               {
                                   Title = m.q_title,
                                   Content = m.q_content,
                                   Regist_Datetime = m.reg_dt,
                                   Answer_Datetime = m.a_dt,
                                   Answer_Id = m.a_id
                               };

            result.UserQnAList = await userQnAQuery.ToListAsync();
            #endregion

            #region 6. 관리자 메모 
            var orderIds = orderList.Select(m => m.Order_ID).ToList();
            var memoQuery = from a in _barunsonDb.TB_Admin_Memos
                            where orderIds.Contains(a.Order_ID.Value)
                            orderby a.Regist_DateTime descending
                            select new AdminOrderMemberDetail.AdminMemo
                            {
                                Memo_ID = a.Memo_ID,
                                Content = a.Content,
                                Regist_User_ID = a.Regist_User_ID,
                                Regist_User_Name = a.Regist_User_ID,
                                Regist_DateTime = a.Regist_DateTime
                            };
            result.AdminMemoList = await memoQuery.ToListAsync();

            #endregion

            return result;
        }

        /// <summary>
        /// 회원 상세 정보
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="memberType">구분 U : 회원 / G : 비회원</param>
        /// <param name="page"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> DetailAsync(string id, string memberType, string returnUrl)
        {
            var model = await GetUserDetailAsyn(memberType, id);

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        #endregion

        #region 환불

        /// <summary>
        /// 환불 수정 화면 
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> RefundInfoAsync(int id)
        {
            var query = from o in _barunsonDb.TB_Orders
                        join r in _barunsonDb.TB_Refund_Infos
                        on o.Order_ID equals r.Order_ID into or
                        from refund in or.DefaultIfEmpty()
                        where o.Order_ID == id
                        select new RefundInfoViewModel
                        {
                            Order_ID = o.Order_ID,
                            Order_Code = o.Order_Code,
                            Trading_Number = o.Trading_Number,
                            Payment_Price = o.Payment_Price,
                            Payment_Method_Code = o.Payment_Method_Code,
                            Refund_Type_Code = refund.Refund_Type_Code,
                            Refund_Price = refund.Refund_Price ?? o.Payment_Price,
                            Bank_Type_Code = refund.Bank_Type_Code,
                            AccountNumber = refund.AccountNumber,
                            Refund_Status_Code = refund.Refund_Status_Code,
                            Depositor_Name = refund.Depositor_Name,
                            Refund_Content = refund.Refund_Content
                        };

            var model = await query.FirstOrDefaultAsync();
            model.RefundTyeCodes = await GetSelectListsCommonCodesAsync("Refund_Type_Code");
            model.RefundStatusCodes = await GetSelectListsCommonCodesAsync("Refund_Status_Code");
            model.BankCodes = await GetSelectListsCommonCodesAsync("Bank_Code");

            
            if (string.IsNullOrEmpty(model.Refund_Type_Code))
            {
                if (model.Payment_Method_Code == "PMC01") //신용카드
                {
                    model.RefundTyeCodes = model.RefundTyeCodes.Where(m => m.Value == "RTC01");
                    model.RefundStatusCodes = model.RefundStatusCodes.Where(m => m.Value == "RSC02"); //환불완료 
                    model.IsBankInput = false;
                }
                else if (model.Payment_Method_Code == "PMC02") //무통장입금(가상계좌)
                {
                    model.RefundTyeCodes = model.RefundTyeCodes.Where(m => m.Value == "RTC03");
                    model.RefundStatusCodes = model.RefundStatusCodes.Where(m => m.Value == "RSC01"); //환불접수  
                    model.IsBankInput = true;
                }
                else if (model.Payment_Method_Code == "PMC03") //계좌이체
                {
                    model.RefundTyeCodes = model.RefundTyeCodes.Where(m => m.Value == "RTC02");
                    model.RefundStatusCodes = model.RefundStatusCodes.Where(m => m.Value == "RSC02"); //환불완료 
                    model.IsBankInput = false;
                }
                else if (model.Payment_Method_Code == "PMC04") //기타(쿠폰)
                {
                    model.RefundTyeCodes = model.RefundTyeCodes.Where(m => m.Value == "RTC04");
                    model.RefundStatusCodes = model.RefundStatusCodes.Where(m => m.Value == "RSC02"); //환불완료 
                    model.IsBankInput = false;
                }
            }
            else // 환불 데이터 있음.
            {
                if (model.Refund_Status_Code == "RSC02") //환불완료, 저장 불가. 모든 필드 비활성
                {
                    model.IsSave = false;
                }
                else if (model.Refund_Status_Code == "RSC01" || model.Refund_Status_Code == "RSC04") //환불접수, 환불취소
                {
                    model.RefundStatusCodes = model.RefundStatusCodes.Where(m => m.Value != "RSC01");
                }
            }

            return View("RefundInfo", model);
        }

        /// <summary>
        /// 환불 저장
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RefundSaveAsync(RefundInfoViewModel model)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };

            if (model.Refund_Type_Code == "RTC03") //가상계좌 취소
            {
                if (string.IsNullOrWhiteSpace(model.Bank_Type_Code))
                {
                    result.message = "입금은행을 입력하세요.";
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.AccountNumber))
                {
                    result.message = "계좌번호를 입력하세요.";
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.Depositor_Name))
                {
                    result.message = "예금주를 입력하세요.";
                    return Json(result);
                }
            }

            var now = DateTime.Now;
            try
            {
                var Update_Order_Item = await (from o in _barunsonDb.TB_Orders where o.Order_ID == model.Order_ID select o).FirstAsync();

                TB_Refund_Info item = new TB_Refund_Info
                {
                    Order_ID = model.Order_ID,
                    Regist_DateTime = now,
                };

                if (Update_Order_Item.Payment_Method_Code == "PMC01") //신용카드
                {
                    item.Refund_Type_Code = "RTC01";
                    item.Refund_Status_Code = "RSC02";  //환불완료 
                }
                else if(Update_Order_Item.Payment_Method_Code == "PMC02") //무통장입금(가상계좌)
                {
                    item.Refund_Type_Code = "RTC03";
                    item.Refund_Status_Code = "RSC01";  //환불접수  
                }
                else if (Update_Order_Item.Payment_Method_Code == "PMC03") //계좌이체
                {
                    item.Refund_Type_Code = "RTC02";
                    item.Refund_Status_Code = "RSC02";  //환불완료  
                }
                else if (Update_Order_Item.Payment_Method_Code == "PMC04") //기타(쿠폰)
                {
                    item.Refund_Type_Code = "RTC04";
                    item.Refund_Status_Code = "RSC02";  //환불완료  
                }

                item.Bank_Type_Code = model.Bank_Type_Code;
                item.AccountNumber = model.AccountNumber;
                item.Depositor_Name = model.Depositor_Name;
                item.Refund_Price = model.Refund_Price;
                item.Refund_Content = model.Refund_Content;

                item.Refund_DateTime = DateTime.Now;
                item.Regist_IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                item.Regist_User_ID = CurrentUserId();

                //TODO: toss에서는 가상계좌 환불 가능함. 
                // 기존 bankcode 수정 및 환불업무 협의 후 코드 구현.
                if (item.Refund_Type_Code == "RTC01" || item.Refund_Type_Code == "RTC02") //카드취소 , 계좌이체취소
                {

                    //Toss
                    var response = await _tossPay.CancelAsync(model.Trading_Number, new TossPostPaymentCancel
                    {
                        cancelReason = "고객이 결제취소/환불 요청",
                    });
                    if (response == null || response.cancels == null)
                    {
                        throw new Exception($"결제 취소요청이 실패하였습니다.(Code: {response?.code}, message: {response?.message})");
                    }
                }
                _barunsonDb.Add(item);

                Update_Order_Item.Payment_Status_Code = "PSC03";
                Update_Order_Item.Update_DateTime = now;

                if (item.Refund_Type_Code != "RTC03")  //가상계좌 취소가 아닌 카드,계좌이체 결제취소 
                {
                    Update_Order_Item.Cancel_DateTime = now;
                    Update_Order_Item.Cancel_Time = now.ToString("hh");

                    //쿠폰원복 
                    var couponUse = await (from c in _barunsonDb.TB_Order_Coupon_Uses where c.Order_ID == item.Order_ID select c).FirstOrDefaultAsync();
                    if (couponUse != null)
                    {
                        var couponPublish = await (from c in _barunsonDb.TB_Coupon_Publishes where c.Coupon_Publish_ID == couponUse.Coupon_Publish_ID select c).FirstAsync();
                        couponPublish.Use_YN = "N";
                        couponPublish.Use_DateTime = null;

                        _barunsonDb.Remove(couponUse);
                    }
                }

                await _barunsonDb.SaveChangesAsync();
                result.status = true;
                result.message = "";
            }
            catch (Exception e)
            {
                result.status = false;
                result.message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region 쿠폰전액결제 전환
        [HttpGet]
        public async Task<IActionResult> PaymethodChange(int id)
        {
            var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            try
            {
                var now = DateTime.Now;

                using (var trans = await _barunsonDb.Database.BeginTransactionAsync())
                {
                    //주문 정보
                    var orderQuery = from m in _barunsonDb.TB_Orders
                                     where m.Order_ID == id 
                                       && m.Payment_Method_Code == "PMC02" && m.Payment_Status_Code == "PSC04" //무통장, 입금대기
                                     select m;
                    var orderItem = await orderQuery.FirstOrDefaultAsync();
                    if (orderItem == null)
                        throw new Exception("입금대기중인 주문정보를 찾을 수 없습니다.");

                    //쿠폰정보
                    var couponQuery = from cp in _barunsonDb.TB_Coupon_Publishes
                                      join c in _barunsonDb.TB_Coupons on cp.Coupon_ID equals c.Coupon_ID
                                      where cp.User_ID == orderItem.User_ID
                                        && cp.Retrieve_DateTime == null //회수날짜
                                        && cp.Use_YN == "N"
                                        && c.Discount_Method_Code == "DMC03"  //전액할인 
                                        && (cp.Expiration_Date == null || cp.Expiration_Date.CompareTo(now.ToString("yyyy-MM-dd")) >= 0)
                                      orderby cp.Regist_DateTime
                                      select new { c.Coupon_ID, cp.Coupon_Publish_ID };

                    var exitsCoupon = await couponQuery.FirstOrDefaultAsync();
                    if (exitsCoupon == null)
                        throw new Exception("사용가능한 전액쿠폰 발급내역이 없습니다.");

                    //update order
                    var CopuponPrice = orderItem.Payment_Price;

                    orderItem.Payment_Method_Code = "PMC04";    //기타(쿠폰)  
                    orderItem.Payment_Status_Code = "PSC02";    //결제완료
                    orderItem.Payment_DateTime = now;
                    orderItem.Payment_Price = null;
                    orderItem.Order_Status_Code = "OSC01";      //주문완료
                    orderItem.Coupon_Price = CopuponPrice;
                    orderItem.Finance_Name = null;
                    orderItem.Payer_Name = null;
                    orderItem.Escrow_YN = null;
                    orderItem.Account_Number = null;
                    orderItem.Trading_Number = null;
                    orderItem.Cancel_DateTime = null;
                    orderItem.Cancel_Time = null;
                    orderItem.Deposit_DeadLine_DateTime = null;
                    orderItem.Order_Path = "PC";
                    orderItem.Payment_Path = "PC";
                    orderItem.Update_DateTime = now;

                    //update Copon Use
                    var cpItem = await (from m in _barunsonDb.TB_Coupon_Publishes where m.Coupon_Publish_ID == exitsCoupon.Coupon_Publish_ID select m).FirstAsync();
                    cpItem.Use_YN = "Y";
                    cpItem.Use_DateTime = now;

                    var orderCouponUseItem = new TB_Order_Coupon_Use
                    {
                        Order_ID = orderItem.Order_ID,
                        Coupon_Publish_ID = exitsCoupon.Coupon_Publish_ID,
                        Discount_Price = CopuponPrice,
                        Regist_User_ID = CurrentUserId(),
                        Regist_DateTime = now,
                        Regist_IP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                        Update_User_ID = CurrentUserId(),
                        Update_DateTime = now,
                        Update_IP = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                    };
                    _barunsonDb.TB_Order_Coupon_Uses.Add(orderCouponUseItem);

                    await _barunsonDb.SaveChangesAsync();
                    await trans.CommitAsync();

                    result.status = true;
                    result.message = "";
                }
            }
            catch (Exception e)
            {
                result.status = false;
                result.message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Admin to User Login
        public async Task<IActionResult> UserLogin(int id)
        {
            var query = from a in _barunsonDb.TB_Orders
                        where a.Order_ID == id
                        select new AuthTicketModel
                        {
                            Name = a.Name,
                            UserID = a.User_ID,
                            Email = a.Email,
                            Issue = DateTime.Now
                        };
            var item = await query.FirstAsync();

            var jsonstring = JsonSerializer.Serialize(item);
            var authTicket = AesHelper.Encrypt128(jsonstring, AesHelper.AES_DEFAULT_KEY);

            var url = new UriBuilder(_barunnConfig.UserSiteUrl);
            url.Path = "Member/CSLogin"; // "Member/Mypage";
            url.Query = $"ticket={HttpUtility.UrlEncode(authTicket)}";
            return Redirect(url.ToString());

        }
		#endregion

		/// <summary>
		/// 메모 저장
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> MemoSaveAsync(AdminOrderMemberDetail.AdminMemo model, string NowUrl)
		{

            //var result = new JsonReusltStatusModel { status = false, message = "알수없는 에러가 발생하였습니다." };
            string returnUrl = NowUrl;

            var now = DateTime.Now;
            try
            {
                TB_Admin_Memo memo = new TB_Admin_Memo
                {
                    Order_ID = model.Order_ID,
                    Regist_DateTime = now,
                    Regist_IP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Regist_User_ID = CurrentUserId(),
                    Content = model.Content
                };

                _barunsonDb.Add(memo);

                await _barunsonDb.SaveChangesAsync();
             
            }
            catch (Exception e)
            {
            }
            return Redirect(returnUrl);
        }


	}
}
