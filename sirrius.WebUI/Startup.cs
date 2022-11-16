using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using sirrius.WebUI.Models;
//using System;

namespace sirrius.WebUI
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
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection(AppSettings.SectionName));
            services.AddSingleton(s => s.GetRequiredService<IOptions<AppSettings>>().Value);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddMvc(options => options.EnableEndpointRouting = false)
            .SetCompatibilityVersion(CompatibilityVersion.Latest);

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(180); // 3 saat session zamani
            //});

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            ////HTTPClient service
            //services.AddHttpClient("sirrius-api", c =>
            //{
            //    c.BaseAddress = new Uri(Configuration.GetSection("AppSettings:ApiBaseURL").Value);

            //    c.DefaultRequestHeaders.Add("User-Agent", "sirriUS.WebUI");
            //});

            services.AddCookieManager(options =>
            {
                // Allow cookie data to encrypt by default it allow encryption
                options.AllowEncryption = false;
                // Throw if not all chunks of a cookie are available on a request for re-assembly.
                //options.ThrowForPartialCookies = true;
                // Set null if not allow to devide in chunks
                //options.ChunkSize = null;
                // Default Cookie expire time if expire time set to null of cookie
                // Default time is 1 day to expire cookie 
                //options.DefaultExpireTimeInDays = 10;
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors();

            //app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "giris",
            //        template: "{controller=Login}/{action=Index}");

            //    routes.MapRoute(
            //        name: "giris",
            //        template: "{controller=Home}/{action=Index}/{id?}");            
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "giris",
                    "{controller=Login}/{action=Index}");

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
