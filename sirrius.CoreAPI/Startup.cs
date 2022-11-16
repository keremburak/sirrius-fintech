using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using sirrius.CoreAPI.Middleware;
using sirrius.Data;
using sirrius.Model;
using sirrius.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Helper;

namespace sirrius.CoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //load nLog config file
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<sirriusContext>();
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings object
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));

            //SMTP settings
            var smtpConfiguration = Configuration.GetSection("SmtpConfiguration").Get<SmtpConfiguration>();
            services.AddSingleton(smtpConfiguration);

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            //
            ////

            // configure DI for application services
            services.AddServiceDependency();

            //api versioning
            services.AddApiVersioning(opt =>
            {
                // Will provide the different api version which is available for the client
                opt.ReportApiVersions = true;
                // this configuration will allow the api to automaticaly take api_version=1.0 in case it was not specify
                opt.AssumeDefaultVersionWhenUnspecified = true;
                // We are giving the default version of 1.0 to the api
                //opt.DefaultApiVersion = ApiVersion.Default; // new ApiVersion(1, 0);
                opt.DefaultApiVersion = new ApiVersion(1, 0);

                opt.ApiVersionReader = ApiVersionReader.Combine(
                    new MediaTypeApiVersionReader("x-api-version"),
                    new HeaderApiVersionReader("x-api-version")
                );
            });

            //add in-memory cache
            services.AddMemoryCache();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "sirrius.CoreAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "sirrius.CoreAPI v1"));
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseMiddleware<ApiResponseMiddleware>("1.0", "sirriUS API");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
