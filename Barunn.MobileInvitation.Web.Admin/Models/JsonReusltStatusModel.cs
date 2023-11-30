using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class JsonReusltStatusModel
    {
        public bool status { get; set; }
        public string message { get; set; }

        public string url { get; set; }
        public List<string> errors { get; set; }
    }
}
