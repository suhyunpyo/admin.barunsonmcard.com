using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class IconViewModel
    {
        public int Icon_ID { get; set; }
        public string Icon_URL { get; set; }

        public bool Selected { get; set; }

        /// <summary>
        /// 포함 제품 수
        /// </summary>
        public int Products { get; set; }
    }
}
