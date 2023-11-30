using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 메뉴 관리
    /// </summary>
    public class MenuViewModel
    {
        public List<MenuItem> Items { get; set; }
        public class MenuItem
        {
            public int MenuID { get; set; }
            public int? ParentMenuID { get; set; }
            public string MenuName { get; set; }
            public string MenuTypeCode { get; set; }
            public string MenuURL { get; set; }
            public int Sort { get; set; }
            public bool BoldYN { get; set; }
            public string ImageURL { get; set; }
            public string ImageAbsoluteUrl { get; set; }

            public string Message { get; set; }
        }
    }
}
