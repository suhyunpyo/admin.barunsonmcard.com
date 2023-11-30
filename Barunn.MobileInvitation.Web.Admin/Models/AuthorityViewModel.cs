using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 관리자 레벨
    /// </summary>
    public enum AdminLevel : byte
    {
        /// <summary>
        /// 운영자
        /// </summary>
        [Display(Name = "운영자")]
        Manager = 1,
        /// <summary>
        /// 관리자
        /// </summary>
        [Display(Name = "관리자")]
        Admin = 2
    }

    /// <summary>
    /// 관리자 검색 뷰모델
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AdminMemberViewModel<TEntity> : PageViewModel
    {
        public string Searchtxt { get; set; } = "";

        /// <summary>
        /// 바인딩 아이템 목록
        /// </summary>
        public TEntity DataModel { set; get; }
    }

    public class AdminRegisterViewModel
    {
        [Required(ErrorMessage = "ID를 입력하세요.")]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required(ErrorMessage = "이름을 입력하세요.")]
        [Display(Name = "이름")]
        public string Name { get; set; }

        [Required(ErrorMessage = "비밀번호를 입력하세요.")]
        [StringLength(20, ErrorMessage = "비밀번호를 4자리 이상 입력하세요.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        public string Password { get; set; }

        public AdminLevel Level { get; set; } = AdminLevel.Manager;
    }
}
