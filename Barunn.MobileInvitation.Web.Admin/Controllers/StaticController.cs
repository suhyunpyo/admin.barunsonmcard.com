using Barunn.MobileInvitation.Common.Helpers;
using Barunn.MobileInvitation.Common.Models.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    public class StaticController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly BarunnConfig _barunnConfig;
        private readonly IStaticContentHelper _staticContent;

        public StaticController(ILogger<StaticController> logger, IOptions<BarunnConfig> barunnConfig,
            IStaticContentHelper staticContent)
        {
            _logger = logger;
            _barunnConfig = barunnConfig.Value;
            _staticContent = staticContent;
        }

        [Route("static/{*id}")]
        public IActionResult StaticFile(string id)
        {
            var localPath = Path.Combine(_barunnConfig.FileConfig.UploadPath, id);

            var file = new FileInfo(localPath);
            if (!file.Exists)
            {
                var url = _staticContent.DefaultAbsoluteUri(id).AbsoluteUri;
                return Redirect(url);
                
            }
            else
            {
                string contentType;
                new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider().TryGetContentType(file.FullName, out contentType);
                return File(file.OpenRead(), contentType);
            }
        }
    }
}
