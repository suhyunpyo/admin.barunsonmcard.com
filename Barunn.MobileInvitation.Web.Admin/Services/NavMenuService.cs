using Barunn.MobileInvitation.Common.Models;
using Barunn.MobileInvitation.Web.Admin.Models;
using System.Collections.Generic;
using System.Linq;

namespace Barunn.MobileInvitation.Web.Admin.Services
{
    public class NavMenuService
    {
        public List<NavMenuModel> MenuItems { get; set; }

        public NavMenuService()
        {
            MenuItems = new List<NavMenuModel>();
        }

        public IOrderedEnumerable<NavMenuModel> GetParentMenus(NavMenuModel menu)
        {
            var reuslt = new List<NavMenuModel>();
            reuslt.Add(menu);
            reuslt.AddRange(ParentMenus(menu));

            return reuslt.OrderBy(m => m.Id);
        }
        /// <summary>
        /// 상위 메뉴 제귀 출력
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private IEnumerable<NavMenuModel> ParentMenus(NavMenuModel menu)
        {
            foreach(var item in MenuItems.Where(x => x.Id == menu.ParentId))
            {
                yield return item;
                if (item.ParentId > 0)
                {
                    foreach (var parent in ParentMenus(item))
                        yield return parent;
                }
            }
        }
        
        public static NavMenuService Menus
        {
            get
            {
                var ShowAll = AdminRole.None;
                var ShowMember = AdminRole.Product | AdminRole.Order | AdminRole.Market | AdminRole.Operation | AdminRole.Admin;

                var MenuList = new NavMenuService();

                //Top Menu
                MenuList.MenuItems.Add(new NavMenuModel(10, null, "TotalDaily", "Statistics", "통계관리", 0, ShowMember));
                MenuList.MenuItems.Add(new NavMenuModel(20, null, "Index", "Member", "회원관리", 0, ShowAll));
                MenuList.MenuItems.Add(new NavMenuModel(30, null, "Index", "Productlist", "상품관리", 0, ShowAll));
                MenuList.MenuItems.Add(new NavMenuModel(40, null, "Index", "Order", "주문관리", 0, ShowAll));
                MenuList.MenuItems.Add(new NavMenuModel(50, null, "Index", "Coupon", "쿠폰관리", 0, ShowAll));
                MenuList.MenuItems.Add(new NavMenuModel(60, null, "Index", "Banner", "운영관리", 0, ShowAll));
                MenuList.MenuItems.Add(new NavMenuModel(70, null, "SalesDaily", "Flagift", "화환선물", 0, ShowAll) { MenuPosition = 2 });
                MenuList.MenuItems.Add(new NavMenuModel(80, null, "TotalDaily", "KakaoRemit", "송금서비스", 0, ShowAll) { MenuPosition = 2 });

                //통계관리
                MenuList.MenuItems.Add(new NavMenuModel(100, null, null, null, "전체현황", 10));
                MenuList.MenuItems.Add(new NavMenuModel(101, null, "TotalDaily", "Statistics", "일별", 100));
                MenuList.MenuItems.Add(new NavMenuModel(102, null, "TotalMonthly", "Statistics", "월별", 100));
                MenuList.MenuItems.Add(new NavMenuModel(110, null, null, null, "매출통계", 10));
                MenuList.MenuItems.Add(new NavMenuModel(111, null, "SalesDaily", "Statistics", "일별", 110));
                MenuList.MenuItems.Add(new NavMenuModel(122, null, "SalesMonthly", "Statistics", "월별", 110));
                MenuList.MenuItems.Add(new NavMenuModel(130, null, "PurchasePattern", "Statistics", "구매패턴", 10));
                MenuList.MenuItems.Add(new NavMenuModel(140, null, null, null, "결제수단", 10));
                MenuList.MenuItems.Add(new NavMenuModel(141, null, "PayTypeDaily", "Statistics", "일별", 140));
                MenuList.MenuItems.Add(new NavMenuModel(142, null, "PayTypeMonthly", "Statistics", "월별", 140));
                MenuList.MenuItems.Add(new NavMenuModel(150, null, "Products", "Statistics", "제품통계", 10));
                MenuList.MenuItems.Add(new NavMenuModel(160, null, "Produce", "Statistics", "제작통계", 10));

                //회원관리
                MenuList.MenuItems.Add(new NavMenuModel(200, null, "Index", "Member", "회원관리", 20));
                MenuList.MenuItems.Add(new NavMenuModel(201, null, "Detail", "Member", "회원상세", 200, isDisplay: false));
                //MenuList.MenuItems.Add(new NavMenuModel(210, null, "Index", "Authority", "권한관리", 20, AdminRole.Admin));
              
                //상품관리
                MenuList.MenuItems.Add(new NavMenuModel(300, null, null, null, "상품관리", 30));
                MenuList.MenuItems.Add(new NavMenuModel(301, null, "Index", "Productlist", "상품목록", 300));
                MenuList.MenuItems.Add(new NavMenuModel(302, null, "Index", "ProductRegist", "상품등록", 300));
                MenuList.MenuItems.Add(new NavMenuModel(303, null, "Edit", "ProductRegist", "상품수정", 300, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(304, null, "Save", "ProductRegist", "상품저장", 300, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(310, null, null, null, "진열관리", 30));
                MenuList.MenuItems.Add(new NavMenuModel(311, null, "MainDisplayList", "Productlist", "메인 진열", 310));
                MenuList.MenuItems.Add(new NavMenuModel(312, null, "CateDisplayList", "Productlist", "카테고리 진열", 310));

                MenuList.MenuItems.Add(new NavMenuModel(320, null, null, null, "분류관리", 30));
                MenuList.MenuItems.Add(new NavMenuModel(321, null, "MainIndex", "Category", "메인 분류", 320));
                MenuList.MenuItems.Add(new NavMenuModel(322, null, "Index", "Category", "카테고리 분류", 320));
                
                //주문관리
                MenuList.MenuItems.Add(new NavMenuModel(400, null, null, null, "주문관리", 40));
                MenuList.MenuItems.Add(new NavMenuModel(401, null, "Index", "Order", "주문목록", 400));
                MenuList.MenuItems.Add(new NavMenuModel(402, null, "Cancel", "Order", "취소/환불", 400));
                MenuList.MenuItems.Add(new NavMenuModel(403, null, "Transform", "Order", "비회원주문 -> 회원주문전환", 400));
                MenuList.MenuItems.Add(new NavMenuModel(404, null, "Reset", "Order", "주문상태변경", 400));

                //쿠폰관리
                MenuList.MenuItems.Add(new NavMenuModel(500, null, null, null, "쿠폰관리", 50));
                MenuList.MenuItems.Add(new NavMenuModel(501, null, "Index", "Coupon", "쿠폰목록", 500));
                MenuList.MenuItems.Add(new NavMenuModel(510, null, "Edit", "Coupon", "쿠폰수정", 501, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(515, null, "Save", "Coupon", "쿠폰수정", 501, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(511, null, "PublishList", "Coupon", "쿠폰발생목록", 501, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(512, null, "PublishAdd", "Coupon", "쿠폰발생", 501, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(502, null, "Add", "Coupon", "쿠폰등록", 500, ShowMember));


                MenuList.MenuItems.Add(new NavMenuModel(520, null, null, null, "시리얼쿠폰", 50));
                MenuList.MenuItems.Add(new NavMenuModel(521, null, "Index", "SerialCoupon", "쿠폰 발급/조회", 520));
                MenuList.MenuItems.Add(new NavMenuModel(530, null, "Edit", "SerialCoupon", "쿠폰수정", 521, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(531, null, "Save", "SerialCoupon", "쿠폰수정", 521, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(532, null, "PublishList", "SerialCoupon", "쿠폰발생목록", 521, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(533, null, "PublishAdd", "SerialCoupon", "쿠폰발생", 521, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(522, null, "Add", "SerialCoupon", "쿠폰 생성/등록", 520, ShowMember));

               

                //운영관리
                MenuList.MenuItems.Add(new NavMenuModel(600, null, null, null, "배너관리", 60));
                MenuList.MenuItems.Add(new NavMenuModel(610, null, "Index", "Banner", "배너목록", 600));
                MenuList.MenuItems.Add(new NavMenuModel(611, null, "Register", "Banner", "배너등록", 610, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(612, null, "Edit", "Banner", "배너수정", 610, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(613, null, "Save", "Banner", "배너저장", 610, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(620, null, "Category", "Banner", "배너분류", 600));

                MenuList.MenuItems.Add(new NavMenuModel(625, null, "BannerManageList", "Flagift", "화환배너", 600));
                MenuList.MenuItems.Add(new NavMenuModel(626, null, "BannerManageEdit", "Flagift", "화환배너 등록/수정", 625, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(627, null, "BannerManageSave", "Flagift", "화환배너 등록/수정", 625, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(630, null, "Index", "Popup", "팝업관리", 60));
                MenuList.MenuItems.Add(new NavMenuModel(631, null, "Register", "Popup", "팝업등록", 630, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(632, null, "Edit", "Popup", "팝업수정", 630, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(633, null, "Save", "Popup", "팝업저장", 630, isDisplay: false));
                
                MenuList.MenuItems.Add(new NavMenuModel(640, null, "Index", "Notice", "공지사항", 60));
                MenuList.MenuItems.Add(new NavMenuModel(641, null, "Add", "Notice", "공지사항", 640, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(642, null, "Edit", "Notice", "공지사항", 640, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(643, null, "Save", "Notice", "공지사항", 640, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(650, null, "Index", "FAQ", "FAQ", 60));
                MenuList.MenuItems.Add(new NavMenuModel(651, null, "Add", "FAQ", "FAQ", 650, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(652, null, "Edit", "FAQ", "FAQ", 650, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(653, null, "Save", "FAQ", "FAQ", 650, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(660, null, "", "", "1:1 문의", 60, isDisplay: true, alert: "1:1문의는 빠른손에서 확인하세요!"));

                MenuList.MenuItems.Add(new NavMenuModel(670, null, "IconManage", "ProductList", "아이콘 관리", 60));
                MenuList.MenuItems.Add(new NavMenuModel(671, null, "IconUpload", "ProductList", "아이콘 관리", 670, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(672, null, "IconDelete", "ProductList", "아이콘 관리", 670, isDisplay: false));

                MenuList.MenuItems.Add(new NavMenuModel(680, null, "", "", "메뉴 관리", 60));
                MenuList.MenuItems.Add(new NavMenuModel(651, null, "Index", "Menu", "상단 GNB", 680, AdminRole.None,true, null, new Dictionary<string, string> { { "id", "MTC01" } }));
                MenuList.MenuItems.Add(new NavMenuModel(652, null, "Index", "Menu", "풋터", 680, AdminRole.None, true, null, new Dictionary<string, string> { { "id", "MTC02" } }));

                MenuList.MenuItems.Add(new NavMenuModel(690, null, "Index", "Policy", "약관 관리", 60));
                MenuList.MenuItems.Add(new NavMenuModel(691, null, "Add", "Policy", "약관 관리", 690, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(692, null, "Edit", "Policy", "약관 관리", 690, isDisplay: false));
                MenuList.MenuItems.Add(new NavMenuModel(693, null, "Save", "Policy", "약관 관리", 690, isDisplay: false));

                //화환선물
                MenuList.MenuItems.Add(new NavMenuModel(700, null, null, null, "매출현황", 70));
                MenuList.MenuItems.Add(new NavMenuModel(701, null, "SalesDaily", "Flagift", "일별", 700));
                MenuList.MenuItems.Add(new NavMenuModel(702, null, "SalesMonthly", "Flagift", "월별", 700));
                MenuList.MenuItems.Add(new NavMenuModel(710, null, null, null, "주문관리", 70));
                MenuList.MenuItems.Add(new NavMenuModel(711, null, "OrderList", "Flagift", "주문별", 710));
                MenuList.MenuItems.Add(new NavMenuModel(712, null, "FlaOrderList", "Flagift", "건별", 710));
                MenuList.MenuItems.Add(new NavMenuModel(713, null, "FlaRefundList", "Flagift", "취소/환불", 710));

                //송금서비스
                MenuList.MenuItems.Add(new NavMenuModel(800, null, null, null, "매출현황", 80));
                MenuList.MenuItems.Add(new NavMenuModel(801, null, "TotalDaily", "KakaoRemit", "일별", 800));
                MenuList.MenuItems.Add(new NavMenuModel(802, null, "TotalMonthly", "KakaoRemit", "월별", 800));
                MenuList.MenuItems.Add(new NavMenuModel(810, null, null, null, "주문관리", 80));
                MenuList.MenuItems.Add(new NavMenuModel(811, null, "OrderList", "KakaoRemit", "주문별", 810));
                MenuList.MenuItems.Add(new NavMenuModel(812, null, "RemitList", "KakaoRemit", "건별", 810));
                MenuList.MenuItems.Add(new NavMenuModel(820, null, null, null, "설정", 80));
                MenuList.MenuItems.Add(new NavMenuModel(821, null, "SettingTax", "KakaoRemit", "수수료", 820));
                return MenuList;
            }
        }
    }
}
