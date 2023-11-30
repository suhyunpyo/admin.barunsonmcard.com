using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace Barunn.MobileInvitation.Common.Models
{
    public class SalesGubunCodeModel
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }

        public static List<SalesGubunCodeModel> SalesGubunCodes
        {
            get
            {
                return new List<SalesGubunCodeModel>
                {
                    new SalesGubunCodeModel { Code = "SB", ShortName="바", Name="바른손" },
                    new SalesGubunCodeModel { Code = "SA", ShortName="비", Name="비핸즈" },
                    new SalesGubunCodeModel { Code = "ST", ShortName="더", Name="더카드" },
                    new SalesGubunCodeModel { Code = "SS", ShortName="프", Name="프리미어" },
                    new SalesGubunCodeModel { Code = "SD", ShortName="디", Name="디얼디어" },
                    new SalesGubunCodeModel { Code = "B", ShortName="몰", Name="바른손몰" },
                    new SalesGubunCodeModel { Code = "H", ShortName="몰", Name="바른손몰" },
                    new SalesGubunCodeModel { Code = "BM", ShortName="M", Name="모바일" }
                };
            }
        }
    }
    public static class MemberGubunModel
    {
        public static IEnumerable<SelectListItem> MemberGubuns
        {
            get
            {
                return new SelectList(
                    new List<SelectListItem>
                    {
                                    new SelectListItem { Text = "전체", Value = "ALL" },
                                    new SelectListItem { Text = "회원", Value = "U" },
                                    new SelectListItem { Text = "비회원", Value = "G" },
                    }, "Value", "Text");
            }
        }
    }

    /// <summary>
    /// 관리자 역할
    /// </summary>
    [Flags]
    public enum AdminRole : byte
    {
        None = 0,            //없음, bit연산 불가, 단일 할당만 가능.
        Product = 1 << 0,    //상품관리: 1
        Order = 1 << 1,      //주문관리: 2
        Operation = 1 << 2,  //운영관리: 4
        Market = 1 << 3,     //마케팅 관리: 8
        CS = 1 << 4,         //CS
        Admin = 1 << 6,      //어드민: 64
    }
}
