using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    /// <summary>
    /// 로그인 사용자 구분
    /// </summary>
    public enum UserAuthType
    {
        User = 0,
        Manager = 1,
        Admin = 2,
    }
    public class UserLoginInfo
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string CellPhone { get; set; }
        public string EMail { get; set; }
        public string DupInfo { get; set; }
        public DateTime? WeddingDate { get; set; }

        public UserAuthType AuthType { get; set; }
    }
}
