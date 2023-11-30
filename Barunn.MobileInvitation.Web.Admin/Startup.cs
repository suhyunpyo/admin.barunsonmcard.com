using Barunn.MobileInvitation.Common.Helpers;
using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Dac.Contexts;
using Barunn.MobileInvitation.Web.Admin.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Barunn.MobileInvitation.Web.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Application gateway x-forword-for 설정
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = 5;
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("172.16.100.0"), 24));
            });
            #endregion

            #region DB Context & Repository 설정

            services.AddDbContext<BarunsonContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("BarunsonDBConn")));
            services.AddDbContext<BarShopContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("BarShopDBConn")));
            services.AddDbContext<ProtectKeysContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BarunsonDBConn")));
            #endregion

            #region Config Settings

            //Toss 결제 상점 키 정보, API 호출 서비스 등록
            if (Configuration.GetSection("PgMertInfos").Value == null)
                services.AddSingleton<List<PgMertInfo>>(Configuration.GetSection("PgMertInfos").Get<List<PgMertInfo>>());
            else
                services.AddSingleton<List<PgMertInfo>>(JsonSerializer.Deserialize<List<PgMertInfo>>(Configuration.GetSection("PgMertInfos").Get<string>()));
            services.AddScoped<ITossPaymentService, TossPaymentService>();

            var kakoinfo = Configuration.GetSection("KakaoBank").Get<KakaoBankConfig>();
            services.AddSingleton(kakoinfo);
 
            services.Configure<BarunnConfig>(Configuration.GetSection("BarunnConfig"));
            var barunnConfig = Configuration.GetSection("BarunnConfig").Get<BarunnConfig>();

            services.AddSingleton<IStaticContentHelper, StaticFileContentHelper>();
            #endregion

            #region 인증      


            services.AddDataProtection()
                .PersistKeysToDbContext<ProtectKeysContext>()
                .SetApplicationName("barun.mobileinvitation");

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = barunnConfig.AuthCookie.DefaultScheme;
            })
                .AddCookie(barunnConfig.AuthCookie.DefaultScheme, barunnConfig.AuthCookie.DefaultScheme, o =>
                {
                    o.Cookie.Name = barunnConfig.AuthCookie.DefaultScheme;
                    o.Cookie.Domain = barunnConfig.AuthCookie.Domain;
                    o.ExpireTimeSpan = TimeSpan.FromHours(1);
                    o.LoginPath = "/Member/LogIn";
                    o.LogoutPath = "/Member/LogOut";
                    o.Cookie.SecurePolicy = CookieSecurePolicy.None;
                });

            #endregion

            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All); // 한글이 인코딩되는 문제 해결
            });

            services.AddHttpClient();
            services.AddControllersWithViews();

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //정적 콘텐츠의 클라이언트 캐시 30일간 설정
            const string cacheMaxAge = "2592000"; //30 일
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append(
                         "Cache-Control", $"public, max-age={cacheMaxAge}");
                }
            });

            app.UseRouting();
            app.UseCookiePolicy();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
