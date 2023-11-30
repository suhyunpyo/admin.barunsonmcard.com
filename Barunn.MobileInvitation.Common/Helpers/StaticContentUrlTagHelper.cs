using Barunn.MobileInvitation.Common.Models.Configs;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barunn.MobileInvitation.Common.Helpers
{
    /// <summary>
    /// 정적 콘텐츠 URL 생성 - barunnConfig.StaticContent.BaseUrl
    /// </summary>
    [HtmlTargetElement(Attributes = "static-href")]
    public class StaticContentUrlTagHelper : TagHelper
    {
        protected readonly BarunnConfig _barunnConfig;
        public string StaticHref { get; set; }
        public StaticContentUrlTagHelper(IOptions<BarunnConfig> barunnConfig)
        {
            _barunnConfig = barunnConfig.Value;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("static-href");

            var address = _barunnConfig.StaticContent.BaseUrl.Combine(StaticHref).AbsoluteUri;

            output.Attributes.SetAttribute("href", address);
        }
    }

    /// <summary>
    /// 정적  콘텐츠 URL 생성 - barunnConfig.StaticContent.BaseUrl
    /// </summary>
    [HtmlTargetElement(Attributes = "static-src")]
    public class StaticContentSrcTagHelper : TagHelper
    {
        protected readonly BarunnConfig _barunnConfig;
        public string StaticSrc { get; set; }
        public StaticContentSrcTagHelper(IOptions<BarunnConfig> barunnConfig)
        {
            _barunnConfig = barunnConfig.Value;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("static-src");

            var address = _barunnConfig.StaticContent.BaseUrl.Combine(StaticSrc).AbsoluteUri; 

            output.Attributes.SetAttribute("src", address);
        }
    }

    /// <summary>
    /// CDN 콘텐츠 URL 생성 - barunnConfig.StaticContent.CDNUri
    /// </summary>
    [HtmlTargetElement(Attributes = "cdn-href")]
    public class CDNContentUrlTagHelper : TagHelper
    {
        protected readonly BarunnConfig _barunnConfig;
        public string CdnHref { get; set; }
        public CDNContentUrlTagHelper(IOptions<BarunnConfig> barunnConfig)
        {
            _barunnConfig = barunnConfig.Value;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("cdn-href");

            var address = _barunnConfig.StaticContent.CDNUri.Combine(CdnHref).AbsoluteUri;

            output.Attributes.SetAttribute("href", address);
        }
    }

    /// <summary>
    /// 정적  콘텐츠 URL 생성 - barunnConfig.StaticContent.CDNUri
    /// </summary>
    [HtmlTargetElement(Attributes = "cdn-src")]
    public class CDNContentSrcTagHelper : TagHelper
    {
        protected readonly BarunnConfig _barunnConfig;
        public string CdnSrc { get; set; }
        public CDNContentSrcTagHelper(IOptions<BarunnConfig> barunnConfig)
        {
            _barunnConfig = barunnConfig.Value;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("cdn-src");

            var address = _barunnConfig.StaticContent.CDNUri.Combine(CdnSrc).AbsoluteUri; 
            output.Attributes.SetAttribute("src", address);
        }
    }
}
