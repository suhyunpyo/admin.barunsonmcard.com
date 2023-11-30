using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Web.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    public class NaverMapController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly BarunnConfig _barunnConfig;
        public NaverMapController(ILogger<NaverMapController> logger, IOptions<BarunnConfig> barunnConfig)
        {
            _logger = logger;
            _barunnConfig = barunnConfig.Value;
        }
        public IActionResult index()
        {
            Map map = new Map
            {
                ApiId = _barunnConfig.Map.NaverCloudId,
                ApiKey = _barunnConfig.Map.NaverCloudKey,
                DefaultMapLat = _barunnConfig.Map.DefaultMapLat,
                DefaultMapLot = _barunnConfig.Map.DefaultMapLot
            };
            return View("index", map);
        }

        [HttpGet]
        public async Task<IActionResult> ReverseGeocodeForNaver(string lat, string lot)
        {
            string result = "";

            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri("https://naveropenapi.apigw.ntruss.com/map-reversegeocode/v2/gc?encoding=utf-8&request=coordsToaddr&coord=latlng&output=json&coords=" + lot + "," + lat);
                    request.Headers.Add("X-NCP-APIGW-API-KEY-ID", _barunnConfig.Map.NaverCloudId);
                    request.Headers.Add("X-NCP-APIGW-API-KEY", _barunnConfig.Map.NaverCloudKey);
                    
                    HttpResponseMessage response = await client.SendAsync(request);
                    result = response.Content.ReadAsStringAsync().Result;
                }

            }
            catch
            { }

            return Content(result);
        }
        public async Task<IActionResult> GeocodeForNaver(string query)
        {
            string result = "";
            var url = new Uri("https://naveropenapi.apigw.ntruss.com/map-geocode/v2/geocode?query=" + HttpUtility.UrlEncode(query));
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Get;
                    request.RequestUri = url;
                    request.Headers.Add("X-NCP-APIGW-API-KEY-ID", _barunnConfig.Map.NaverCloudId);
                    request.Headers.Add("X-NCP-APIGW-API-KEY", _barunnConfig.Map.NaverCloudKey);

                    HttpResponseMessage response = await client.SendAsync(request);
                    result = response.Content.ReadAsStringAsync().Result;
                }

            }
            catch
            { }

            return Content(result);
        }

    }
}
